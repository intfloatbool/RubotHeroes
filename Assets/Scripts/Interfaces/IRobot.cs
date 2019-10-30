using System.Collections;
using System.Collections.Generic;

public interface IRobot
{
    bool IsCommandsRunning { get; set; }
    void Jump();

    void MoveY(float value);

    void MoveX(float value);

    void LaunchMissle(float offset);

    void GunAttack(float offset);

    void RotateX(float value);

    void RotateY(float value);

    void LookAtEnemy();
}
