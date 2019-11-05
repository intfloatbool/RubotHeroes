using System.Collections.Generic;
using Commands;
using UnityEngine;

public class TestCommandsProvider : CommandsProviderBase
{
    [SerializeField] private List<CommandType> _commands;
    public override IEnumerable<CommandType> GetCommands(Robot robot)
    {
        return _commands;
    }
}
