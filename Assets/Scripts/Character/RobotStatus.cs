﻿using System;
using UnityEngine;

public class RobotStatus : MonoBehaviour
{
    [SerializeField] private float _healthPoints = 100;

    public float HealthPoints => _healthPoints;

    [SerializeField] private bool _isOnShield;

    public bool IsOnShield
    {
        get => _isOnShield;
        set => _isOnShield = value;
    }

    [SerializeField] private bool _isDead;

    public bool IsDead
    {
        get
        {
            _isDead = _healthPoints <= 0f;
            return _isDead;
        }
    }

    public event Action OnDamaged = () => { };

    public void AddDamage(float dmg)
    {
        this._healthPoints -= dmg;
        OnDamaged.Invoke();
    }

}