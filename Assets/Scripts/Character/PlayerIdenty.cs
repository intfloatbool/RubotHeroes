using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdenty : MonoBehaviour
{
    public enum PlayerOwner
    {
        PLAYER_1,
        PLAYER_2
    }

    [SerializeField] private PlayerFlag _flagPrefab;
    
    [SerializeField] private Robot[] _robots;
    [SerializeField] private Dictionary<PlayerOwner, Color> _playerIdentyDict = new Dictionary<PlayerOwner, Color>();

    private void Start()
    {
        InitializeDict();

        foreach (Robot robot in _robots)
        {
            PlayerFlag flag = Instantiate(_flagPrefab, robot.BotHead);
            flag.InitializeFlag(_playerIdentyDict[robot.RobotStatus.Owner]);
        }
    }
    private void InitializeDict()
    {
        foreach (Robot robot in _robots)
        {
            PlayerOwner robotIdenty = robot.RobotStatus.Owner;
            if (!_playerIdentyDict.ContainsKey(robotIdenty))
            {
                _playerIdentyDict.Add(robotIdenty, GetColorByOwner(robotIdenty));
            }
        }
    }
     
    private Color GetColorByOwner(PlayerOwner owner)
    {
        switch (owner)
        {
            case PlayerOwner.PLAYER_1:
            {
                return Color.red;
            }
            case PlayerOwner.PLAYER_2:
            {
                return Color.blue;
            }
            default:
            {
                return Color.black;
            }
        }
    }
}
