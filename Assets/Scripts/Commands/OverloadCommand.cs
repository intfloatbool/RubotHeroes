using UnityEngine;

public class OverloadCommand : RebootCommand
{
    public OverloadCommand(Robot robot) : base(robot)
    {
        CommandType = CommandType.OVERLOAD;
        _rebootDelay *= 2;
    }
}
