using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class GameProcessStatus : MonoBehaviour
{
    [SerializeField] private List<RobotCommandRunner> _robotRunners;
    private bool _isWinnerDetected;
    public event Action<RobotCommandRunner> OnWinnerDetected = (robot) => { };

    private void Start()
    {
        foreach (RobotCommandRunner robotRunner in _robotRunners)
            robotRunner.Robot.OnDeath += OnRobotDead;
    }

    private void OnRobotDead()
    {
        if (_isWinnerDetected)
            return;
        RobotCommandRunner winnerRobotRunner = _robotRunners.FirstOrDefault(r => r.Robot.RobotStatus.IsDead == false);
        if (winnerRobotRunner != null)
        {
            Debug.Log($"WINNER! : {winnerRobotRunner.Robot.gameObject.name} !!!");
            OnWinnerDetected.Invoke(winnerRobotRunner);
            _isWinnerDetected = true;
        }
        else
        {
            Debug.LogError($"Cannot find WINNER!");
        }
    }
}
