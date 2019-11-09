using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CommandsAudioPlayer : MonoBehaviour
{
    [SerializeField] private Robot[] _robots;

    [System.Serializable]
    private class CommandWithSound
    {
        public CommandType CommandType;
        public AudioClip AudioClip;
    }

    [SerializeField] private List<CommandWithSound> _commandsWithSounds;
    private Dictionary<CommandType, AudioClip> _soundsDict = new Dictionary<CommandType, AudioClip>();

    private void Awake()
    {
        InitializeDict();
        foreach (Robot robot in _robots)
        {
            robot.OnCommandExecuted += (command) =>
            {
                PlayClipByCommand(robot.AudioSource,command.CommandType);
            };
        }
    }

    private void InitializeDict()
    {
        foreach (CommandWithSound cmd in _commandsWithSounds)
        {
            if (!_soundsDict.ContainsKey(cmd.CommandType))
            {
                _soundsDict.Add(cmd.CommandType, cmd.AudioClip);
            }
        }
    }

    private void PlayClipByCommand(AudioSource audioSource,CommandType commandType)
    {
        if (_soundsDict.ContainsKey(commandType))
        {
            audioSource.PlayOneShot(_soundsDict[commandType]);
        }
    }
    
}
