using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    [SerializeField] private PlayerStatusPanel _userStatusPanel;
    [SerializeField] private PlayerStatusPanel _enemyStatusPanel;
    
    [SerializeField] private RobotCommandRunner _userCommandRunner;
    [SerializeField] private RobotCommandRunner _enemyCommandRunner;

    private IEnumerator Start()
    {
        InitializePlayerContainers();
        
        OnBeforeStart();
        
        _currentTimer = _timeToStart;
        while (_currentTimer > 0)
        {
            _currentTimer -= 1f;
            yield return new WaitForSeconds(1);
        }
        StartCommandRunners();
    }

    private void OnBeforeStart()
    {
        InitializeStatusPanels();
        InitializeRobots();
    }

    private void InitializePlayerContainers()
    {
        _playersDict.Add(PlayerOwner.PLAYER_1, new PlayerContainer()
        {
            Player =  GlobalPlayersSide.UserPlayer,
            CommandRunner = _userCommandRunner,
            StatusPanel =  _userStatusPanel
        });
        
        _playersDict.Add(PlayerOwner.PLAYER_2, new PlayerContainer()
        {
            Player =  GlobalPlayersSide.EnemyPlayer,
            CommandRunner = _enemyCommandRunner,
            StatusPanel =  _enemyStatusPanel
        });
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
}
