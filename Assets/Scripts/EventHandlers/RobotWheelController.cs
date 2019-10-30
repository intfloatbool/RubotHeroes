using System.Collections;
using Commands;
using UnityEngine;

public class RobotWheelController : MonoBehaviour
{
    [SerializeField] private Robot _robot;
    [SerializeField] private RobotPult _robotPult;

    [SerializeField] private Transform _leftWheel;
    [SerializeField] private Transform _rightWheel;
    
    private Coroutine _currentCoroutine;

    [SerializeField] private float _speed = 4f;
    
    private void Awake()
    {
        _robotPult.OnCommandExecuted += OnRobotMove;
    }
    
    private void OnRobotMove(ICommand command)
    {

        if (command is MoveXCommand || command is MoveYCommand)
        {
            float forwardVelocity = _robot.Rigidbody.velocity.normalized.z;
            StopCurrentCoroutineIfExists();
            _currentCoroutine = StartCoroutine(MoveWheelsCoroutine(forwardVelocity));
        } 

        else if (command is RotateXCommand || command is RotateYCommand)
        {
            StopCurrentCoroutineIfExists();
        }
    }

    private void StopCurrentCoroutineIfExists()
    {
        if (this._currentCoroutine != null)
        {
            StopCoroutine(this._currentCoroutine);
            this._currentCoroutine = null;
        }
    }

    private IEnumerator MoveWheelsCoroutine(float origin)
    {
        while (_robot.IsCommandsRunning)
        {
            _leftWheel.Rotate(Vector3.right * _speed * origin * Time.deltaTime);
            _rightWheel.Rotate(Vector3.right * _speed * origin * Time.deltaTime);
            yield return null;
        }
    }
}
