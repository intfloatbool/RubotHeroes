using Abstract;

namespace Commands
{
    public class LaunchMissleCommand: RobotCommand
    {
        private float _offset;
        public LaunchMissleCommand(IRobot robot, float offset) : base(robot)
        {
            this._offset = offset;
        }

        public override void Execute()
        {
            _robot.LaunchMissle(this._offset);
        }
    }
}