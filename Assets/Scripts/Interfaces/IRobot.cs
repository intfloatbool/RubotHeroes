using System.Collections;
using System.Collections.Generic;

public interface IRobot
{
    bool IsCommandsRunning { get; set; }
    void Jump();
    void LaunchMissle();
    void ProtectionShield();
    void MeeleAttack();
    void RandomMove();

}
