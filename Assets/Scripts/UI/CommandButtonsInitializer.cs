using System;
using UnityEngine;

public class CommandButtonsInitializer : MonoBehaviour
{
    [SerializeField] private CommandsPanel _commandsPanel;
    [SerializeField] private CommandButton _cmdBtnPrefab;
    private void Start()
    {
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        string[] commandTypeNames = Enum.GetNames(typeof(CommandType));

        for (int i = 0; i < commandTypeNames.Length; i++)
        {
            CommandType command = (CommandType) i;
            CommandButton cmdBtn = Instantiate(_cmdBtnPrefab, transform);
            cmdBtn.Initialize(GetNameByCommand(command));
            
            cmdBtn.Button.onClick.AddListener(() =>
            {
                _commandsPanel.AddCommand(command);
            });
        }
    }

    public static string GetNameByCommand(CommandType cmdType)
    {
        switch (cmdType)
        {
            case CommandType.JUMP:
            {
                return "Jump";
            }
            case CommandType.RANDOM_MOVE:
            {
                return "Random move";
            }
            case CommandType.MEELE_ATTACK:
            {
                return "Melee attack";
            }
            case CommandType.LAUNCH_MISSLE:
            {
                return "Launch missile";
            }
            case CommandType.PROTECTED_SHIELD:
            {
                return "Protected shield";
            }
            default:
            {
                return "UNKNOWN";
            }
        }
    }
}
