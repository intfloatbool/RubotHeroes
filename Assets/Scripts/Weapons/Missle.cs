using System.Collections;
using Interfaces.Views;
using UnityEngine;
using Random = UnityEngine.Random;

public class Missle : BlowedObject, IColorizable
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

    private GameObject _lastTargetObj;
    private float _basicY;
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
            robot.MakeStun();
        }

        Explosion(robot ? robot.Rigidbody : null, _lastPosition);
        Destroy(this.gameObject, 2f);
    }

    private void Inverse(GameObject target)
    {
        //To avoid rotation in each frame
        if (_lastTargetObj == target)
            return;
        Vector3 detectPos = new Vector3(_lastPosition.z, transform.position.y, _lastPosition.z);
        Vector3 mirrored = Vector3.Reflect(transform.forward, detectPos);
        Vector3 posToLook = new Vector3(mirrored.x,
            _basicY,
            mirrored.z);
        transform.rotation = Quaternion.LookRotation(posToLook);
        transform.position = new Vector3(
            transform.position.x,
            _basicY,
            transform.position.z
            );
        _lastTargetObj = target;
        _randomedSpeed *= 1.1f;
    }
    
    
}
