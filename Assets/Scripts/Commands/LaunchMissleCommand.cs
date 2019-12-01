using System.Collections;
using Abstract;
using Enums;
using UnityEngine;

namespace Commands
{
    public class LaunchMissleCommand: WeaponCommand, IChargable
    {
        public int ChargeCost { get; } = 2;

        public LaunchMissleCommand(Robot robot) : base(robot, WeaponType.ROCKET_LAUNCHER)
        {
            this.CommandType = CommandType.LAUNCH_MISSLE;
        }

        protected override IEnumerator CommandEnumerator()
        {
            yield return base.CommandEnumerator();
            _weapon.LaunchWeapon(_robot.gameObject);
            OnUndelayedCommandRunning();
            while (_weapon.IsInProcess)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            _robot.ResetCommandsRunning();
        }
    }
}