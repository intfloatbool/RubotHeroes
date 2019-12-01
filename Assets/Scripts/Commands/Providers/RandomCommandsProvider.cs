using System.Collections.Generic;
using System.Linq;
using Global.Extensions;
using UnityEngine;

public class RandomCommandsProvider : CommandsProviderBase
{
    public override List<CommandType> GetCommands()
    {
        _commands.Shuffle();
        return base.GetCommands();
    }
    [SerializeField] private int _countOfRandomCommands = 10;
    protected virtual void Start()
    {
        _commands = CommandHelper.GetRandomCommandTypes(_countOfRandomCommands).ToList();
    }
}
