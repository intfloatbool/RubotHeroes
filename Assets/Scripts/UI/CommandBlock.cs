using System;
using UnityEngine;
using UnityEngine.UI;

public class CommandBlock : MonoBehaviour
{
    public event Action<CommandBlock> OnRemove = (block) => { };
    [SerializeField] private Button _removeBtn;
    [SerializeField] private Text _indexTextUi;
    [SerializeField] private Text _commandNameTextUi;
    public CommandType CommandType { get; private set; }
    public void InitializeBlock(int index, string name, CommandType commandType)
    {
        SetIndex(index);
        _commandNameTextUi.text = name;
        CommandType = commandType;
        _removeBtn.onClick.AddListener(() =>
        {
            OnRemove.Invoke(this);
            Destroy(gameObject);
        });
    }

    public void SetIndex(int index)
    {
        _indexTextUi.text = index.ToString();
    }
}
