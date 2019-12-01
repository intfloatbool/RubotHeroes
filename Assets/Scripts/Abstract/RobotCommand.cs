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

        protected virtual IEnumerator CommandEnumerator()
        {
            yield return WaitIfRobotStunned();
        }

        protected IEnumerator WaitIfRobotStunned()
        {
            if(_robot.IsStunned)
                Debug.Log($"{_robot.gameObject.name} is stunned! Wait...");
            while (_robot.IsStunned)
            {
                yield return null;
            }
        }
        
        
        /// <summary>
        /// Use this method to notify robot about Command exactly running,
        /// to avoid issues with delay on some commands. For example sound listener
        /// of robot should know when command excatly running, but not with delay.
        /// </summary>
        protected virtual void OnUndelayedCommandRunning()
        {
            _robot.NotifyThatCommandRun(CommandType);
        }
    }
}