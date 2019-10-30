namespace Abstract
{
    public abstract class RobotCommand: ICommand
    {
        protected IRobot _robot;
        public RobotCommand(IRobot robot)
        {
            this._robot = robot;
        }

        public abstract void Execute();
    }
}