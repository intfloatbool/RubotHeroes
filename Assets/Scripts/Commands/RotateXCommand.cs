using Abstract;

namespace Commands
{
    public class RotateXCommand: RobotCommand
    {
        private float _value;
        public RotateXCommand(IRobot robot, float value) : base(robot)
        {
            this._value = value;
        }

        public override void Execute()
        {
            _robot.RotateX(this._value);
        }
    }
}