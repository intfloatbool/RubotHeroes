using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandsData : DontDestroyOnLoadMB
{
    public static CommandsData Instance { get; private set; }
    [SerializeField] private List<CommandInfo> _commandInfos;
    private Dictionary<CommandType, CommandInfo> _infosDict = new Dictionary<CommandType, CommandInfo>();

    protected override void Awake()
    {
        base.Awake();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new Exception("Cannot create second commandsData!");
        }

        InitializeDict();
    }

    private void InitializeDict()
    {
        foreach (CommandInfo cmdInfo in _commandInfos)
        {
            CommandType infoType = cmdInfo.CommandType;
            if (!_infosDict.ContainsKey(infoType))
            {
                _infosDict.Add(infoType, cmdInfo);
            }
            else
            {
                Debug.LogError($"Cannot add command info with same type!- {infoType}");
            }
        }
    }

    public CommandInfo GetCommandInfoByType(CommandType cmdType)
    {
        return _infosDict.ContainsKey(cmdType) ? _infosDict[cmdType] : null;
    }
}
