using System.Collections;
using UnityEngine;

public class DummyLandMineCommand : PutLandmineCommand
{
    private float _timeToblow = 2f;
    public DummyLandMineCommand(Robot robot) : base(robot)
    {
        CommandType = CommandType.PUT_LANDMINE;
    }
    
    protected override IEnumerator CommandEnumerator()
    {
        OnUndelayedCommandRunning();

        LandMineLauncher landMineLauncher = (LandMineLauncher) _weapon;
        if (landMineLauncher != null)
            landMineLauncher.TimeToBlow = _timeToblow;
         
        _weapon.LaunchWeapon(_robot.gameObject);
        while (_weapon.IsInProcess)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        _robot.ResetCommandsRunning();
    }
}
