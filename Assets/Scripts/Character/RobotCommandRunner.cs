using System;
using System.Collections;
using System.Collections.Generic;
using Abstract;
using Commands;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;
using Random = UnityEngine.Random;

public class RobotCommandRunner : MonoBehaviour
{
    [SerializeField] private Robot _robot;
    public Robot Robot => _robot;

    [SerializeField] private RobotPult _robotPult;

    private Coroutine _runCommandsCoroutine;

    [SerializeField] private bool _isEnabled;

    [SerializeField] private bool _useExternalCommandsProviders = false;
    [SerializeField] private CommandsProviderBase _instantCommandsProvider;
    
    
    public void InitializeRobot(Player player)
    {
        _robot.Initialize(player);
        PlayerProperties properties = player.Properties;
        
        _robot.InitializeRobotStatus(properties);
        _robot.InitializeWeapons(new[]
        {
            properties.GetWeaponByType(WeaponType.ROCKET_LAUNCHER),
            properties.GetWeaponByType(WeaponType.FIREGUN),
            properties.GetWeaponByType(WeaponType.LANDMINE)
        });
    }
    
    public void Initialize(List<CommandType> commandTypes)
    {
        if (!_isEnabled)
            return;

        List<CommandType> commandTypesForExexute = null;
        //For tests
        if (_instantCommandsProvider != null && _useExternalCommandsProviders)
        {
            commandTypesForExexute = _instantCommandsProvider.GetCommands();
            Debug.Log($"Robot pult #{_robot.gameObject.name} running by external command provider!!!");
        }
        else
        {
            commandTypesForExexute = commandTypes;
        }
        
        RunCommands(CommandHelper.GetCommandsByTypes(commandTypesForExexute, _robot));
        
        _isEnabled = false;
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
        
        Debug.LogWarning($"Commands has been ended on robot: {_robot.gameObject.name}!");
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
