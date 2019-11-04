using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RobotStatus))]
public class Robot : MonoBehaviour, IRobot, IDeadable
{
    public event Action OnDeath = () => { };

    [SerializeField] private float _moveSpeed = 0.4f;
    [SerializeField] private float _rotSpeed = 4f;
    [SerializeField] private float _jumpStrength = 500f;

    private RobotStatus _robotStatus;
    public RobotStatus RobotStatus => _robotStatus;

    [SerializeField] private float _distanceFromDestiny;
    
    //Actions which robot can do
    public bool IsCommandsRunning { get; set; }
    private Coroutine _currentAction;

    public Rigidbody Rigidbody { get; private set; }
    private Collider _collider;
    [SerializeField] private Robot _enemyRobot;
    [SerializeField] private Transform _botBody;
    [SerializeField] private Transform _botHead;

    public Transform BotHead => _botHead;

    private bool _isRandomMove;
    private Vector3 _randomPos;

    [SerializeField] private WeaponLauncherBase _rocketLauncher;
    [SerializeField] private WeaponLauncherBase _fireGun;

    private bool _isStunned = false;

    public bool IsLanding => Mathf.Approximately(Rigidbody.velocity.y, 0f);

    private void Awake()
    {
        this._robotStatus = GetComponent<RobotStatus>();
        this.Rigidbody = GetComponent<Rigidbody>();
        this._collider = GetComponent<Collider>();
        this._robotStatus.OnDamaged += DeadControlListening;
    }

    private void DeadControlListening(float currentHP)
    {
        if (this._robotStatus.IsDead)
        {
            OnDead();
            OnDeath.Invoke();
        }
    }

    private void OnDead()
    {
        StopActionIfExists();
        this._collider.enabled = false;

    }

    private void FixedUpdate()
    {
        if (_robotStatus.IsDead)
            return;
        
        //Face head to enemy 
        Vector3 enemyTargetPos = new Vector3(_enemyRobot.transform.position.x, transform.position.y, _enemyRobot.transform.position.z);
        Vector3 relativeHeadPos = enemyTargetPos - transform.position;
        Quaternion headRotation = Quaternion.LookRotation(relativeHeadPos, Vector3.up);
        _botHead.rotation = Quaternion.Lerp(_botHead.rotation, headRotation, _rotSpeed * Time.fixedDeltaTime);

        if (_isRandomMove)
        {
            if (_isStunned)
                return;
            
            Vector3 targetPos = new Vector3(_randomPos.x, _botBody.position.y, _randomPos.z);
            Vector3 direction = (targetPos - _botBody.position).normalized;
            _distanceFromDestiny = Vector3.Distance(_botBody.position, targetPos);
            if (_distanceFromDestiny <= 0.5f)
            {
                return;
            }
            
            Rigidbody.velocity = direction * _moveSpeed * Time.fixedDeltaTime;

            //face bot body to position
            Vector3 relativeBodyPos = targetPos - transform.position;
            Quaternion bodyRotation = Quaternion.LookRotation(relativeBodyPos, Vector3.up);
            _botBody.rotation = Quaternion.Lerp(_botBody.rotation, bodyRotation, _rotSpeed * Time.fixedDeltaTime);
        }
        
    }

    //*** COMMANDS ***
    public void Jump()
    {
        StopActionIfExists();
        _currentAction = StartCoroutine(JumpCoroutine());
    }
    

    public void LaunchMissle()
    {
        StopActionIfExists();
        _currentAction = StartCoroutine(LaunchMissleCoroutine());
    }

    public void ProtectionShield()
    {
        StopActionIfExists();
        _currentAction = StartCoroutine(ProtectionShieldCoroutine());
    }

    public void MeeleAttack()
    {
        StopActionIfExists();
        _currentAction = StartCoroutine(MeeleAttackCoroutine());
    }

    public void RandomMove()
    {
        StopActionIfExists();
        _currentAction = StartCoroutine(RandomMoveCoroutine());
    }


    private void StopActionIfExists()
    {
        if (this._currentAction != null)
        {
            StopCoroutine(_currentAction);
            _currentAction = null;
        }
    }
    
    // * * * COROUTINES * * *
    public IEnumerator JumpCoroutine()
    {
        Debug.Log("Jump!");
        
        Rigidbody.AddForce(Vector3.up * _jumpStrength);
        
        //TODO Complete func
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();

        while (!Mathf.Approximately(Rigidbody.velocity.y, 0f))
        {
            yield return null;
        }

        while (_isStunned)
        {
            yield return null;
        }
        
        yield return new WaitForFixedUpdate();
        yield return new WaitForSeconds(1);
        IsCommandsRunning = false;

        yield return null;
    }
    
    public IEnumerator LaunchMissleCoroutine()
    {
        Debug.Log($"Launch missle");
        
        _rocketLauncher.LaunchWeapon(gameObject);
        while (_rocketLauncher.IsInProcess)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        ResetCommandsRunning();
    }
    
    public IEnumerator ProtectionShieldCoroutine()
    {
        Debug.Log($"ProtectionShieldCoroutine");
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        ResetCommandsRunning();
    }

    public IEnumerator MeeleAttackCoroutine()
    {
        Debug.Log($"MeeleAttackCoroutine");
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        ResetCommandsRunning();
    }

    public IEnumerator RandomMoveCoroutine()
    {
        Debug.Log($"RandomMoveCoroutine");
        _isRandomMove = true;
        _randomPos = WorldPositionsGenerator.Instance.RandomPosition;
        //TODO Complete func
        yield return new WaitForSeconds(3);
        _isRandomMove = false;
        ResetCommandsRunning();
    }


    private void ResetCommandsRunning()
    {
        IsCommandsRunning = false;
    }

    public void MakeStun()
    {
        if (!_isStunned)
            StartCoroutine(MakeStunCoroutine());
    }

    private IEnumerator MakeStunCoroutine()
    {
        _isStunned = true;
        float time = 2f;
        yield return new WaitForSeconds(time);
        _isStunned = false;
    }
}
