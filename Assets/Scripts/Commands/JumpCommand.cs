using System.Collections;
using Abstract;
using UnityEngine;

namespace Commands
{
    public class JumpCommand: RobotCommand
    {
        private float _jumpStrength = 3000f;
        public JumpCommand(Robot robot) : base(robot)
        {
            this.CommandType = CommandType.JUMP;
        }

        protected override IEnumerator CommandEnumerator()
        {
            _robot.Rigidbody.AddForce(Vector3.up * _jumpStrength);
        
            //TODO Complete func
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();

            while (!Mathf.Approximately(_robot.Rigidbody.velocity.y, 0f))
            {
                yield return null;
            }

            while (_robot.IsStunned)
            {
                yield return null;
            }
        
            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(1);
            _robot.ResetCommandsRunning();

            yield return null;
        }
    }
}