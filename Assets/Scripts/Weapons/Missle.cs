using System;
using System.Collections;
using System.Collections.Generic;
using GameStatus.Static;
using Interfaces.GameEffects;
using Interfaces.Triggers;
using Interfaces.Views;
using UnityEngine;
using Random = UnityEngine.Random;

public class Missle : BlowedObject, IColorizable, IStatusCarrier
{

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _maxDamage = 25f;
    [SerializeField] private float _randomedDamage;
    [SerializeField] private float _maxSpeed = 7f;
    [SerializeField] private float _randomedSpeed;

    [SerializeField] private float _detectionDistance = 0.5f;
    [SerializeField] private float _lifeTime = 15f;

    private Collider _collider;
    private bool _isExploded;
    private Vector3 _lastPosition;

    private Vector3 _launchPosition;

    private GameObject _lastTargetObj;
    private float _basicY;

    public List<StatusInfo> Effects => _effects;
    private List<StatusInfo> _effects;
    
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        CalculateRandomValues();
        _basicY = transform.position.y;
    }

    public void SetColor(Color color)
    {
        foreach (Material mat in _meshRenderer.materials)
        {
            mat.SetColor("_Color", color);
        }
    }

    private IEnumerator Start()
    {
        _launchPosition = transform.position;
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
        int layerMask = 1 << 8;
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        if (Physics.SphereCast(ray, 0.3f, out hit, _detectionDistance, layerMask))
        {
            _lastPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            GameObject colGo = hit.collider.gameObject;
            if (IsShielded(colGo))
            {
                Inverse(colGo);
                return;
            }

            CheckForStatusable(colGo);

            Robot robot = hit.collider.GetComponent<Robot>();
            if (robot != null)
            {
                OnExplode(robot);
                return;
            }
            
            OnExplode(null);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
    }

    private void CheckForStatusable(GameObject target)
    {
        IStatusable statusable = target.GetComponent<IStatusable>();
        if (statusable != null)
        {
            OnCarry(statusable);
        }
    }

    private bool IsShielded(GameObject target)
    {
        bool isShielded = false;
        IProtectable protectable = target.GetComponent<IProtectable>();
        if (protectable != null)
        {
            if (protectable.IsOnShield)
            {
                isShielded = true;

                if (protectable is IProtectableContacted contacted)
                {
                    contacted.OnContact(transform.position);
                }
            }
        }
        return isShielded;
    }

    private void OnExplode(Robot robot)
    {
        _isExploded = true;
        if (robot != null)
        {
            robot.RobotStatus.AddDamage(_randomedDamage);
            //TODO: Realize status effects!
            //robot.MakeStun();
        }
        Explosion(robot != null ? robot.Rigidbody : null, _lastPosition);
        Destroy(this.gameObject, 2f);
    }

    private void Inverse(GameObject target)
    {
        //To avoid rotation in each frame
        if (_lastTargetObj == target)
            return;

        Vector3 relativePos = _launchPosition - _lastPosition;
        Quaternion rot = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rot;

    }

    public void InitializeStatusEffects(List<StatusInfo> effects)
    {
        _effects = effects;
    }

    public void OnCarry(IStatusable statusable)
    {
        bool isPossible = statusable != null && _effects != null && _effects.Count >= 0;
        if (isPossible)
        {
            this.SendStatusToTarget(statusable);
        }
    }
}
