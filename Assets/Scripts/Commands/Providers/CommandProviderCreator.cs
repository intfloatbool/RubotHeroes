using System.Collections.Generic;
using UnityEngine;

public class CommandProviderCreator : SingletonDoL<CommandProviderCreator>
{
    [SerializeField] private List<CommandsProviderBase> _providerPrefabs;
    public T GetProvider<T>() where T : CommandsProviderBase
    {
        foreach (CommandsProviderBase provider in _providerPrefabs)
        {
            if (provider.GetType() == typeof(T))
            {
                return (T) Instantiate(provider);
            }
        }
        Debug.LogError($"Cannot get type {typeof(T)} of provider!");
        return null;
    }

    protected override CommandProviderCreator GetLink()
    {
        return this;
    }
}
