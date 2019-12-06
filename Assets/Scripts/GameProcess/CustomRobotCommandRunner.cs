using UnityEngine;

namespace GameProcess
{
    [RequireComponent(typeof(Robot))]
    public class CustomRobotCommandRunner : MonoBehaviour
    {
        [SerializeField] protected CommandType _command;
        public CommandType Command
        {
            get => _command;
            set => _command = value;
        }

        [SerializeField] protected Robot _robot;

        public virtual void RunCommand()
        {
            ICommand command = CommandHelper.GetCommandByType(_command, _robot);
            command.Execute();
        }

        public virtual void RunSpecificCommand(CommandType commandType)
        {
            ICommand command = CommandHelper.GetCommandByType(commandType, _robot);
            command.Execute();
        }
    }
}
