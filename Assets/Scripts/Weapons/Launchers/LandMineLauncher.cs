using System.Collections;
using Enums;
using UnityEngine;

public class LandMineLauncher : WeaponLauncherBase
{
    [SerializeField] private Vector3 _launchForce = Vector3.forward * 20;
    [SerializeField] private float _plantDelay = 1.0f;
    [SerializeField] private LandMine _landMinePrefab;
    public override WeaponType WeaponType { get; } = WeaponType.LANDMINE;

    [SerializeField] private float _timeToBlow = 0;
    public float TimeToBlow
    {
        get => _timeToBlow;
        set => _timeToBlow = value;
    }

    protected override IEnumerator OnLaunchedCoroutine(GameObject sender)
    {
        yield return new WaitForSeconds(_plantDelay);
        PlantLandMine();
        yield return new WaitForSeconds(_plantDelay);
    }

    private void PlantLandMine()
    {
        LandMine mine = Instantiate(_landMinePrefab);
        if (_timeToBlow > 0)
        {
            mine.SetTimerForBlow(_timeToBlow);
        }
        mine.transform.rotation = _sourceOfLaunch.rotation;
        mine.transform.position = _sourceOfLaunch.position;
        mine.Rigidbody.AddForce(_sourceOfLaunch.forward 
                                * _launchForce.magnitude *
                                _landMinePrefab.Rigidbody.mass);
        Colorize(mine);
    }
}
