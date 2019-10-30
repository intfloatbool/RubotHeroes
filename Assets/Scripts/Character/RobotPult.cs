using UnityEngine;

public class RobotPult : MonoBehaviour, IInvoker
{
    public ICommand Command { get; private set; }
    public void SetCommand(ICommand command)
    {
        Command = command;
    }
    
    public void Run()
    {
        Command.Execute();
    }
}
