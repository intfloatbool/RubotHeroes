using UnityEngine;

[System.Serializable]
public class StatusInfo
{
    [SerializeField]
    private StatusEffectType _statusEffectType;
    public StatusEffectType StatusEffectType => _statusEffectType;
    
    [SerializeField]
    private float _effectValue;
    public float EffectValue => _effectValue;
}