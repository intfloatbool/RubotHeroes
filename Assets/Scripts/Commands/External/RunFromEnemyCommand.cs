using System.Collections;
using Abstract;
using UnityEngine;

namespace Commands.External
{
    public class RunFromEnemyCommand : RobotCommand
    {
        private float _delayToChangePos = 2f;
        private float _neededDistance = 2f;
        public RunFromEnemyCommand(Robot robot) : base(robot)
        {
            CommandType = CommandType.RUN_FROM_ENEMY;
        }

        protected override IEnumerator CommandEnumerator()
        {
            yield return WaitIfRobotStunned();
            OnUndelayedCommandRunning();
            _robot.IsRandomMove = true;
            while (_robot.DistanceToEnemy <= _neededDistance)
            {
                Vector3 randomPos = WorldPositionsGenerator.Instance.GetLongestPosition(
                    _robot.EnemyRobot.transform.position);
                _robot.RandomPos = randomPos;
                
                yield return new WaitForSeconds(_delayToChangePos);
            }
            
            _robot.ResetCommandsRunning();
            _robot.IsRandomMove = false;
        }
        
    }
}