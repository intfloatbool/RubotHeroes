using System.Collections;
using Abstract;
using UnityEngine;

public class RebootCommand : RobotCommand
{
    protected float _rebootDelay = 2f;
    public RebootCommand(Robot robot) : base(robot)
    {
        CommandType = CommandType.REBOOT;
    }

    protected override IEnumerator CommandEnumerator()
    {
        _robot.RobotStatus.ResetEnergy();
        yield return new WaitForSeconds(_rebootDelay);
        _robot.ResetCommandsRunning();
    }
}
