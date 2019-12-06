using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.BattleScene.Commands
{
    public class CommandsMonitorUI : CustomizableUI
    {
        private GameProcessStatus _gameProcessStatus;
        [SerializeField] private Robot _robot;
        [SerializeField] private UIView _commandViewPrefab;
        [SerializeField] private Transform _parentToSpawnCommandViews;
        private int _maxViews = 3;
        private int _currentViewId = 0;
        
        private Dictionary<int, UIView> _commandViewDict = new Dictionary<int, UIView>();
        private CommandType _lastCommand;
        private void Awake()
        {
            _gameProcessStatus = FindObjectOfType<GameProcessStatus>();
            this.CheckLinks(new Object[]
            {
                _robot,
                _gameProcessStatus
            });
        
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _robot.OnCommandTypeExecuted += OnRobotCommand;
            _gameProcessStatus.OnWinnerDetected += (runner) => { gameObject.SetActive(false); };
        }

        private void OnRobotCommand(CommandType cmdType)
        {
            if (!CommandHelper.ReadyCommands.Contains(cmdType))
                return;
            
            _lastCommand = cmdType;
            //TODO realize views on command!
            InitializeView();
        }

        private void InitializeView()
        {
            if (_currentViewId > _maxViews - 1)
                ResetViews();
            
            UIView currentView;
            if (_commandViewDict.ContainsKey(_currentViewId))
            {
                currentView = _commandViewDict[_currentViewId];
                currentView.gameObject.SetActive(true);
            }
            else
            {
                currentView = CreateView();
                _commandViewDict.Add(_currentViewId, currentView);
            }
            
            currentView.SetText(GameLocalization.GetLocalization(_lastCommand.ToString()));
            _currentViewId++;
        }

        private void ResetViews()
        {
            _currentViewId = 0;
            
            foreach (var view in _commandViewDict.Values)
            {
                view.gameObject.SetActive(false);
            }
        }
        private UIView CreateView()
        {
            return Instantiate(_commandViewPrefab, _parentToSpawnCommandViews);
        }
    
    }
}
