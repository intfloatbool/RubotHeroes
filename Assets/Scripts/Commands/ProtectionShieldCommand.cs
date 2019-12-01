using System.Collections;
using Abstract;
using UnityEngine;

namespace Commands
{
    public class ProtectionShieldCommand: RobotCommand, IChargable
    {
        public int ChargeCost { get; } = 3;

        private float _duration = 2.5f;
        public ProtectionShieldCommand(Robot robot) : base(robot)
        {
            this.CommandType = CommandType.PROTECTED_SHIELD;
        }
        
        protected override IEnumerator CommandEnumerator()
        {
            yield return base.CommandEnumerator();
            yield return new WaitForEndOfFrame();
            _robot.RobotStatus.IsOnShield = true;
            _robot.ShieldEffect.SetActive(true);
            OnUndelayedCommandRunning();
            yield return new WaitForSeconds(_duration);
            _robot.ShieldEffect.SetActive(false);
            _robot.RobotStatus.IsOnShield = false;
            _robot.ResetCommandsRunning();
        }
    }
}