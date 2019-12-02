using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.BattleScene.Commands
{
    public class CommandsMonitorUI : CustomizableUI
    {
        [SerializeField] private Robot _robot;
        [SerializeField] private CommandView _commandViewPrefab;
        [SerializeField] private Transform _parentToSpawnCommandViews;
        [SerializeField] private List<CommandType> _executedCommands;
        
        
        private int _maxViews = 3;

        private Dictionary<int, CommandView> _commandViewDict = new Dictionary<int, CommandView>();
        
        private void Awake()
        {
            this.CheckLinks(new Object[]
            {
                _robot
            });
        
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _robot.OnCommandTypeExecuted += OnRobotCommand;
        }

        private void OnRobotCommand(CommandType cmdType)
        {
            //TODO realize views on command!
            AddCommand(cmdType);
        }

        private void AddCommand(CommandType cmdType)
        { 
            if (_executedCommands.Count >= _maxViews)
            {
                CommandType firstView = _executedCommands.FirstOrDefault();
                _executedCommands.Remove(firstView);
            }
        }

        private void UpdateCommandViews()
        {
            foreach (CommandType cmdType in _executedCommands)
            {
                
            }
        }

        private CommandView CreateView()
        {
            return Instantiate(_commandViewPrefab, _parentToSpawnCommandViews);
        }
    
    }
}
