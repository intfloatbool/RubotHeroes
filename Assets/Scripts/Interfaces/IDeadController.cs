public interface IDeadController
{
    IDeadable Deadable { get; }
    void HandleDeath();
}
