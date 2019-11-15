using System;
using System.Collections;
using System.Collections.Generic;
using Abstract;
using Commands;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class RobotCommandRunner : MonoBehaviour
{
    [SerializeField] private Robot _robot;
    public Robot Robot => _robot;

    [SerializeField] private RobotPult _robotPult;

    private Coroutine _runCommandsCoroutine;

    [SerializeField] private bool _isEnabled;
    
    [SerializeField] private bool _isRandomCommands;
    [SerializeField] private int _countOfRandomCommands = 10;

    [SerializeField] private CommandsProviderBase _instantCommandsProvider;

    private void Awake()
    {
        //For tests
        if (_instantCommandsProvider)
        {
            Initialize(_instantCommandsProvider.GetCommands());
        }
    }
    
    public void InitializeRobot(Player player)
    {
        _robot.Initialize(player);
        _robot.InitializeRobotStatus(player.Properties);
    }
    
    public void Initialize(List<CommandType> commandTypes)
    {
        if (!_isEnabled)
            return;
        
        IEnumerable<ICommand> commands = null;
        if (_isRandomCommands)
        {
            commands = CommandHelper.GetRandomCommands(_countOfRandomCommands,_robot);
        }
        else
        {
            if (commandTypes.Count == 0)
            {
                commands = CommandHelper.GetRandomCommands(_countOfRandomCommands, _robot);
            }
            else
            {
                commands = CommandHelper.GetCommandsByTypes(commandTypes, _robot);
            }
        }
        
        RunCommands(commands);
    }
    
    public void RunCommands(IEnumerable<ICommand> commands)
    {
        if (_runCommandsCoroutine != null)
        {
            Debug.Log($" {gameObject.name} : Commands changed!");
            StopCoroutine(_runCommandsCoroutine);
        }

        _runCommandsCoroutine = StartCoroutine(RunCommandsCoroutine(commands));
    }

    private IEnumerator RunCommandsCoroutine(IEnumerable<ICommand> commands)
    {
        while (!_robot.RobotStatus.IsDead)
        {
            foreach (ICommand command in commands)
            {
                if (_robot.ExternalCommand != null)
                {
                    yield return StartCoroutine(HandleCommand(_robot.ExternalCommand));
                    _robot.ExternalCommand = null;
                }
                
                yield return StartCoroutine(HandleCommand(command));
            }

            yield return null;
        }

        _runCommandsCoroutine = null;
    }

    private IEnumerator HandleCommand(ICommand command)
    {
        if (_robot.RobotStatus.IsDead)
            yield break;
        
        _robotPult.SetCommand(command);
        _robotPult.Run();
                
        //Wait until commands execute
        while (_robot.IsCommandsRunning )
        {
            if (_robot.RobotStatus.IsDead)
                yield break;
            yield return null;
        }
    }
    
    

}
