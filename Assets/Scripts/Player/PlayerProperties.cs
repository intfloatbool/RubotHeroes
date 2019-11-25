using System.Collections.Generic;
using Enums;
using UnityEngine;
using Weapons;

/// <summary>
/// Should get values from server! To avoid cheaters.
/// </summary>

[System.Serializable]
public class PlayerProperties
{
    [SerializeField] private int _energyCount = 100;

    private Dictionary<WeaponType, WeaponID> _equipmentWeaponsDict =
        new Dictionary<WeaponType, WeaponID>()
        {
            //Defaults basic waeapons
            {WeaponType.FIREGUN, WeaponID.MA_FireThrowler},
            {WeaponType.LANDMINE, WeaponID.LS_InfantryMine},
            {WeaponType.ROCKET_LAUNCHER, WeaponID.RL_Butterfly},
        };
    
    public int EnergyCount
    {
        get => _energyCount;
        set => _energyCount = value;
    }

    [SerializeField] private float _healthPoints = 200;
    public float HealthPoints
    {
        get => _healthPoints;
        set => _healthPoints = value;
    }

    public void ChangeWeaponByType(WeaponType type, WeaponID weaponID)
    {
        if (_equipmentWeaponsDict.ContainsKey(type))
        {
            _equipmentWeaponsDict[type] = weaponID;
        }
        else
        {
            _equipmentWeaponsDict.Add(type, weaponID);
        }
    }

    public WeaponID GetWeaponByType(WeaponType type)
    {
        if (_equipmentWeaponsDict.ContainsKey(type))
        {
            return _equipmentWeaponsDict[type];
        }
        else
        {
            Debug.Log($"Weapon with type: {type} not exists in player!");
            return WeaponID.UNDEFINED;
        }
    } 
}
