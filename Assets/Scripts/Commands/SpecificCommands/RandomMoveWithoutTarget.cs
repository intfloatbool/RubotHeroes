using System.Collections;
using UnityEngine;

namespace Commands.SpecificCommands
{
    public class RandomMoveWithoutTarget : RandomMoveCommand
    {
        public RandomMoveWithoutTarget(Robot robot) : base(robot)
        {
            CommandType = CommandType.RANDOM_MOVE;
        }
        
        protected override IEnumerator CommandEnumerator()
        {
            OnUndelayedCommandRunning();
            _robot.ResetCommandsRunning();
            yield return null;
        }
    }
}
