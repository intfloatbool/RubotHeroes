using Abstract;

namespace Commands
{
    public class LookAtEnemyCommand: RobotCommand
    {
        public LookAtEnemyCommand(IRobot robot) : base(robot)
        {
        }

        public override void Execute()
        {
            base.Execute();
            _robot.LookAtEnemy();
        }
    }
}