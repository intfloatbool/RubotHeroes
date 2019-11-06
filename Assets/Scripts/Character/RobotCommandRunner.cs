using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private CommandsProviderBase _commandsProvider;

    private void Start()
    {
        if (_commandsProvider != null)
        {
            Initialize(_commandsProvider.GetCommands(_robot));
        }
    }

    public void InitializeRobot(Player player)
    {
        _robot.Initialize(player);
    }
    
    public void Initialize(IEnumerable<CommandType> commandTypes)
    {
        if (!_isEnabled)
            return;
        
        IEnumerable<ICommand> commands = null;
        if (_isRandomCommands)
        {
            commands = GetRandomCommands();
        }
        else
        {
            if (commandTypes == null)
            {
                commands = GetRandomCommands();
            }
            else
            {
                commands = GetCommandsByTypes(commandTypes);
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

            yield return null;
        }

        _runCommandsCoroutine = null;
    }
    
    private IEnumerable<ICommand> GetRandomCommands()
    {
        for (int i = 0; i < _countOfRandomCommands; i++)
        {
            yield return GetRandomCommand();
        }

    }

    private IEnumerable<ICommand> GetCommandsByTypes(IEnumerable<CommandType> commandTypes)
    {
        foreach (CommandType type in commandTypes)
        {
            yield return GetCommandByType(type);
        }
    }
    private ICommand GetCommandByType(CommandType commandType)
    {
        switch (commandType)
        {
            case CommandType.JUMP:
            {
               return new JumpCommand(_robot); 
            }
            case CommandType.RANDOM_MOVE:
            {
                return new RandomMoveCommand(_robot); 
            }
            case CommandType.MEELE_ATTACK:
            {
                return new MeeleAttackCommand(_robot); 
            }
            case CommandType.LAUNCH_MISSLE:
            {
                return new LaunchMissleCommand(_robot); 
            }
            case CommandType.PROTECTED_SHIELD:
            {
                return new ProtectionShieldCommand(_robot); 
            }
            default:
                throw new Exception($"Command with type {commandType} not found!!!");
        }
    }
    
    private ICommand GetRandomCommand()
    {
        //TODO realize more commands!
        //READY COMMANDS!
        int commandsCount = 5;
        int random = Random.Range(0, commandsCount);

        switch (random)
        {
            case 0:
            {
                return new JumpCommand(_robot);
            }
            case 1:
            {
                return new RandomMoveCommand(_robot);
            }
            case 2:
            {
                return new LaunchMissleCommand(_robot);
            }
            case 3:
            {
                return new MeeleAttackCommand(_robot);
            }
            case 4:
            {
                return new ProtectionShieldCommand(_robot);
            }
            default:
            {
                throw new Exception($"Command with index ${random} not found!");
            }
        }
    }

}
