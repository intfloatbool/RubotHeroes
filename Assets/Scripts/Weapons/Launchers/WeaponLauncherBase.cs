using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Interfaces.GameEffects;
using Interfaces.Views;
using UnityEngine;

public abstract class WeaponLauncherBase : MonoBehaviour
{
    [SerializeField] protected StatusItem _statusItem;
    public StatusItem StatusItem => _statusItem;
    public abstract WeaponType WeaponType { get; }
    [SerializeField] protected Transform _sourceOfLaunch;
    [SerializeField] protected bool _isInProcess;
    protected Robot _owner;
    public Robot Owner => _owner;
    
    protected Coroutine _currentCoroutine;
    
    public bool IsInProcess
    {
        get => _isInProcess;
        protected set => _isInProcess = value;
    } 

    private GameObject _sender;

    public void SetOwner(Robot robot)
    {
        _owner = robot;
    }
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
        _currentCoroutine = StartCoroutine(OnLaunchedCoroutine(this._sender));
        yield return _currentCoroutine;
        _currentCoroutine = null;
        IsInProcess = false;
    }

    protected abstract IEnumerator OnLaunchedCoroutine(GameObject sender);

    public void StopWeapon()
    {
        if(_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        IsInProcess = false;
        
    }

    protected void Colorize(IColorizable colorizable)
    {
        colorizable.SetColor(_owner.RobotColor);
    }

    protected virtual void TryAttachStatusToCarrier(Object target)
    {
        if (target is IStatusCarrier carrier)
        {
            if (_statusItem == null || !_statusItem.StatusCollection.Any())
                return;
            carrier.InitializeStatusEffects(_statusItem.StatusCollection);
        }
    }
}
