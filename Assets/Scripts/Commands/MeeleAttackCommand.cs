using System.Collections;
using Abstract;
using Enums;
using UnityEngine;

namespace Commands
{
    public class MeeleAttackCommand: WeaponCommand
    {
        private float _range = 4f;
        private float _delayAfterAttack = 1f;
        public MeeleAttackCommand(Robot robot) : base(robot, WeaponType.FIREGUN)
        {
            this.CommandType = CommandType.MEELE_ATTACK;
        }

        protected override IEnumerator CommandEnumerator()
        {
            _robot.DistanceFromDestiny = _range;
            while (_robot.DistanceFromDestiny >= _range)
            {
                _robot.MoveLoop(_robot.EnemyRobot.transform.position);
                yield return null;
            }
            _weapon.LaunchWeapon(_robot.gameObject);
            while (_weapon.IsInProcess)
            {
                yield return null;
            }
            yield return new WaitForSeconds(_delayAfterAttack);
            _robot.ResetCommandsRunning();
        }
    }
}