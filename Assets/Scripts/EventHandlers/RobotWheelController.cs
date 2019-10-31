using System;
using System.Collections;
using Commands;
using UnityEngine;

public class RobotWheelController : MonoBehaviour
{
    [SerializeField] private Robot _robot;

    [SerializeField] private Transform _leftWheel;
    [SerializeField] private Transform _rightWheel;
    

    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _forwardVelocity;
    private void FixedUpdate()
    {
        _forwardVelocity = _robot.Rigidbody.velocity.z;
        if (Mathf.Approximately(_forwardVelocity, 0f))
        {
            return;
        }
        _leftWheel.Rotate(Vector3.right * _speed * _forwardVelocity * Time.deltaTime);
        _rightWheel.Rotate(Vector3.right * _speed * _forwardVelocity * Time.deltaTime);
    }
    
}
