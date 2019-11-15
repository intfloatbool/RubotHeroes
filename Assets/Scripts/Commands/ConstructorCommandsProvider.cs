using System.Collections.Generic;
using UnityEngine;

public class ConstructorCommandsProvider : CommandsProviderBase
{
    [SerializeField] private List<CommandType> _currentCommands;
    public override void SetCommands(List<CommandType> commands)
    {
        _currentCommands = commands;
    }

    public override List<CommandType> GetCommands() => _currentCommands;

    public override void ClearCommands()
    {
        _currentCommands.Clear();
    }
}
