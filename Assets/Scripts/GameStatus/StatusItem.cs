using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusItem : MonoBehaviour
{
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

    [SerializeField] private List<StatusInfo> _statusCollection = new List<StatusInfo>();

    public List<StatusInfo> StatusCollection => _statusCollection;
}
