using UnityEngine;

public class RobotPult : MonoBehaviour, IInvoker
{
    public ICommand Command { get; private set; }
    public void SetCommand(ICommand command)
    {
        Command = command;
    }

    public void Cancel()
    {
        Command.Execute();
    }

    public void Run()
    {
        Command.Undo();
    }
}
