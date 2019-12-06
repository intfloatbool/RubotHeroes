using UnityEngine;

[RequireComponent(typeof(Robot))]
public class CustomRobotCommandRunner : MonoBehaviour
{
    [SerializeField] private CommandType _command;
    public CommandType Command
    {
        get => _command;
        set => _command = value;
    }

    [SerializeField] private Robot _robot;

    public void RunCommand()
    {
        ICommand command = CommandHelper.GetCommandByType(_command, _robot);
        command.Execute();
    }
}
