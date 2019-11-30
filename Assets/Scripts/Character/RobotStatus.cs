using System;
using Abstract;
using Interfaces.Triggers;
using UnityEngine;

[RequireComponent(typeof(Robot))]
public class RobotStatus : UnitStatus, IProtectable
{
    private Robot _robot;

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

    protected override void Awake()
    {
        base.Awake();
        _robot = _unit as Robot;
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

    public void ResetEnergy()
    {
        EnergyCount = BasicEnergyCount;
    }

}
