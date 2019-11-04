using System.Collections.Generic;
using Commands;

public class TestCommandsProvider : CommandsProviderBase
{
    public override IEnumerable<CommandType> GetCommands(Robot robot)
    {
        return new[]
        {
            CommandType.PROTECTED_SHIELD
        };
    }
}
