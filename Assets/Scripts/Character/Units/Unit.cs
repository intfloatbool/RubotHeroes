using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected bool _isStunned = false;
    public bool IsStunned => _isStunned;
    
    [SerializeField] protected UnitStatus _unitStatus;
    public UnitStatus UnitStatus => _unitStatus;

    protected virtual void Awake()
    {
        if (_unitStatus == null)
        {
            _unitStatus = GetComponent<UnitStatus>();
            if(_unitStatus == null)
                Debug.LogError("Unit status MISSING!");
        }
    }
    
    public virtual void MakeStun(float timeToStun)
    {
        if (!_isStunned)
            StartCoroutine(MakeStunCoroutine(timeToStun));
    }

    protected virtual IEnumerator MakeStunCoroutine(float timeToStun)
    {
        _isStunned = true;
        yield return new WaitForSeconds(timeToStun);
        _isStunned = false;
    }
    
}
