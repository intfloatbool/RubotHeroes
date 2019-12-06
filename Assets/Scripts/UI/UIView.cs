using UnityEngine;
using UnityEngine.UI;

public class UIView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Text _textUI;

    public virtual void SetView(Sprite sprite, string text)
    {
        SetSprite(sprite);
        SetText(text);
    }

    public void SetSprite(Sprite sprite)
    {
        this._image.sprite = sprite;
    }

    public void SetText(string text)
    {
        this._textUI.text = text;
    }
}
