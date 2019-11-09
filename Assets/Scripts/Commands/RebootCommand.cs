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
        yield return new WaitForSeconds(_rebootDelay);
        _robot.RobotStatus.ResetEnergy();
        _robot.ResetCommandsRunning();
    }
}
