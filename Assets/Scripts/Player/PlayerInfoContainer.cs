using UnityEngine;

[System.Serializable]
public class PlayerInfoContainer
{
    [SerializeField] private PlayerOwner _owner;
    public PlayerOwner Owner => _owner;
    
    [SerializeField] private Player _player;
    public Player Player
    {
        get => _player;
        set => _player = value;
    }

    [SerializeField] private CommandsProviderBase _commandsProvider;
    public CommandsProviderBase CommandsProvider
    {
        get => _commandsProvider;
        set => _commandsProvider = value;
    }
}