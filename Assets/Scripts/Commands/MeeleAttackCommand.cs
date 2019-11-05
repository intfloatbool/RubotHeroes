using System.Collections;
using Abstract;
using Enums;
using UnityEngine;

namespace Commands
{
    public class MeeleAttackCommand: WeaponCommand
    {
        public MeeleAttackCommand(Robot robot) : base(robot, WeaponType.FIREGUN)
        {
            this.CommandType = CommandType.MEELE_ATTACK;
        }

        protected override IEnumerator CommandEnumerator()
        {
            _robot.DistanceFromDestiny = 2.5f;
            while (_robot.DistanceFromDestiny >= 2.5f)
            {
                _robot.MoveLoop(_robot.EnemyRobot.transform.position);
                yield return null;
            }
            
            _weapon.LaunchWeapon(_robot.gameObject);
            while (_weapon.IsInProcess)
            {
                yield return null;
            }
            yield return new WaitForEndOfFrame();
            _robot.ResetCommandsRunning();
        }
    }
}