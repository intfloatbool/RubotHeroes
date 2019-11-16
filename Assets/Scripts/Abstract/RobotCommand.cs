using System.Collections;
using UnityEngine;

namespace Abstract
{
    public abstract class RobotCommand: ICommand
    {
        public CommandType CommandType { get; protected set; }
        protected Robot _robot;
        public RobotCommand(Robot robot)
        {
            this._robot = robot;
        }

        public virtual void Execute()
        {
            Debug.Log($"COMMANDS: {_robot.gameObject.name} starting to execute command : {CommandType.ToString()}");
            this._robot.IsCommandsRunning = true;
            _robot.SetCurrentCommand(CommandEnumerator(), this);
        }

        protected abstract IEnumerator CommandEnumerator();
    }
}