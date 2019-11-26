using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameScenes
{
    private static float _delayOfLoading = 1f;
    public static bool IsLastSceneLoaded { get; private set; }
    public static Action<Scenes> OsSceneStartingLoaded = (scene) => { };
    public static Action<Scenes> OnSceneLoaded = (scene) => { };
    public static IEnumerator LoadSceneCoroutine(Scenes scene)
    {
        IsLastSceneLoaded = false;
        OsSceneStartingLoaded(scene);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync((int) scene);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
        
        yield return new WaitForSeconds(_delayOfLoading);

        OnSceneLoaded(scene);
        IsLastSceneLoaded = true;
    }
}
