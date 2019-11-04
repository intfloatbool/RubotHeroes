using System.Collections.Generic;
using System.Linq;

public static class PlayerCommands
{
    private static List<CommandType> Commands;

    public static void SetCommands(IEnumerable<CommandType> commands)
    {
        Commands = commands?.ToList();
    }

    public static List<CommandType> GetCommands()
    {
        return Commands;
    }
}
