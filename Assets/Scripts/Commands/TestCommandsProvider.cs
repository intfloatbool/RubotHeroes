using System.Collections.Generic;
using Commands;

public class TestCommandsProvider : CommandsProviderBase
{
    public override IEnumerable<ICommand> GetCommands(Robot robot)
    {
        return new[]
        {
            new ProtectionShieldCommand(robot) 
        };
    }
}
