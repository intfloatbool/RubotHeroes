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

    [SerializeField] private float _detectionDistance = 0.5f;

    [SerializeField] private float _lifeTime = 15f;

    [SerializeField] private float _explodePower = 60f;
    private Collider _collider;
    private bool _isExploded;

    private Vector3 _lastPosition;
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
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
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.SphereCast(ray, 0.3f, out hit, _detectionDistance))
        {
            _lastPosition = hit.point;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.collider.tag.Contains("MISSLE"))
            {
                Missle crossMissle = hit.collider.GetComponent<Missle>();
                
                crossMissle.OnExplode(null);
                OnExplode(null);
                return;
            }
            Robot robot = hit.collider.GetComponent<Robot>();
            if (robot != null)
            {
                OnExplode(robot);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }    

    private void OnExplode(Robot robot)
    {
        _collider.enabled = false;
        _isExploded = true;
        if (robot != null)
        {
            robot.RobotStatus.AddDamage(_randomedDamage);
            Vector3 relativePos = _lastPosition - transform.position;
            Vector3 positionToForce = new Vector3(transform.position.x, robot.transform.position.y, transform.position.z).normalized;
            robot.MakeStun();
            robot.Rigidbody.AddForce(relativePos.normalized * _explodePower * _randomedDamage);
        }
            
        _missleBody.SetActive(false);
        _missleExposionEffect.SetActive(true);
        Destroy(this.gameObject, 2f);
    }
    
    
}
