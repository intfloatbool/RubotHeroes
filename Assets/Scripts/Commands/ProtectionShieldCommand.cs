using System.Collections;
using Abstract;
using UnityEngine;

namespace Commands
{
    public class ProtectionShieldCommand: RobotCommand
    {
        public ProtectionShieldCommand(Robot robot) : base(robot)
        {
            this.CommandType = CommandType.PROTECTED_SHIELD;
        }
        
        protected override IEnumerator CommandEnumerator()
        {
            _robot.RobotStatus.IsOnShield = true;
            _robot.ShieldEffect.SetActive(true);
            yield return new WaitForSeconds(2);
            _robot.ShieldEffect.SetActive(false);
            _robot.RobotStatus.IsOnShield = false;
            _robot.ResetCommandsRunning();
        }
    }
}