using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesSwitcher : MonoBehaviour
{
    public void GoToBattleScene()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMenuScene()
    {
        SceneManager.LoadScene(0);

    }

    public void GoToEditCommandsScene()
    {
        SceneManager.LoadScene(2);

    }
}
