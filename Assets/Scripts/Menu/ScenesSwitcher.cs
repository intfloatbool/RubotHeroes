using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesSwitcher : MonoBehaviour
{
    
    public void GoToBattleScene()
    {
        GameScenes.LoadScene(Scenes.BATTLE_SCENE);
    }

    public void GoToMenuScene()
    {
        GameScenes.LoadScene(Scenes.MAIN_MENU);
    }

    public void GoToEditCommandsScene()
    {
        GameScenes.LoadScene(Scenes.COMMANDS_CONSTRUCTOR);
    }
}
