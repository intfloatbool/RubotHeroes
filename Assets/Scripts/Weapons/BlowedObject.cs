using UnityEngine;

public abstract class BlowedObject : MonoBehaviour
{
    [SerializeField] protected Collider _bodyCollider;
    [SerializeField] protected GameObject _objectBody;
    [SerializeField] protected GameObject _blowEffect;
    [SerializeField] protected float _explodePower = 60f;
    [SerializeField] protected float _maxExplodeRandomizeMultiplier = 1.5f;
    [SerializeField] protected float _lifeTimeAfterBlow = 2f;
    private float RandomExplodeMultipler => Random.Range(1f, _maxExplodeRandomizeMultiplier);
    protected void Explosion(Rigidbody affected = null, Vector3 lastPosition = default)
    {
        _bodyCollider.enabled = false;
        _objectBody.SetActive(false);
        _blowEffect.SetActive(true);
        
        if (affected != null)
        {
            Vector3 relativePos = lastPosition - transform.position;
            affected.AddForce(relativePos.normalized * _explodePower * RandomExplodeMultipler);
        }
        Destroy(this.gameObject, _lifeTimeAfterBlow);
    }
}
