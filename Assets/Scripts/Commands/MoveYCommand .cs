using Abstract;

namespace Commands
{
    public class MoveYCommand: RobotCommand
    {
        private float _value;
        public MoveYCommand(IRobot robot, float value) : base(robot)
        {
            this._value = value;
        }

        public override void Execute()
        {
            _robot.MoveY(this._value);
        }
    }
}