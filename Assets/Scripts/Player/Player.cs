using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private List<CommandType> _robotCommands = new List<CommandType>();

    public List<CommandType> RobotCommands
    {
        get => _robotCommands;
        set => _robotCommands = value;
    }

    public string NickName { get; set; } = "Unknown";
    public Color Color { get; set; } = Color.red;

    public PlayerProperties Properties { get; set; } = new PlayerProperties();

}
