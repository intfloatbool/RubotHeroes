
using System.Collections.Generic;
using UnityEngine;

public class GameCollidersDeactivator : OnGameEndTrigger
{
    [SerializeField] private List<Collider> _collidersDeactivator;
    protected override void OnGameEnd(RobotCommandRunner winner)
    {
        foreach (Collider col in _collidersDeactivator)
            col.enabled = false;
    }
}
