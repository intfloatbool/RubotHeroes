using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextLocalization : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
        UpdateTextByLocalization();
    }

    private void UpdateTextByLocalization()
    {
        string key = _text.text;
        string localized = GameLocalization.GetLocalization(key);
        _text.text = localized;
    }
}
