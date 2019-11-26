using Interfaces;
using UnityEngine;

[RequireComponent(typeof(ICommandExecutor), 
    typeof(IAudioPlayable))]
public class RobotCommandsAudioPlayer : MonoBehaviour
{
    private ICommandExecutor _commandExecutor;
    private IAudioPlayable _audioPlayable;

    private void Awake()
    {
        _commandExecutor = GetComponent<ICommandExecutor>();
        _audioPlayable = GetComponent<IAudioPlayable>();
    }

    private void Start()
    {
        _commandExecutor.OnCommandTypeExecuted += PlaySound;
    }

    private void PlaySound(CommandType cmdType)
    {
        CommandsAudioPlayer commandsAudioPlayer = CommandsAudioPlayer.Instance;
        if (commandsAudioPlayer == null)
        {
            Debug.LogError($"Cannot get instance of {typeof(CommandsAudioPlayer)}!");
            return;
        }
        
        commandsAudioPlayer.PlayCommandSoundByType(_audioPlayable.AudioSource, cmdType);
    }
}
