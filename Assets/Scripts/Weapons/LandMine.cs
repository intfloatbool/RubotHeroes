using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces.Triggers;
using UnityEngine;


public class LandMine : TriggeringCollide
{
    [SerializeField] protected float _damage = 45f;
    [SerializeField] protected float _timeToActivate = 3f;
    [SerializeField] protected bool _isActivated;
    [SerializeField] protected float _damageRadius = 5.5f;
    [SerializeField] protected BlowedObject _blowedObject;
    protected Collider[] _collidersBuffer;

    protected virtual IEnumerator Start()
    {
        yield return new WaitForSeconds(_timeToActivate);
        _isActivated = true;
    }

    protected override void OnCollide(ICollidable collidable)
    {
        if (!_isActivated)
            return;
        
        if(_blowedObject != null)
            _blowedObject.Explosion(collidable.Rigidbody, transform.position);

        int size = Physics.OverlapSphereNonAlloc(transform.position, _damageRadius, _collidersBuffer);
        if (size > 0)
            TryDamageTargets();
    }

    protected void TryDamageTargets()
    {
        //TODO: Where is damage!?
        IEnumerable<IDamageble> damagebles = 
            _collidersBuffer.Select(t => t.GetComponent<IDamageble>());
        foreach (IDamageble damageble in damagebles)
        {
            damageble.AddDamage(_damage);
        }
    } 
}
