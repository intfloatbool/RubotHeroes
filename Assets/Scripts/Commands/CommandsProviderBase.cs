using System.Collections.Generic;
using UnityEngine;

public abstract class CommandsProviderBase : MonoBehaviour
{
    public abstract void SetCommands(List<CommandType> commands);
    public abstract List<CommandType> GetCommands();
    public abstract void ClearCommands();
}
