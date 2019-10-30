using UnityEngine;

public class Robot : MonoBehaviour, IRobot
{
    //Actions which robot can do
    public void Jump()
    {
        Debug.Log("Jump!");
    }

    public void MoveY(float value)
    {
        Debug.Log($"Move Y to {value}");
    }
    
    public void MoveX(float value)
    {
        Debug.Log($"Move X to {value}");
    }

    public void LaunchMissle(float offset)
    {
        Debug.Log($"Launch missle with offset: {offset}");
    }

    public void GunAttack(float offset)
    {
        Debug.Log($"Gun attack with offset: {offset}");
    }

    public void RotateX(float value)
    {
        Debug.Log($"Rotate X to {value}");
    }
    
    public void RotateY(float value)
    {
        Debug.Log($"Rotate Y to {value}");
    }

    public void LookAtEnemy()
    {
        Debug.Log($"Look at enemy!");
    }
}
