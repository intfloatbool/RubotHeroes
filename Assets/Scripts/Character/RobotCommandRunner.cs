using System.Collections;
using System.Collections.Generic;
using Commands;
using UnityEngine;

public class RobotCommandRunner : MonoBehaviour
{
    [SerializeField] private Robot _robot;
    [SerializeField] private RobotPult _robotPult;

    private Coroutine _runCommandsCoroutine;
    
    private void Start()
    {
        //TEST Commands
        ICommand[] testCommands =
        {
            new JumpCommand(_robot), 
            new GunAttackCommand(_robot, 50),
            new LaunchMissleCommand(_robot, 25), 
            new LookAtEnemyCommand(_robot), 
            new MoveXCommand(_robot, 250),  
            new MoveYCommand(_robot, 250),  
            new RotateXCommand(_robot, 500),
            new RotateYCommand(_robot, 600)
        };
        
        RunCommands(testCommands);
    }
    
    public void RunCommands(IEnumerable<ICommand> commands)
    {
        if (_runCommandsCoroutine != null)
        {
            Debug.LogError("Cannot run commands coroutine! Commands already executed!");
            return;
        }

        _runCommandsCoroutine = StartCoroutine(RunCommandsCoroutine(commands));
    }

    private IEnumerator RunCommandsCoroutine(IEnumerable<ICommand> commands)
    {
        foreach (ICommand command in commands)
        {
            _robotPult.SetCommand(command);
            _robotPult.Run();

            while (_robot.IsCommandsRunning)
            {
                yield return null;
            }
        }

        _runCommandsCoroutine = null;
    }

}
