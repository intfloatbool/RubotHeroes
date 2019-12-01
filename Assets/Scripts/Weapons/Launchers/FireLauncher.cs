﻿using System.Collections;
using Enums;
using UnityEngine;

public class FireLauncher : WeaponLauncherBase
{
    [SerializeField] private float _fixedDamage = 20f;
    [SerializeField] private float _distance = 6f;
    [SerializeField] private GameObject _flameEffect;
    public override WeaponType WeaponType { get; } = WeaponType.FIREGUN;

    protected override IEnumerator OnLaunchedCoroutine(GameObject sender)
    {
        _flameEffect.SetActive(true);
        RaycastHit hit;
        if (Physics.Raycast(_sourceOfLaunch.position, _sourceOfLaunch.forward, out hit, _distance))
        {
            Debug.DrawRay(_sourceOfLaunch.position, _sourceOfLaunch.forward, Color.red, 2f);
            RobotStatus enemyStatus = hit.collider.GetComponent<RobotStatus>();
            if (enemyStatus != null)
            {
                enemyStatus.AddDamage(_fixedDamage);
            }
        }
        
        yield return new WaitForSeconds(1);
        _flameEffect.SetActive(false);
        
    }
}
