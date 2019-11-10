using System;
using UnityEngine;

public class Preloader : MonoBehaviour
{
    public static bool IsReady { get; private set; }
    public event Action OnGameDataReady = () => { };
    [SerializeField] private DontDestroyOnLoad[] _prefabs;

    private void Awake()
    {
        if (IsReady)
            return;
        foreach (var prefab in _prefabs)
        {
            Instantiate(prefab);
        }

        OnGameDataReady();
        IsReady = true;
    }
}
