using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CommandsAudioPlayer : SingletonDoL<CommandsAudioPlayer>
{
    [System.Serializable]
    private class CommandWithSound
    {
        public CommandType CommandType;
        public AudioClip AudioClip;
    }

    [SerializeField] private List<CommandWithSound> _commandsWithSounds;
    private Dictionary<CommandType, AudioClip> _soundsDict = new Dictionary<CommandType, AudioClip>();

    protected override CommandsAudioPlayer GetLink() => this;

    protected override void Awake()
    {
        base.Awake();
        InitializeDict();
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

    public void PlayCommandSoundByType(AudioSource source, CommandType cmdType)
    {
        AudioClip audioClip = _soundsDict.ContainsKey(cmdType) ? _soundsDict[cmdType] : null;
        if (audioClip == null)
            return;
        if (source == null)
            return;
        source.PlayOneShot(audioClip);
    }

}
