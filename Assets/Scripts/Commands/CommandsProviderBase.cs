using System.Collections.Generic;
using UnityEngine;

public abstract class CommandsProviderBase : MonoBehaviour
{
    public abstract IEnumerable<ICommand> GetCommands(Robot robot);
}
