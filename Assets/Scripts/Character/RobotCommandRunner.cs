﻿using System;
using System.Collections;
using System.Collections.Generic;
using Commands;
using UnityEngine;
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
        if (!_isEnabled)
            return;
        
        IEnumerable<ICommand> commands =
            _isRandomCommands ? GetRandomCommands() : _commandsProvider.GetCommands(_robot);
        
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
        //TEST Commands
        ICommand[] testCommands =
        {
            new JumpCommand(_robot),
            new LaunchMissleCommand(_robot), 
            new RandomMoveCommand(_robot),
            new JumpCommand(_robot),
            new RandomMoveCommand(_robot),
            new JumpCommand(_robot),
            new RandomMoveCommand(_robot),
            new MeeleAttackCommand(_robot),
            new ProtectionShieldCommand(_robot)
        };

        for (int i = 0; i < _countOfRandomCommands; i++)
        {
            yield return GetRandomCommand();
        }

    }

    private ICommand GetRandomCommand()
    {
        //TODO realize more commands!
        
        //READY COMMANDS!
        int commandsCount = 3;
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
            default:
            {
                throw new Exception($"Command with index ${random} not found!");
            }
        }
    }

}
