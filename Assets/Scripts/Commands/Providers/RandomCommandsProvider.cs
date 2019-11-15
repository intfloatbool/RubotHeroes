using System.Linq;
using UnityEngine;

public class RandomCommandsProvider : CommandsProviderBase
{
    [SerializeField] private int _countOfRandomCommands = 10;
    protected virtual void Start()
    {
        _commands = CommandHelper.GetRandomCommandTypes(_countOfRandomCommands).ToList();
    }
}
