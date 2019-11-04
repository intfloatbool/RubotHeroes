using System;
using System.Collections.Generic;
using UnityEngine;

public class OnEndObjectManipulator : OnGameEndTrigger
{
    [System.Serializable]
    private struct ObjectActivationContainer
    {
        public GameObject GameObject;
        public bool IsActive;
    }

    [SerializeField] private List<ObjectActivationContainer> _containers;
    
    protected override void OnGameEnd(RobotCommandRunner winner)
    {
        foreach (ObjectActivationContainer container in _containers)
        {
            container.GameObject.SetActive(container.IsActive);
        }
    }
}
