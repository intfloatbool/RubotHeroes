using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Abstract;
using Enums;
using Interfaces;
using Interfaces.Triggers;
using UnityEngine;
using Weapons;

[RequireComponent(typeof(RobotStatus))]
public class Robot : Unit, IDeadable,
    IPlayer, ICollidable, ICommandExecutor, IAudioPlayable
{
    [SerializeField] private Transform _weaponsParent;
    [SerializeField] private AudioSource _audioSource;
    public AudioSource AudioSource => _audioSource;

    [SerializeField] private List<InvokedActionByInitializeBase> _actionsOnStart;
    public event Action OnDeath = () => { };
    public event Action<RobotCommand> OnCommandExecuted = (cmd) => {};
    public event Action<CommandType> OnCommandTypeExecuted = (cmdType) => {};
    public event Action<WeaponID, WeaponType> OnWeaponChanged = (weaponID, weaponType) => { };

    [SerializeField] private float _moveSpeed = 0.4f;
    [SerializeField] private float _rotSpeed = 4f;

    private RobotStatus _robotStatus;
    public RobotStatus RobotStatus => _robotStatus;

    [SerializeField] private float _distanceFromDestiny;
    public virtual float DistanceFromDestiny
    {
        get => _distanceFromDestiny;
        set => _distanceFromDestiny = value;
    }

    //Actions which robot can do
    public bool IsCommandsRunning { get; set; }
    private Coroutine _currentAction;

    public Color RobotColor { get; private set; } = Color.white;

    public Rigidbody Rigidbody { get; private set; }
    private Collider _collider;
    [SerializeField] private Robot _enemyRobot;
    public Robot EnemyRobot => _enemyRobot;
    
    [SerializeField] private Transform _botBody;
    [SerializeField] private Transform _botHead;

    [SerializeField] private GameObject _shieldEffect;
    public GameObject ShieldEffect => _shieldEffect;
    public Transform BotHead => _botHead;
    private bool _isRandomMove;
    
    public GameObject TargetToMove { get; set; }
    
    [SerializeField] private float _pathDistanceMinValue = 1f;
    private bool _isDestinationReach = false;
    public bool IsDestinationReach => _isDestinationReach;

    private CommandType _currentCommand;
    public CommandType CurrentCommand => _currentCommand;
    public float DistanceToEnemy
    {
        get
        {
            float distance = _enemyRobot != null
                ? Vector3.Distance(transform.position,
                    _enemyRobot.transform.position)
                : 0f;
            return distance;
        }
    }
    
    public bool IsRandomMove
    {
        get => _isRandomMove;
        set => _isRandomMove = value;
    }

    [SerializeField] private Vector3 _randomPos;
    public Vector3 RandomPos
    {
        get => _randomPos;
        set => _randomPos = value;
    }
    
    private Dictionary<WeaponType, WeaponLauncherBase> _weaponsDict = new Dictionary<WeaponType, WeaponLauncherBase>();
    public RobotCommand ExternalCommand { get; set; }

    public bool IsMoving => _isRandomMove || TargetToMove != null;
    
    protected virtual void Awake()
    {
        this._robotStatus = GetComponent<RobotStatus>();
        this.Rigidbody = GetComponent<Rigidbody>();
        this._collider = GetComponent<Collider>();
        this._robotStatus.OnHealthChanged += DeadControlListening;
        
    }

    public void SetWeapons(WeaponID[] weaponIdentifiers)
    {
        foreach (WeaponID weaponId in weaponIdentifiers)
        {
            WeaponContainer container = WeaponsHolder.Instance.GetWeaponContainerByID(weaponId);
            WeaponLauncherBase launcherPrefab = container.LauncherPrefab;
            WeaponType weaponType = container.WeaponType;
            if (!_weaponsDict.Keys.Contains(weaponType))
            {
                WeaponLauncherBase weaponInstance = Instantiate(launcherPrefab, _weaponsParent);
                _weaponsDict.Add(container.WeaponType, weaponInstance);
                weaponInstance.SetOwner(this);
            }
            else
            {
                Debug.Log($"Robot already have weapon {weaponType}! Destroy old!");
                Destroy(_weaponsDict[weaponType].gameObject);
                WeaponLauncherBase weaponInstance = Instantiate(launcherPrefab, _weaponsParent);
                _weaponsDict[weaponType] = weaponInstance;
                weaponInstance.SetOwner(this);
            }

            OnWeaponChanged(weaponId, weaponType);
        }
    }

    public WeaponLauncherBase GetWeaponByType(WeaponType weaponType)
    {
        if (_weaponsDict.Keys.Contains(weaponType))
        {
            return _weaponsDict[weaponType];
        }
  
        return null;
    }

    public void InitializeRobotStatus(PlayerProperties properties)
    {
        _robotStatus.HealthPoints = properties.HealthPoints;
        _robotStatus.EnergyCount = properties.EnergyCount;
        _robotStatus.BasicEnergyCount = properties.EnergyCount;
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
        try
        {
            _collider.enabled = false;
        }
        catch (Exception ex)
        {
            Debug.LogError($"{ex.Message} \n {ex.StackTrace}");
        }
    }

    protected virtual void FixedUpdate()
    {
        if (_isStunned)
            return;
        
        if (_robotStatus.IsDead)
            return;
        
        //Face head to enemy 
        if ( _enemyRobot != null && !_enemyRobot._robotStatus.IsDead)
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

        //avoid moving when stun
        if (_isStunned)
            return;
        
        if (TargetToMove != null)
        {
            MoveToTarget(TargetToMove.transform.position);
            return;
        }
        
        if (_isRandomMove)
        {
            MoveToPoint(_randomPos);
        }
        
    }

    private void MoveToPoint(Vector3 target)
    {
        Vector3 targetPos = new Vector3(target.x, _botBody.position.y, target.z);
        _distanceFromDestiny = Vector3.Distance(_botBody.position, targetPos);
        _isDestinationReach = _distanceFromDestiny <= _pathDistanceMinValue;
        if (_isDestinationReach)
        {
            return;
        }
        
        MoveLoop(targetPos);
    }

    private void MoveToTarget(Vector3 target)
    {
        Vector3 targetPos = new Vector3(target.x, _botBody.position.y, target.z);
        MoveLoop(targetPos);
    }

    private void MoveLoop(Vector3 target)
    {
        
        Vector3 direction = (target - _botBody.position).normalized;
        
        MoveRobotByDirection(direction);
        
        //face bot body to position
        Vector3 relativeBodyPos = target - transform.position;
        Quaternion bodyRotation = Quaternion.LookRotation(relativeBodyPos, Vector3.up);
        _botBody.rotation = Quaternion.Lerp(_botBody.rotation, bodyRotation, _rotSpeed * Time.fixedDeltaTime);
    }

    private void MoveRobotByDirection(Vector3 direction)
    {
        Rigidbody.MovePosition(transform.position + direction * _moveSpeed * Time.fixedDeltaTime); 
    }
    
    private void StopActionIfExists()
    {
        if (this._currentAction != null)
        {
            StopCoroutine(_currentAction);
            _currentAction = null;
        }
    }

    public void SetCurrentCommand(IEnumerator commandEnumerator, RobotCommand cmd)
    {
        StopActionIfExists();
        this._currentAction = StartCoroutine(commandEnumerator);
        OnCommandExecuted(cmd);
        _currentCommand = cmd.CommandType;
    }

    public void NotifyThatCommandRun(CommandType cmdType)
    {
        OnCommandTypeExecuted(cmdType);
    }
    
    public void ResetCommandsRunning()
    {
        IsCommandsRunning = false;
    }

    
    
    public void Initialize(Player player)
    {
        RobotColor = player.Color;
        foreach (var action in _actionsOnStart)
        {
            action.OnInitialized(player);
        }
    }
}
