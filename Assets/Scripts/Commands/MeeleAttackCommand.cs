using Abstract;

namespace Commands
{
    public class MeeleAttackCommand: RobotCommand
    {
        public MeeleAttackCommand(IRobot robot) : base(robot)
        {
            this.CommandType = CommandType.MEELE_ATTACK;

        }
        
        public override void Execute()
        {
            base.Execute();
            this._robot.MeeleAttack();
        }
    }
}