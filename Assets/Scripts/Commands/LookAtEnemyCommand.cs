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
            _robot.LookAtEnemy();
        }
    }
}