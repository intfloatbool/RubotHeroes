using System.Collections;
using Enums;
using UnityEngine;

public class LandMineLauncher : WeaponLauncherBase
{
    [SerializeField] private float _plantDelay = 1.5f;
    [SerializeField] private LandMine _landMinePrefab;
    public override WeaponType WeaponType { get; } = WeaponType.LANDMINE;
    protected override IEnumerator OnLaunchedCoroutine(GameObject sender)
    {
        yield return new WaitForSeconds(_plantDelay);
        PlantLandMine();
        yield return new WaitForSeconds(_plantDelay);
    }

    private void PlantLandMine()
    {
        LandMine mine = Instantiate(_landMinePrefab);
        mine.transform.position = _sourceOfLaunch.position;
        Colorize(mine);
    }
}
