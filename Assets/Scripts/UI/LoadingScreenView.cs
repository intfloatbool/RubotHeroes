using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenView : MonoBehaviour
{
    [SerializeField] private float _speedOfSlider = 4f;
    [SerializeField] private Image _progressImg;
    [SerializeField] private float _reachedValue = 0.3f;
    public float ReachedValue
    {
        get => _reachedValue;
        set { this._reachedValue = value; }
    }

    private void Update()
    {
        if (_progressImg == null)
            return;
        _progressImg.fillAmount = Mathf.Lerp(_progressImg.fillAmount, 
            _reachedValue,
            _speedOfSlider *
            Time.deltaTime);
    }

    private void OnDisable()
    {
        Reset();
    }

    private void Reset()
    {
        _progressImg.fillAmount = 0;
        _reachedValue = 0;
    }
}
