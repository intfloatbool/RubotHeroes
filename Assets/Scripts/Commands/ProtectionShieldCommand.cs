using Abstract;

namespace Commands
{
    public class ProtectionShieldCommand: RobotCommand
    {
        public ProtectionShieldCommand(IRobot robot) : base(robot)
        {
            this.CommandType = CommandType.PROTECTED_SHIELD;
        }
        
        public override void Execute()
        {
            base.Execute();
            this._robot.ProtectionShield();
        }
    }
}