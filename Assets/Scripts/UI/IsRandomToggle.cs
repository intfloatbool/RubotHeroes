using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class IsRandomToggle : MonoBehaviour
{
    [SerializeField] private GameObject _blocksPanel;
    [SerializeField] private GameObject _commandsPanel;

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();

        _toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool isActive)
    {
        _blocksPanel.SetActive(!isActive);
        _commandsPanel.SetActive(!isActive);

        if (isActive)
        {
            
        }
    }
}
