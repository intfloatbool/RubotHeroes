using UnityEngine;

[System.Serializable]
public class CommandInfo
{
    [SerializeField] private CommandType _commandType;
    public CommandType CommandType => _commandType;
    [SerializeField] private Sprite _icon;
    public Sprite Icon => _icon;
    [SerializeField] private string _nameKey;
    public string NameKey => _nameKey;
}
