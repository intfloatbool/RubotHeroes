public class ScenesSwitcher : SingletonDoL<ScenesSwitcher>
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

    protected override ScenesSwitcher GetLink()
    {
        return this;
    }
}
