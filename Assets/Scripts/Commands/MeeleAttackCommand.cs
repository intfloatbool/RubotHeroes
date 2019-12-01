using System.Collections;
using Abstract;
using Enums;
using UnityEngine;

namespace Commands
{
    public class MeeleAttackCommand: WeaponCommand
    {
        private float _range = 5f;
        private float _delayAfterAttack = 1f;
        public MeeleAttackCommand(Robot robot) : base(robot, WeaponType.FIREGUN)
        {
            this.CommandType = CommandType.MEELE_ATTACK;
        }

        protected override IEnumerator CommandEnumerator()
        {
            yield return base.CommandEnumerator();
            _robot.DistanceFromDestiny = _range;
            _robot.TargetToMove = _robot.EnemyRobot.gameObject;
            while (_robot.DistanceFromDestiny >= _range)
            {
                yield return null;
            }
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