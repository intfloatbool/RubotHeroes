using Abstract;

namespace Commands
{
    public class JumpCommand: RobotCommand
    {
        public JumpCommand(IRobot robot) : base(robot)
        {
            this.CommandType = CommandType.JUMP;
        }

        public override void Execute()
        {
            base.Execute();
            _robot.Jump();
        }
    }
}