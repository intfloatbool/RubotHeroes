using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Missle : MonoBehaviour
{
    [SerializeField] private float _maxDamage = 25f;
    [SerializeField] private float _randomedDamage;
    [SerializeField] private GameObject _missleBody;
    [SerializeField] private GameObject _missleExposionEffect;

    [SerializeField] private float _maxSpeed = 7f;
    [SerializeField] private float _randomedSpeed;

    [SerializeField] private float _detectionDistance = 1f;

    [SerializeField] private float _lifeTime = 15f;
    
    private bool _isExploded;
    
    private void Awake()
    {
        CalculateRandomValues();
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_lifeTime);
        OnExplode(null);
    }

    private void CalculateRandomValues()
    {
        float halfOfMaxDamage = _maxDamage / 2f;
        _randomedDamage = Random.Range(halfOfMaxDamage, _maxDamage);

        float halfOfMaxSpeed = _maxSpeed / 2f;
        _randomedSpeed = Random.Range(halfOfMaxSpeed, _maxSpeed);
    }

    private void FixedUpdate()
    {
        if (_isExploded)
            return;
        MoveLoop();
        DetectionLoop();
    }

    private void MoveLoop()
    {
        transform.Translate(Vector3.forward * _randomedSpeed * Time.fixedDeltaTime);
    }

    private void DetectionLoop()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _detectionDistance))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.collider.tag.Contains("MISSLE"))
            {
                OnExplode(null);
                return;
            }
            RobotStatus status = hit.collider.GetComponent<RobotStatus>();
            if (status != null)
            {
                OnExplode(status);
            }
            
            Debug.Log("Did Hit : " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }    

    private void OnExplode(RobotStatus robotStatus)
    {
        _isExploded = true;
        if(robotStatus != null)
            robotStatus.AddDamage(_randomedDamage);
        _missleBody.SetActive(false);
        _missleExposionEffect.SetActive(true);
        Destroy(this.gameObject, 2f);
    }
    
    
}
