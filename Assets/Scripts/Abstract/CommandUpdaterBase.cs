using System.Collections.Generic;
using UnityEngine;

public abstract class CommandUpdaterBase : MonoBehaviour
{
    public abstract Player Player { get; protected set; }
    public virtual void UpdateCommands(List<CommandType> commands)
    {
        Player.RobotCommands = commands;
    }
}
