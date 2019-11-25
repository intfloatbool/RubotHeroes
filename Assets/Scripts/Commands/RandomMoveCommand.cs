using System.Collections;
using Abstract;
using UnityEngine;

namespace Commands
{
    public class RandomMoveCommand: RobotCommand
    {
        private float _timeToReachTarget = 2f;
        public RandomMoveCommand(Robot robot) : base(robot)
        {
            this.CommandType = CommandType.RANDOM_MOVE;
        }
        
        protected override IEnumerator CommandEnumerator()
        {
            _robot.IsRandomMove = true;
            Vector3[] expectionsPos =
            {
                _robot.RandomPos,
                _robot.EnemyRobot.RandomPos
            };
            Vector3 randomPos = WorldPositionsGenerator.Instance.GetRandomPosExcept(expectionsPos);
            _robot.RandomPos = randomPos;
            //TODO Complete func
            yield return new WaitForSeconds(_timeToReachTarget);
            if(!_robot.IsDestinationReach)
                yield return new WaitForSeconds(_timeToReachTarget);
            
            _robot.IsRandomMove = false;
            _robot.ResetCommandsRunning();
        }
    }
}