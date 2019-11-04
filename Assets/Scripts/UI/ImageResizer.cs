using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageResizer : MonoBehaviour
{
    [SerializeField] private Vector3 _neededScale = Vector3.one;
    [SerializeField] private Vector3 _basicScale = Vector3.zero;
    [SerializeField] private float _speed = 6f;
    public bool IsActive { get; set; }
    private Image _img;
    
    private void Start()
    {
        _img = GetComponent<Image>();
        transform.localScale = _basicScale;
    }

    private void Update()
    {
        if (!IsActive)
            return;
        transform.localScale = Vector3.Lerp(transform.localScale, _neededScale, _speed * Time.deltaTime);
    }
    
}
