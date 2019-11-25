using System;
using Abstract;
using Interfaces.Triggers;
using UnityEngine;

[RequireComponent(typeof(Robot))]
public class RobotStatus : MonoBehaviour, IProtectable, IDamageble
{
    public GameObject GameObject => gameObject;
    private Robot _robot;
    [SerializeField] private float _healthPoints = 200;
    public float HealthPoints
    {
        get => _healthPoints;
        set => _healthPoints = value;
    }

    [SerializeField] private int _energyCount;
    public int BasicEnergyCount { get; set; }
    public int EnergyCount
    {
        get => _energyCount;
        set
        {
            _energyCount = value;
            if (_energyCount < 0)
            {
                OverLoad();
            }
            OnChargesChanged(_energyCount);
        }
    }

    public event Action<int> OnChargesChanged = (chargeCount) => { };

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
    
    public event Action<float> OnDamaged = (currHp) => { };

    private void Awake()
    {
        _robot = GetComponent<Robot>();
    }
    
    private void Start()
    {
        _robot.OnCommandExecuted += OnCommandExecuted;
    }

    private void OnDestroy()
    {
        _robot.OnCommandExecuted -= OnCommandExecuted;
    }

    void OverLoad()
    {
        if (_robot.ExternalCommand == null)
        {
            Debug.Log($" {gameObject.name} OVERLOAD!");
            _robot.ExternalCommand = new OverloadCommand(_robot);
            EnergyCount = 0;
        }
    }

    void OnCommandExecuted(RobotCommand cmd)
    {
        if (cmd is IChargable chargable)
        {
            EnergyCount -= chargable.ChargeCost;
        }
    }
    
    public void AddDamage(float dmg)
    {
        if (IsDead)
            return;
        this._healthPoints -= dmg;
        OnDamaged.Invoke(_healthPoints);
    }

    public void ResetEnergy()
    {
        EnergyCount = BasicEnergyCount;
    }

}
