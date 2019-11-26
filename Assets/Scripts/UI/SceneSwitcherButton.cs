using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneSwitcherButton : MonoBehaviour
{
    [SerializeField] private Scenes _sceneToGo;
    private Button _button;

    private void Awake()
    {
        this._button = GetComponent<Button>();
        this._button.onClick.AddListener(() => ScenesSwitcher.Instance.RunSceneByType(_sceneToGo));
    }
}
