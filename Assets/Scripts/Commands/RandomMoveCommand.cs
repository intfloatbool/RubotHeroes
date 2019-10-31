using Abstract;

namespace Commands
{
    public class RandomMoveCommand: RobotCommand
    {
        public RandomMoveCommand(IRobot robot) : base(robot)
        {
        }
        
        public override void Execute()
        {
            base.Execute();
            this._robot.RandomMove();
        }
    }
}