using UI.Enum;
using UnityEngine;

public class CustomizableUI : MonoBehaviour
{
    [SerializeField] protected UIType _uiType;
    public UIType UiType => _uiType;
    public void SetActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }
}
