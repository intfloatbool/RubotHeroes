using System.Collections;
using UnityEngine;

public class Robot : MonoBehaviour, IRobot
{
    //Actions which robot can do
    public bool IsCommandsRunning { get; set; }
    private Coroutine _currentAction;
    public void Jump()
    {
        StopActionIfExists();
        _currentAction = StartCoroutine(JumpCoroutine());
    }

    public void MoveY(float value)
    {
        StopActionIfExists();
        _currentAction = StartCoroutine(MoveYCoroutine(value));
    }

    public void MoveX(float value)
    {
        StopActionIfExists();
        _currentAction = StartCoroutine(MoveXCoroutine(value));
    }

    public void LaunchMissle(float offset)
    {
        StopActionIfExists();
        _currentAction = StartCoroutine(LaunchMissleCoroutine(offset));
    }

    public void GunAttack(float offset)
    {
        StopActionIfExists();
        _currentAction = StartCoroutine(GunAttackCoroutine(offset));
    }

    public void RotateX(float value)
    {
        StopActionIfExists();
        _currentAction = StartCoroutine(RotateXCoroutine(value));
    }

    public void RotateY(float value)
    {
        StopActionIfExists();
        _currentAction = StartCoroutine(RotateYCoroutine(value));
    }

    public void LookAtEnemy()
    {
        StopActionIfExists();
        _currentAction = StartCoroutine(LookAtEnemyCoroutine());
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

    public IEnumerator MoveYCoroutine(float value)
    {
        Debug.Log($"Move Y to {value}");
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        IsCommandsRunning = false;
    }
    
    public IEnumerator MoveXCoroutine(float value)
    {
        Debug.Log($"Move X to {value}");
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        IsCommandsRunning = false;
    }

    public IEnumerator LaunchMissleCoroutine(float offset)
    {
        Debug.Log($"Launch missle with offset: {offset}");
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        IsCommandsRunning = false;
    }

    public IEnumerator GunAttackCoroutine(float offset)
    {
        Debug.Log($"Gun attack with offset: {offset}");
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        IsCommandsRunning = false;
    }

    public IEnumerator RotateXCoroutine(float value)
    {
        Debug.Log($"Rotate X to {value}");
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        IsCommandsRunning = false;
    }
    
    public IEnumerator RotateYCoroutine(float value)
    {
        Debug.Log($"Rotate Y to {value}");
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        IsCommandsRunning = false;
    }

    public IEnumerator LookAtEnemyCoroutine()
    {
        Debug.Log($"Look at enemy!");
        
        //TODO Complete func
        yield return new WaitForSeconds(1);
        IsCommandsRunning = false;
    }
}
