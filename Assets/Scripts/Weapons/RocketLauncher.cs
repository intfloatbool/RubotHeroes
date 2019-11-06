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
        yield return new WaitForEndOfFrame();
        Instantiate(_misslePrefab, _sourceOfLaunch.position, _sourceOfLaunch.transform.rotation);
        Instantiate(_misslePrefab, _secondSourceOfLaunch.position, _secondSourceOfLaunch.transform.rotation);
        yield return new WaitForSeconds(1);
    }
}
