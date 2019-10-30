using Abstract;

namespace Commands
{
    public class JumpCommand: RobotCommand
    {
        public JumpCommand(IRobot robot) : base(robot)
        {
        }

        public override void Execute()
        {
            _robot.Jump();
        }
    }
}