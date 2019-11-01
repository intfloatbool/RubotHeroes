using System;
using System.Collections;
using Commands;
using UnityEngine;

public class RobotWheelController : MonoBehaviour
{
    [SerializeField] private Robot _robot;

    [SerializeField] private Transform _leftWheel;
    [SerializeField] private Transform _rightWheel;

    [SerializeField] private AudioSource _engineSource;
    

    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _forwardVelocity;
    private void FixedUpdate()
    {
        _engineSource.volume = Mathf.Abs(_robot.Rigidbody.velocity.normalized.z);
        _forwardVelocity = _robot.Rigidbody.velocity.z;
        if (Mathf.Approximately(_forwardVelocity, 0f))
        {
            return;
        }

        _forwardVelocity = _robot.Rigidbody.velocity.z > 0 ? -1f : 1f;
        
        _leftWheel.Rotate(Vector3.up * _speed * _forwardVelocity * Time.fixedDeltaTime);
        _rightWheel.Rotate(Vector3.up * _speed * _forwardVelocity * Time.fixedDeltaTime);
        
        
    }
    
}
