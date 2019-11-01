using System;
using UnityEngine;

public class RobotPult : MonoBehaviour, IInvoker
{
    public event Action<ICommand> OnCommandExecuted = (command) => { };
    public ICommand Command { get; private set; }
    
    [SerializeField] private AudioSource _robotAudioSource;
    public AudioSource RobotAudioSource => _robotAudioSource;

    public void SetCommand(ICommand command)
    {
        Command = command;
    }
    
    public void Run()
    {
        Command.Execute();
        OnCommandExecuted.Invoke(Command);
    }
}
