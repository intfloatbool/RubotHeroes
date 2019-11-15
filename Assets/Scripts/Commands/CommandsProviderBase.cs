using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandsProviderBase : MonoBehaviour
{
    protected Player _player;
    public Player Player
    {
        get => _player;
        set => _player = value;
    }

    public virtual void SetCommands(List<CommandType> commands)
    {
        _player.RobotCommands = commands;
    }

    public abstract List<CommandType> GetCommands();

    public virtual void ClearCommands()
    {
        _player.RobotCommands.Clear();
    }
}
