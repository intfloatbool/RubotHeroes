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
    public Robot EnemyRobot => _enemyRobot;
    
    [SerializeField] private Transform _botBody;
    [SerializeField] private Transform _botHead;
    [SerializeField] private GameObject _shieldEffect;

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
        SphereCollider col = (SphereCollider) this._collider;
        col.radius /= 3f;
    }

    private void FixedUpdate()
    {
        if (_robotStatus.IsDead)
            return;
        
        //Face head to enemy 
        if (!_enemyRobot._robotStatus.IsDead)
        {
            Vector3 enemyTargetPos = new Vector3(_enemyRobot.transform.position.x, transform.position.y, _enemyRobot.transform.position.z);
            Vector3 relativeHeadPos = enemyTargetPos - transform.position;
            Quaternion headRotation = Quaternion.LookRotation(relativeHeadPos, Vector3.up);
            _botHead.rotation = Quaternion.Lerp(_botHead.rotation, headRotation, _rotSpeed * Time.fixedDeltaTime);
        }
        else
        {
            //Rotate a robot head becouse the enemy is dead and its funny!!
            _botHead.Rotate(Vector3.up * (_rotSpeed * 50f) * Time.deltaTime);
        }
        
        if (_isRandomMove)
        {
            if (_isStunned)
                return;

            MoveLoop(_randomPos);
        }
        
    }

    private void MoveLoop(Vector3 target)
    {
        Vector3 targetPos = new Vector3(target.x, _botBody.position.y, target.z);
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
        _robotStatus.IsOnShield = true;
        _shieldEffect.SetActive(true);
        yield return new WaitForSeconds(2);
        _shieldEffect.SetActive(false);
        _robotStatus.IsOnShield = false;
        ResetCommandsRunning();
    }

    public IEnumerator MeeleAttackCoroutine()
    {
        _distanceFromDestiny = 2.5f;
        while (_distanceFromDestiny >= 2.5f)
        {
            MoveLoop(EnemyRobot.transform.position);
            yield return null;
        }
        
        _fireGun.LaunchWeapon(gameObject);
        while (_fireGun.IsInProcess)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        ResetCommandsRunning();
    }

    public IEnumerator RandomMoveCoroutine()
    {
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
