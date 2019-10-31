using System;
using System.Collections;
using UnityEngine;

public class Robot : MonoBehaviour, IRobot
{

    [SerializeField] private float _moveSpeed = 0.4f;
    [SerializeField] private float _rotSpeed = 4f;
    [SerializeField] private float _jumpStrength = 500f;
    
    //Actions which robot can do
    public bool IsCommandsRunning { get; set; }
    private Coroutine _currentAction;

    public Rigidbody Rigidbody { get; private set; }
    [SerializeField] private Robot _enemyRobot;
    [SerializeField] private Transform _botBody;
    [SerializeField] private Transform _botHead;
    private bool _isRandomMove;
    private Vector3 _randomPos;
    
    private void Awake()
    {
        this.Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {   
        Vector3 basePosByZero = new Vector3(transform.position.x, 0, transform.position.z);
        //Face head to enemy 
        Vector3 enemyTargetPos = new Vector3(_enemyRobot.transform.position.x, 0, _enemyRobot.transform.position.z);
        Vector3 relativeHeadPos = enemyTargetPos - transform.position;
        Quaternion headRotation = Quaternion.LookRotation(relativeHeadPos, Vector3.up);
        _botHead.rotation = Quaternion.Lerp(_botHead.rotation, headRotation, _rotSpeed * Time.fixedDeltaTime);

        if (_isRandomMove)
        {
            Vector3 targetPos = new Vector3(_randomPos.x, _botBody.position.y, _randomPos.z);
            Vector3 direction = (targetPos - _botBody.position).normalized;
            
            if (Vector3.Distance(_botBody.position, targetPos) <= 0.1f)
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
        
        //Rigidbody.AddForce(Vector3.down * _jumpStrength);
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        IsCommandsRunning = false;

        yield return null;
    }
    
    public IEnumerator LaunchMissleCoroutine()
    {
        Debug.Log($"Launch missle");
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        IsCommandsRunning = false;
    }
    
    public IEnumerator ProtectionShieldCoroutine()
    {
        Debug.Log($"ProtectionShieldCoroutine");
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        IsCommandsRunning = false;
    }

    public IEnumerator MeeleAttackCoroutine()
    {
        Debug.Log($"MeeleAttackCoroutine");
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        IsCommandsRunning = false;
    }

    public IEnumerator RandomMoveCoroutine()
    {
        Debug.Log($"RandomMoveCoroutine");
        _isRandomMove = true;
        _randomPos = WorldPositionsGenerator.Instance.RandomPosition;
        //TODO Complete func
        yield return new WaitForSeconds(3);
        _isRandomMove = false;
        IsCommandsRunning = false;
    } 
}
