using Abstract;

namespace Commands
{
    public class GunAttackCommand: RobotCommand
    {
        private float _offset;
        public GunAttackCommand(IRobot robot, float offset) : base(robot)
        {
            this._offset = offset;
        }

        public override void Execute()
        {
            base.Execute();
            _robot.GunAttack(this._offset);
        }
    }
}