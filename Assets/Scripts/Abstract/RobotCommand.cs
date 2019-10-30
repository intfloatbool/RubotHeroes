namespace Abstract
{
    public abstract class RobotCommand: ICommand
    {
        protected IRobot _robot;
        public RobotCommand(IRobot robot)
        {
            this._robot = robot;
        }

        public virtual void Execute()
        {
            this._robot.IsCommandsRunning = true;
        }
    }
}