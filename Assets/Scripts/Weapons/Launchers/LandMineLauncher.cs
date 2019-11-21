using System.Collections;
using Enums;
using UnityEngine;

public class LandMineLauncher : WeaponLauncherBase
{
    public override WeaponType WeaponType { get; }
    protected override IEnumerator OnLaunchedCoroutine(GameObject sender)
    {
        throw new System.NotImplementedException();
    }
}
