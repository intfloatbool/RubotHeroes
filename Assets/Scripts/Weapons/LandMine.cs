using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces.Triggers;
using Interfaces.Views;
using UnityEngine;


public class LandMine : TriggeringCollide, IColorizable
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] protected float _damage = 45f;
    [SerializeField] protected float _timeToActivate = 3f;
    [SerializeField] protected bool _isActivated;
    [SerializeField] protected float _damageRadius = 2.5f;
    [SerializeField] protected BlowedObject _blowedObject;
    protected Collider[] _collidersBuffer = new Collider[10];

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
        int layerMask = 1 << 8;
        
        int size = Physics.OverlapSphereNonAlloc(transform.position, _damageRadius, _collidersBuffer, layerMask);
        if (size > 0)
            TryDamageTargets();
    }

    protected void TryDamageTargets()
    {
        //TODO: Where is damage!?
        IEnumerable<IDamageble> damagebles =
            _collidersBuffer
                .Where(e => e != null)
                .Select(t => t.GetComponent<IDamageble>())
                .Distinct();
                
        foreach (IDamageble damageble in damagebles)
        {
            if(damageble == null)
                continue;
            damageble.AddDamage(_damage);
            Debug.Log($"MINE DAMAGE! to {damageble.GameObject.name}");
        }

        _isActivated = false;
    }

    public void SetColor(Color color)
    {
        if (_meshRenderer == null)
            return;
        foreach (Material mat in _meshRenderer.materials)
        {
            mat.SetColor("_Color", color);
        }
    }
}
