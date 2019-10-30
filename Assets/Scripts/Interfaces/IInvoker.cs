using System;

public interface IInvoker
{
    event Action<ICommand> OnCommandExecuted;
    ICommand Command { get; }
    void SetCommand(ICommand command);
    
    void Run();
}
