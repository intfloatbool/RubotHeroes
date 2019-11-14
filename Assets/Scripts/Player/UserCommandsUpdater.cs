using UnityEngine;

public class UserCommandsUpdater : CommandUpdaterBase
{
    public override Player Player { get; protected set; } = GlobalPlayersSide.UserPlayer;
}
