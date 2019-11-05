using System.Collections;

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
            this._robot.IsCommandsRunning = true;
            _robot.SetCurrentCommand(CommandEnumerator());
        }

        protected abstract IEnumerator CommandEnumerator();
    }
}