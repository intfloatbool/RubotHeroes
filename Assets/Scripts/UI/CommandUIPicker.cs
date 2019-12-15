using System.Collections;
using System.Collections.Generic;
using GameProcess;
using UnityEngine;
using UnityEngine.UI;

public class CommandUIPicker : MonoBehaviour
{
    [SerializeField] private DummyRobotCommandRunner _dummyRobotCommandRunner;
    [SerializeField] private CommandsPanel _commandsPanel;
    [SerializeField] private Text _currentCommandText;
    private CommandType[] _readyCommands = CommandHelper.ReadyCommands.ToArray();

    private int _currentIndex = 0;


    private void Start()
    {
        OnCommandUpdate();
    }
    private CommandType CurrentCommand => _readyCommands[_currentIndex];
    
    public void NextCommand()
    {
        _currentIndex++;
        if (_currentIndex >= _readyCommands.Length)
            _currentIndex = 0;

        OnCommandUpdate();
    }

    public void PreviousCommand()
    {
        _currentIndex--;
        if (_currentIndex < 0)
            _currentIndex = _readyCommands.Length - 1;
        OnCommandUpdate();
    }

    private void OnCommandUpdate()
    {
        _currentCommandText.text = CurrentCommand.ToString();
    }

    public void AddCurrentCommandOnPanel()
    {
        _commandsPanel.AddCommand(CurrentCommand);
    }

    public void ShowCommandOnRobot()
    {
        _dummyRobotCommandRunner.RunSpecificCommand(CurrentCommand);
    }
}
