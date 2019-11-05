using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public IEnumerable<CommandType> RobotCommands { get; set; } = null;
    public string NickName { get; set; } = "Unknown";
    public Color Color { get; set; } = Color.red;

}
