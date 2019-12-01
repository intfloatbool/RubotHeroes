using System.Collections;
using Commands;
using Enums;
using UnityEngine;

public class PutLandmineCommand : WeaponCommand, IChargable
{
    public int ChargeCost { get; } = 2;
    
    public PutLandmineCommand(Robot robot) : base(robot, WeaponType.LANDMINE)
    {
        this.CommandType = CommandType.PUT_LANDMINE;
    }

    protected override IEnumerator CommandEnumerator()
    {
        yield return base.CommandEnumerator();
        OnUndelayedCommandRunning();
        _weapon.LaunchWeapon(_robot.gameObject);
        while (_weapon.IsInProcess)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        _robot.ResetCommandsRunning();
    }
    
}
