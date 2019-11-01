using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : WeaponLauncherBase
{
    [SerializeField] private Transform _secondSourceOfLaunch;
    [SerializeField] private Missle _misslePrefab;
    protected override IEnumerator OnLaunchedCoroutine(GameObject sender)
    {
        yield return new WaitForSeconds(1);
        Instantiate(_misslePrefab, _sourceOfLaunch.position, _sourceOfLaunch.transform.rotation);
        Instantiate(_misslePrefab, _secondSourceOfLaunch.position, _secondSourceOfLaunch.transform.rotation);
        yield return new WaitForSeconds(1);
    }
}
