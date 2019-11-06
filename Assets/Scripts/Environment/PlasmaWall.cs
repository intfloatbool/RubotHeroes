using System.Collections;
using UnityEngine;

public class PlasmaWall : MonoBehaviour, IProtectableContacted
{
    [SerializeField] private bool _isOnShield = true;
    [SerializeField] private GameObject _onContactEffect;
    [SerializeField] private float _effectTime = 1f;

    private Coroutine _contactCoroutine = null;
    
    public bool IsOnShield
    {
        get => _isOnShield;
        set => _isOnShield = value;
    }

    public void OnContact(Vector3 positon)
    {
        if (_contactCoroutine == null)
        {
            _contactCoroutine = StartCoroutine(ContactCoroutine(positon));
        }
    }

    private IEnumerator ContactCoroutine(Vector3 position)
    {
        _onContactEffect.transform.position = position;
        _onContactEffect.SetActive(true);
        yield return new WaitForSeconds(_effectTime);
        _onContactEffect.SetActive(false);
        _contactCoroutine = null;
    }
}
