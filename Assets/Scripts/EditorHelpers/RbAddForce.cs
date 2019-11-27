using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RbAddForce : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _strength = 500f;
    [SerializeField] private Vector3 _forcedVector = Vector3.forward;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void AddForce()
    {
        _rb.AddForce(_forcedVector * _strength);
    }
    
    
}
