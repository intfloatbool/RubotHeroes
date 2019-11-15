using UnityEngine;

/// <summary>
/// Should get values from server! To avoid cheaters.
/// </summary>

[System.Serializable]
public class PlayerProperties
{
    [SerializeField] private int _energyCount = 100;

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
}
