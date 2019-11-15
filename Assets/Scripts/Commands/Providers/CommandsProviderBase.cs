using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandsProviderBase : MonoBehaviour
{
    [SerializeField] protected List<CommandType> _commands;

    public virtual void SetCommands(List<CommandType> commands)
    {
        this._commands = commands;
    }

    public virtual List<CommandType> GetCommands() => _commands;

}
