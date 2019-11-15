using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class IsRandomToggle : MonoBehaviour
{
    [SerializeField] private GameObject _blocksPanel;
    [SerializeField] private GameObject _commandsPanel;
    private Toggle _toggle;

    private PlayerOwner _globalUserOwner;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(OnValueChanged);

        _globalUserOwner = UserPlayerInfo.Instance.GlobalPlayerOwner;
    }

    private void OnValueChanged(bool isActive)
    {
        _blocksPanel.SetActive(!isActive);
        _commandsPanel.SetActive(!isActive);

        if (isActive)
        {
            UserPlayerInfo.Instance.SetCommandProviderByOwner<RandomCommandsProvider>(_globalUserOwner);
        }
        else
        {
            UserPlayerInfo.Instance.SetCommandProviderByOwner<ConstructorCommandsProvider>(_globalUserOwner);
        }

    }
}
