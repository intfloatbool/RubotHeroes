using System.Collections.Generic;
using Commands;

public class SecondTestCommandsProvider : CommandsProviderBase
{
    public override IEnumerable<ICommand> GetCommands(Robot robot)
    {
        return new[]
        {
            new LaunchMissleCommand(robot)
        };
    }
}
