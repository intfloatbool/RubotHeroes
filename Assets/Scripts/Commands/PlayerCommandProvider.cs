using System.Collections.Generic;

public class PlayerCommandProvider : CommandsProviderBase
{
    public override IEnumerable<CommandType> GetCommands(Robot robot)
    {
        return PlayerCommands.GetCommands();
    }
}
