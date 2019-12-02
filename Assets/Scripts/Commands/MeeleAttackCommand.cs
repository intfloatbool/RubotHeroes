using System.Collections;
using Abstract;
using Enums;
using UnityEngine;

namespace Commands
{
    public class MeeleAttackCommand: WeaponCommand
    {
        private float _range = 6f;
        private float _delayAfterAttack = 1f;
        public MeeleAttackCommand(Robot robot) : base(robot, WeaponType.FIREGUN)
        {
            this.CommandType = CommandType.MEELE_ATTACK;
        }

        protected override IEnumerator CommandEnumerator()
        {
            yield return base.CommandEnumerator();
            _robot.TargetToMove = _robot.EnemyRobot.gameObject;
            while (_robot.DistanceToEnemy >= _weapon.WeaponRange)
            {
                yield return null;
            }

            _robot.TargetToMove = null;
            
            OnUndelayedCommandRunning();
            _weapon.LaunchWeapon(_robot.gameObject);
            while (_weapon.IsInProcess)
            {
                yield return null;
            }
            yield return new WaitForSeconds(_delayAfterAttack);
            _robot.TargetToMove = null;
            _robot.ResetCommandsRunning();
        }
    }
}