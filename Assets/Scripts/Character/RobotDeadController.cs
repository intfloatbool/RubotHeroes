using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RobotDeadController : MonoBehaviour, IDeadController
{
    [SerializeField] private float _maxBlowPower = 1000f;
    [SerializeField] private AudioClip _exposionClip;
    [SerializeField] private AudioSource _audioSource;
    public IDeadable Deadable { get; private set; }
    [SerializeField] private Robot _robot;
    [SerializeField] private GameObject _blowEffect;

    private float RandomVal => Random.Range(-1f, 1f);
    private float BlowPower => Random.Range(_maxBlowPower / 3f, _maxBlowPower);

    private void Awake()
    {
        Deadable = _robot;
        Deadable.OnDeath += HandleDeath;
        
        _blowEffect.SetActive(false);
    }
    
    public void HandleDeath()
    {
        List<Rigidbody> robotBody = GetRobotBody().ToList();
        robotBody.Add(_robot.Rigidbody); //add head too
        foreach (Rigidbody rb in robotBody)
        {
            Vector3 randomVector = new Vector3(RandomVal, RandomVal, RandomVal);
            rb.AddForce(randomVector * BlowPower);
        }
        
        _blowEffect.SetActive(true);
        _audioSource.volume = 1f;
        _audioSource.PlayOneShot(_exposionClip);
        _audioSource.loop = false;
    }
    
    private IEnumerable<Rigidbody> GetRobotBody()
    {
        foreach (Transform child in _robot.transform.GetComponentsInChildren<Transform>())
        {
            MeshRenderer meshRend = child.GetComponent<MeshRenderer>();
            if (meshRend != null)
            {
                meshRend.gameObject.AddComponent<BoxCollider>();
                Rigidbody rbToAffect = meshRend.gameObject.AddComponent<Rigidbody>();
                yield return rbToAffect;
            }
        }
    }
}
