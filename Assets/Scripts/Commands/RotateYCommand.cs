using Abstract;

namespace Commands
{
    public class RotateYCommand: RobotCommand
    {
        private float _value;
        public RotateYCommand(IRobot robot, float value) : base(robot)
        {
            this._value = value;
        }

        public override void Execute()
        {
            base.Execute();
            _robot.RotateY(this._value);
        }
    }
}