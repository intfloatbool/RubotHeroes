using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private float _timeToStart = 4f;
    private float _currentTimer;

    [System.Serializable]
    public class PlayerContainer
    {
        public Player Player;
        public RobotCommandRunner CommandRunner;
        public PlayerStatusPanel StatusPanel;
    }

    private Dictionary<PlayerOwner, PlayerContainer> _playersDict =
        new Dictionary<PlayerOwner, PlayerContainer>();

    [SerializeField] private bool _isRandomGameEvent;
    [SerializeField] private List<GameEventBase> _gameEvents;
    private Dictionary<GameEventType, GameEventBase> _gameEventsDict = new Dictionary<GameEventType, GameEventBase>();
    public event Action<GameEventBase> OnGameEventStarted = (gameEvent) => { };
    
    [SerializeField] private PlayerStatusPanel _userStatusPanel;
    [SerializeField] private PlayerStatusPanel _enemyStatusPanel;
    
    [SerializeField] private RobotCommandRunner _userCommandRunner;
    [SerializeField] private RobotCommandRunner _enemyCommandRunner;

    private IEnumerator Start()
    {
        InitializePlayerContainers();
        InitializeGameEvents();
        
        OnBeforeStart();
        
        _currentTimer = _timeToStart;
        while (_currentTimer > 0)
        {
            _currentTimer -= 1f;
            yield return new WaitForSeconds(1);
        }

        OnAfterStart();
    }

    private void OnBeforeStart()
    {
        InitializeStatusPanels();
        InitializeRobots();
    }

    private void OnAfterStart()
    {
        StartCommandRunners();
        
        if(_isRandomGameEvent)
            StartRandomGameEvent();
    }

    private void StartRandomGameEvent()
    {
        int eventCounts = Enum.GetNames(typeof(GameEventType)).Length;
        GameEventType rndEvent = (GameEventType) Random.Range(0, eventCounts);
        StartGameEvent(rndEvent);
    }

    private void StartGameEvent(GameEventType eventType)
    {
        GameEventBase gameEvent = GetGameEvent(eventType);
        if (gameEvent != null)
        {
            gameEvent.SetActiveEvent(true);
            OnGameEventStarted(gameEvent);
        }
        
    }

    private void InitializePlayerContainers()
    {
        _playersDict.Add(PlayerOwner.PLAYER_1, new PlayerContainer()
        {
            Player =  new Player(),
            CommandRunner = _userCommandRunner,
            StatusPanel =  _userStatusPanel
        });
        
        _playersDict.Add(PlayerOwner.PLAYER_2, new PlayerContainer()
        {
            Player =  new BotPlayer(),
            CommandRunner = _enemyCommandRunner,
            StatusPanel =  _enemyStatusPanel
        });
    }

    private void InitializeGameEvents()
    {
        foreach (GameEventBase gameEvent in _gameEvents)
        {
            if (!_gameEventsDict.ContainsKey(gameEvent.EventType))
            {
                _gameEventsDict.Add(gameEvent.EventType, gameEvent);
            }
        }
    }

    private void StartCommandRunners()
    {
        foreach (var playerContainer in _playersDict.Values)
        {
            playerContainer.CommandRunner.Initialize(playerContainer.Player.RobotCommands);
            playerContainer.StatusPanel.InitializeStatusPanel(playerContainer.Player);
        }
    }

    private void InitializeRobots()
    {
        foreach (var playerContainer in _playersDict.Values)
        {
            playerContainer.CommandRunner.InitializeRobot(playerContainer.Player);
        }
    }

    private void InitializeStatusPanels()
    {
        foreach (var playerContainer in _playersDict.Values)
        {
            playerContainer.StatusPanel.InitializeStatusPanel(playerContainer.Player);
        } 
    }

    public PlayerContainer GetPlayer(PlayerOwner owner)
    {
        return _playersDict[owner];
    }

    public GameEventBase GetGameEvent(GameEventType type)
    {
        return _gameEventsDict.ContainsKey(type) ? _gameEventsDict[type] : null;
    }
}
