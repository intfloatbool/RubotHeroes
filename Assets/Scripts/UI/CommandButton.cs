using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CommandButton : MonoBehaviour
{
    public Button Button { get; private set; }
    [SerializeField] private Text _textUI;

    private void Awake()
    {
        Button = GetComponent<Button>();
    }

    public void Initialize(string name)
    {
        _textUI.text = name;
    }
    
}
