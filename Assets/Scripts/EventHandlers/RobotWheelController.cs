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
    [SerializeField] private float _defaultVolume = 0.5f;
    

    [SerializeField] private float _speed = 4f;
    private void FixedUpdate()
    {
        if (_robot.RobotStatus.IsDead)
            return;

        _engineSource.volume = _robot.IsMoving ? _defaultVolume : 0;
        if (!_robot.IsMoving)
            return;
        _leftWheel.Rotate(Vector3.up * _speed  * Time.fixedDeltaTime);
        _rightWheel.Rotate(Vector3.up * _speed * Time.fixedDeltaTime);
        
        
    }
    
}
