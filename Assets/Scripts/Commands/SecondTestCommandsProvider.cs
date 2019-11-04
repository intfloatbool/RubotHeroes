using System.Collections.Generic;
using Commands;

public class SecondTestCommandsProvider : CommandsProviderBase
{
    public override IEnumerable<CommandType> GetCommands(Robot robot)
    {
        return new[]
        {
            CommandType.MEELE_ATTACK
        };
    }
}
