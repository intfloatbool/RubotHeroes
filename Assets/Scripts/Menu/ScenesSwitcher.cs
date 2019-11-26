using System.Collections;
using UnityEngine;

public class ScenesSwitcher : SingletonDoL<ScenesSwitcher>
{

    private Coroutine _currentSceneLoadingCoroutine;
    public void RunSceneByType(Scenes type)
    {
        StartCoroutine(LoadSceneAsync(type));
    }

    private IEnumerator LoadSceneAsync(Scenes type)
    {
        if (_currentSceneLoadingCoroutine != null)
        {
            yield break;
        }
        _currentSceneLoadingCoroutine = StartCoroutine(GameScenes.LoadSceneCoroutine(type));
        yield return _currentSceneLoadingCoroutine;
        _currentSceneLoadingCoroutine = null;
    }
    
    protected override ScenesSwitcher GetLink()
    {
        return this;
    }
}
