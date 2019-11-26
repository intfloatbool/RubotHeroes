using System;
using System.Collections;
using UnityEngine;

public class Preloader : SingletonDoL<Preloader>
{
    [SerializeField] private float _delayAfterPreloading = 2f;
    [SerializeField] private Scenes _sceneAfterLoad = Scenes.MAIN_MENU;
    public static bool IsReady { get; private set; }
    public event Action OnGameDataReady = () => { };
    [SerializeField] private DontDestroyOnLoadMB[] _prefabs;

    protected override Preloader GetLink()
    {
        return this;
    }

    protected void Start()
    {
        if (IsReady)
            return;
        StartCoroutine(LoadAllPrefabs());

        OnGameDataReady += TryLoadFirstScene;
    }

    IEnumerator LoadAllPrefabs()
    {
        foreach (var prefab in _prefabs)
        {
            Instantiate(prefab);
        }

        yield return new WaitForSeconds(_delayAfterPreloading);
        OnGameDataReady();
        IsReady = true;
    }

    private void TryLoadFirstScene()
    {
        ScenesSwitcher switcherInstance = ScenesSwitcher.Instance;
        if (switcherInstance != null)
        {
            switcherInstance.RunSceneByType(_sceneAfterLoad);
        }
        else
        {
            Debug.LogError("Cannot start first scene! SceneSwitcher not found!");
        }
    }
    
}
