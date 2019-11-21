using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class RocketLauncher : WeaponLauncherBase
{
    [SerializeField] private Transform _secondSourceOfLaunch;
    [SerializeField] private Missle _misslePrefab;
    public override WeaponType WeaponType { get; } = WeaponType.ROCKET_LAUNCHER;

    protected override IEnumerator OnLaunchedCoroutine(GameObject sender)
    {
        Robot robotOwner = sender.GetComponent<Robot>();
        yield return new WaitForSeconds(1);
        LaunchMissle(_sourceOfLaunch, robotOwner);
        LaunchMissle(_secondSourceOfLaunch, robotOwner);
        yield return new WaitForSeconds(1);
    }

    private void LaunchMissle(Transform source, Robot owner = null)
    {
        Missle missle = Instantiate(_misslePrefab, source.position, source.rotation);
        if (owner != null)
        {
            missle.SetColor(owner.RobotColor);
        }
    }
}
