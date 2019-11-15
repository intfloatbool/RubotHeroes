using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [SerializeField] private string _nickName;
    public string NickName
    {
        get => _nickName;
        set => _nickName = value;
    }

    [SerializeField] private Color _color;
    public Color Color
    {
        get => _color;
        set => _color = value;
    }

    [SerializeField] private PlayerProperties _playerProperties;
    public PlayerProperties Properties
    {
        get => _playerProperties;
        set => _playerProperties = value;
    }

}
