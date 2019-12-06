using System.Collections.Generic;
using Commands;
using Commands.SpecificCommands;
using UnityEngine;

namespace GameProcess
{
    public class DummyRobotCommandRunner : CustomRobotCommandRunner
    {
        private Dictionary<CommandType, ICommand> _changedCommandsDict;
        private Vector3 _botBasePosition;
        private void Awake()
        {
            InitializeDict();
            _botBasePosition = _robot.transform.position;
        }

        void ResetBotPosition()
        {
            _robot.transform.position = _botBasePosition;
        }

        void InitializeDict()
        {
            _changedCommandsDict = new Dictionary<CommandType, ICommand>()
            {
                {CommandType.MEELE_ATTACK, new MeeleAttackWithoutTarget(_robot)},
                {CommandType.RANDOM_MOVE, new RandomMoveWithoutTarget(_robot)},
                {CommandType.PUT_LANDMINE, new DummyLandMineCommand(_robot)},
            };
        }
        
        public override void RunCommand()
        {
            if (_robot.IsCommandsRunning)
                return;
            
            ICommand command = GetCommandByDummyState(_command);
            command.Execute();
            ResetBotPosition();
        }
        
        public override void RunSpecificCommand(CommandType commandType)
        {
            _command = commandType;
            RunCommand();
        }
        
        private ICommand GetCommandByDummyState(CommandType cmdType)
        {
            if (_changedCommandsDict.ContainsKey(cmdType))
            {
                return _changedCommandsDict[cmdType];
            }

            return CommandHelper.GetCommandByType(cmdType, _robot);
        }
    }
}
