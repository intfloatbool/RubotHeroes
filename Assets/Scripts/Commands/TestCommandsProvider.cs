using System.Collections.Generic;
using Commands;
using UnityEngine;

public class TestCommandsProvider : CommandsProviderBase
{
    [SerializeField] protected List<CommandType> _commands;
    public override void SetCommands(List<CommandType> commands)
    {
        _commands = commands;
    }

    public override List<CommandType> GetCommands()
    {
        return _commands;
    }

    public override void ClearCommands()
    {
        _commands.Clear();
    }
}
