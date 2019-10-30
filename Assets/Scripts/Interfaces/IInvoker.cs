public interface IInvoker 
{ 
    ICommand Command { get; }
    void SetCommand(ICommand command);

    void Cancel();
    void Run();
}
