using Abstract;

namespace Commands
{
    public class LaunchMissleCommand: RobotCommand
    {
        public LaunchMissleCommand(IRobot robot) : base(robot)
        {
        }

        public override void Execute()
        {
            base.Execute();
            _robot.LaunchMissle();
        }
    }
}