using System;
using System.Collections;
using UnityEngine;

public class Robot : MonoBehaviour, IRobot
{
    //Actions which robot can do
    [SerializeField] private float _robotMoveSpeed = 4f;
    public bool IsCommandsRunning { get; set; }
    private Coroutine _currentAction;

    public Rigidbody Rigidbody { get; private set; }
    [SerializeField] private Robot _enemyRobot;

    private void Awake()
    {
        this.Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 relativePos = _enemyRobot.transform.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        Rigidbody.rotation = rotation;
        
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
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        IsCommandsRunning = false;
    } 
}
