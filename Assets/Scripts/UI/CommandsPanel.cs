using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandsPanel : MonoBehaviour
{
    [SerializeField] private CommandBlock _commandBlockPrefab;
    [SerializeField] private List<CommandBlock> _commandBlocks;
    private CommandsProviderBase UserCommandProvider => UserPlayerInfo.Instance.GetGlobalUser().CommandsProvider;

    public void AddCommand(CommandType cmdType)
    {
        Debug.Log($"Add command! {cmdType}!");
        CreateBlock(cmdType);
        UpdateCommands();
    }

    private void CreateBlock(CommandType cmdType)
    {
        CommandBlock block = Instantiate(_commandBlockPrefab, transform);
        _commandBlocks.Add(block);
        string name = CommandButtonsInitializer.GetNameByCommand(cmdType);
        block.InitializeBlock(_commandBlocks.IndexOf(block), name, cmdType);
        block.OnRemove += RemoveBlock;
    }

    private void RemoveBlock(CommandBlock block)
    {
        if (_commandBlocks.Contains(block))
        {
            _commandBlocks.Remove(block);
            UpdateBlocks();
        }
    }

    private void UpdateBlocks()
    {
        foreach (var block in _commandBlocks)
        {
            block.SetIndex(_commandBlocks.IndexOf(block));
        }

        UpdateCommands();
    }
    
    

    private void UpdateCommands()
    {
        List<CommandType> commandTypes = _commandBlocks.Select(block => block.CommandType).ToList();
        UserCommandProvider.SetCommands(commandTypes);
    }

    public void SaveCommands()
    {
        //Bad design.
        Debug.Log("Try to write commands data for player..");
        if (_commandBlocks.Count > 0)
        {
            var playerInfo = UserPlayerInfo.Instance.GetPlayerInfoByOwner(PlayerOwner.PLAYER_1);
            UserPlayerInfo.Instance.SetCommandProviderByOwner<ConstructorCommandsProvider>(PlayerOwner.PLAYER_1);
            playerInfo.CommandsProvider.SetCommands(
                _commandBlocks.Select(block => block.CommandType).ToList()
                );
            Debug.Log($"Commands changed and write into {playerInfo.CommandsProvider.name} !");
        }
        
    }

    private void OnDisable()
    {
        _commandBlocks.ForEach(b => Destroy(b.gameObject));
        _commandBlocks.Clear();
    }
}
