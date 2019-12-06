using System;
using Interfaces.Triggers;
using UnityEngine;

public class UnitStatus : MonoBehaviour, IDamageble, IStatusable
{
    [SerializeField] protected bool _isImmortal;
    public bool IsImmortal
    {
        get => _isImmortal;
        set => _isImmortal = value;
    }

    protected Unit _unit;
    public event Action<float> OnHealthChanged = (currHp) => { };
    public event Action<StatusEffectType> OnStatusEffected = (effType) => { };
    public GameObject GameObject => gameObject;
    [SerializeField] protected float _healthPoints = 200;
    public float HealthPoints
    {
        get => _healthPoints;
        set => _healthPoints = value;
    }
    
    [SerializeField] protected bool _isDead;
    public bool IsDead
    {
        get
        {
            _isDead = _healthPoints <= 0f;
            return _isDead;
        }
    }

    protected virtual void Awake()
    {
        _unit = GetComponent<Unit>();
       if(_unit == null)
           Debug.LogError("Unit is missing!");
    }
    
    public virtual void AddDamage(float dmg)
    {
        if (_isImmortal)
            return;
        if (IsDead)
            return;
        this._healthPoints -= dmg;
        OnHealthChanged.Invoke(_healthPoints);
    }
    
    public void OnStatusEffect(StatusInfo statusEffect)
    {
        if (_isImmortal)
            return;
        
        if (statusEffect == null)
        {
            Debug.LogError($"Cannot handle status! Is null!");
            return;
        }
        StatusEffectType effectType = statusEffect.StatusEffectType;
        float statusValue = statusEffect.EffectValue;
        switch (effectType)
        {
            case StatusEffectType.STUN:
            {
                _unit.MakeStun(statusValue);
                break;
            }
            case StatusEffectType.HEAL:
            {
                throw new NotImplementedException();

                break;
            }
            case StatusEffectType.SLOW:
            {
                throw new NotImplementedException();
                break;
            }
            default:
            {
                Debug.LogError($"Cannot handle status with type {statusEffect.StatusEffectType}!");
                break;
            }
        }
    }
}
