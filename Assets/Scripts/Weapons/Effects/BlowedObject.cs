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
    public virtual void Explosion(Rigidbody affected = null, Vector3 lastPosition = default)
    {
        Explode();
        if (affected != null)
        {
            Vector3 exposiionVector = new Vector3(lastPosition.x, transform.position.y, lastPosition.z);
            Vector3 explosionAffect = exposiionVector.normalized * _explodePower * RandomExplodeMultipler;
            affected.AddForce(explosionAffect, 
                ForceMode.Acceleration);
        }
    }

    protected virtual void Explode()
    {
        _bodyCollider.enabled = false;
        _objectBody.SetActive(false);
        _blowEffect.SetActive(true);
        Destroy(this.gameObject, _lifeTimeAfterBlow);
    }
}
