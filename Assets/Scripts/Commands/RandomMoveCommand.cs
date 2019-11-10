using System.Collections;
using Abstract;
using UnityEngine;

namespace Commands
{
    public class RandomMoveCommand: RobotCommand
    {
        public RandomMoveCommand(Robot robot) : base(robot)
        {
            this.CommandType = CommandType.RANDOM_MOVE;
        }
        
        protected override IEnumerator CommandEnumerator()
        {
            _robot.IsRandomMove = true;
            Vector3 lastRandomPos = _robot.RandomPos;
            Vector3 randomPos = WorldPositionsGenerator.Instance.RandomPosition;
            while (lastRandomPos == randomPos)
            {
                randomPos = WorldPositionsGenerator.Instance.RandomPosition;
            }
            _robot.RandomPos = randomPos;
            //TODO Complete func
            yield return new WaitForSeconds(3);
            _robot.IsRandomMove = false;
            _robot.ResetCommandsRunning();
        }
    }
}