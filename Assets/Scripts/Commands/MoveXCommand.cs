using Abstract;

namespace Commands
{
    public class MoveXCommand: RobotCommand
    {
        private float _value;
        public MoveXCommand(IRobot robot, float value) : base(robot)
        {
            this._value = value;
        }

        public override void Execute()
        {
            base.Execute();
            _robot.MoveX(this._value);
        }
    }
}