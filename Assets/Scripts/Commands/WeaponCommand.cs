using System.Collections;
using Abstract;
using Enums;
using UnityEngine;

namespace Commands
{
    public abstract class WeaponCommand : RobotCommand
    {
        protected WeaponLauncherBase _weapon;
        protected WeaponType _weaponType;
        protected WeaponCommand(Robot robot, WeaponType weaponType) : base(robot)
        {
            _weaponType = weaponType;
        }

        public override void Execute()
        {
            _weapon = _robot.GetWeaponByType(_weaponType);
            if (_weapon == null)
            {
                Debug.LogError($"Cannot get weapon from robot with type :{_weaponType} Command failed.");
                return;
            }
            base.Execute();
        }
    }
}