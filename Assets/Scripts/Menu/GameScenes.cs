using System;
using UnityEngine.SceneManagement;

public static class GameScenes
{
    public static Action<Scenes> OnSceneChanged = (scene) => { };
    public static void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene((int) scene);
        OnSceneChanged(scene);
    }
}
