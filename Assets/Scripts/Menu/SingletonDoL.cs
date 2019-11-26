using UnityEngine;

public  abstract class SingletonDoL<T> : DontDestroyOnLoadMB
{
    public static T Instance { get; protected set; }

    /// <summary>
    /// Must calling to returns self instance for singleton initializing
    /// </summary>
    /// <returns>this link</returns>
    protected abstract T GetLink();
    protected override void Awake()
    {
        if (Instance == null)
            Instance = GetLink();
        base.Awake();
    }
}
