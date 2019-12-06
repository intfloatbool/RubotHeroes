using System.Collections;
using UnityEngine;

namespace Commands
{
    public class MeeleAttackWithoutTarget : MeeleAttackCommand
    {
        public MeeleAttackWithoutTarget(Robot robot) : base(robot)
        {
            CommandType = CommandType.MEELE_ATTACK;
        }
        
        protected override IEnumerator CommandEnumerator()
        {
            
            OnUndelayedCommandRunning();
            _weapon.LaunchWeapon(_robot.gameObject);
            while (_weapon.IsInProcess)
            {
                yield return null;
            }
            _robot.TargetToMove = null;
            _robot.ResetCommandsRunning();
        }
    }
}
