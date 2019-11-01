using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponLauncherBase : MonoBehaviour
{
    [SerializeField] protected Transform _sourceOfLaunch;

    [SerializeField] protected bool _isInProcess;

    public bool IsInProcess
    {
        get => _isInProcess;
        protected set => _isInProcess = value;
    }

    private GameObject _sender;

    public virtual void LaunchWeapon(GameObject sender)
    {
        this._sender = sender;
        if (IsInProcess)
        {
            Debug.LogError($"Cannot launch weapon twice!");
            return;
        }
        IsInProcess = true;
        StartCoroutine(WeaponProcessCoroutine());
    }

    protected virtual IEnumerator WeaponProcessCoroutine()
    {
        yield return StartCoroutine(OnLaunchedCoroutine(this._sender));
        IsInProcess = false;
    }

    protected abstract IEnumerator OnLaunchedCoroutine(GameObject sender);
}
