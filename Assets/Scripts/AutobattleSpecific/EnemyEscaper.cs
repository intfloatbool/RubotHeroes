using System;
using Commands.External;
using UnityEngine;

/// <summary>
/// This script makes robot running from enemy to avoid each blocking.
/// </summary>
public class EnemyEscaper : MonoBehaviour
{
    [SerializeField] private float _distanceOfDanger = 1.5f;
    [SerializeField] private Robot _robot;
    private RunFromEnemyCommand _runCommand;
    [SerializeField] private bool _isRobotRunFromEnemy;
    private void Awake()
    {
        if(_robot == null)
            Debug.LogError("Robot is null!");
        
        _runCommand = new RunFromEnemyCommand(_robot);
    }

    private void FixedUpdate()
    {
        bool isAlreadyRunning =
            _robot.ExternalCommand != null &&
            _robot.ExternalCommand.CommandType == CommandType.RUN_FROM_ENEMY;

        _isRobotRunFromEnemy = isAlreadyRunning;
        
        if (isAlreadyRunning)
            return;

        bool isMeeleContact = _robot.CurrentCommand == CommandType.MEELE_ATTACK;

        if (isMeeleContact)
            return;

        Robot enemy = _robot.EnemyRobot;
        if (enemy == null)
            return;
        bool isAnyDead = enemy.UnitStatus.IsDead || _robot.UnitStatus.IsDead;
        if (isAnyDead)
            return;

        bool isDangeringDistance = _robot.DistanceToEnemy <= _distanceOfDanger;
        if (!isDangeringDistance)
            return;
        _robot.ExternalCommand = _runCommand;
    }
}
