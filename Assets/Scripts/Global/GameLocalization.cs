using UnityEngine;

public class GameLocalization : SingletonDoL<GameLocalization>
{
    public static string GetLocalization(string key)
    {
        return GameLocalization.Instance.Get(key);
    }

    protected override GameLocalization GetLink() => this;

    private string Get(string key)
    {
        //TODO realize localization!
        return key;
    }
}
