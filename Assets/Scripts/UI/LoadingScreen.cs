using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadingScreen : SingletonDoL<LoadingScreen>
{
    [SerializeField] private Scenes _startingSceneType;
    [SerializeField] private float _delayAfterLoading = 1f;
    [SerializeField] private LoadingScreenView _defaultLoadingPreset;
    [SerializeField] private bool _isInitializeInStart = true;

    private Coroutine _currentLoadingCoroutine;
    
    [System.Serializable]
    private class LoadingScreenPreset
    {
        public Scenes SceneType;
        public LoadingScreenView LoadingScreenObject;
    }

    [SerializeField] private List<LoadingScreenPreset> _presets;
    
    protected override LoadingScreen GetLink() => this;

    protected override void Awake()
    {
        base.Awake();
        DeactivateAllPresets();
        GameScenes.OsSceneStartingLoaded += OnScenesSwitched;
    }

    protected void Start()
    {
        if(_isInitializeInStart)
            OnScenesSwitched(_startingSceneType);
    }

    private void OnScenesSwitched(Scenes sceneType)
    {
        if (_currentLoadingCoroutine != null)
            return;
        
        LoadingScreenPreset presetByType = _presets.FirstOrDefault(p => 
            p.SceneType == sceneType && p.LoadingScreenObject != null);
        LoadingScreenView presetToActivate = presetByType != null ? presetByType.LoadingScreenObject : _defaultLoadingPreset;
        presetToActivate.gameObject.SetActive(true);
        
        _currentLoadingCoroutine = StartCoroutine(LoadingScreenCoroutine(presetToActivate));
    }

    private void DeactivateAllPresets()
    {
        _presets.ForEach(p => p?.LoadingScreenObject?.gameObject.SetActive(false));
        _defaultLoadingPreset?.gameObject.SetActive(false);
    }

    private IEnumerator LoadingScreenCoroutine(LoadingScreenView view)
    {
        view.ReachedValue = 0.3f;
        while (!GameScenes.IsLastSceneLoaded)
        {
            view.ReachedValue += Time.deltaTime;
            yield return null;
        }

        view.ReachedValue = 1f;
        
        yield return new WaitForSeconds(_delayAfterLoading);
        DeactivateAllPresets();
        _currentLoadingCoroutine = null;
    }
}
