using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RobotAnimationController : MonoBehaviour
{
    [System.Serializable]
    private class TypedAnimation
    {
        public CommandType CommandType;
        public AnimationClip AnimationClip;
    }
    
    [SerializeField] private Robot _robot;
    private Animator _animator;
    [SerializeField] private List<TypedAnimation> _typedAnimations;
    private Dictionary<CommandType, AnimationClip> _clipsDict = new Dictionary<CommandType, AnimationClip>();
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        InitializeDict();
        Subscribe();
    }

    private void InitializeDict()
    {
        foreach (TypedAnimation typedAnimation in _typedAnimations)
        {
            CommandType animType = typedAnimation.CommandType;
            if (!_clipsDict.ContainsKey(animType))
            {
                AnimationClip clip = typedAnimation.AnimationClip;
                if (clip == null)
                {
                    Debug.LogError("Clip is null!");
                    return;
                }
                
                _clipsDict.Add(animType, clip);
            }
            else
            {
                Debug.LogError($"Failed to initialize animation by type {animType}, already exists!");
            }
        }
    }

    private void Subscribe()
    {
        if (_robot != null)
        {
            _robot.OnCommandExecuted += PlayAnimationByCommand;
        }
        else
        {
            Debug.LogError("Animation controller will not work, robot reference is null!");
        }
    }

    private void PlayAnimationByCommand(ICommand command)
    {
        CommandType cmdType = command.CommandType;
        AnimationClip clipToPlay = _clipsDict.ContainsKey(cmdType) ? _clipsDict[cmdType] : null;

        if (clipToPlay != null)
        {
            _animator.Play(clipToPlay.name);
        }
    }
}
