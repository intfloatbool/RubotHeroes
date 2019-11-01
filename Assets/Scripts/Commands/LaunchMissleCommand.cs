using Abstract;

namespace Commands
{
    public class LaunchMissleCommand: RobotCommand
    {
        public LaunchMissleCommand(IRobot robot) : base(robot)
        {
            this.CommandType = CommandType.LAUNCH_MISSLE;

        }

        public override void Execute()
        {
            base.Execute();
            _robot.LaunchMissle();
        }
    }
}