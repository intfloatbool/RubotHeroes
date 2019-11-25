using System;
using System.Collections;
using UnityEngine;

public class WinnerHighligther : OnGameEndTrigger
{
    [SerializeField] private float _camDelay = 2f;
    private float _speed = 0.2f;
    private Vector3 _neededLocalPos = new Vector3(5, 12, 0);
    private Vector3 _neededLocalEulers = new Vector3(60, -90, 0);
    [SerializeField] private ImageResizer _imgResizer;
    [SerializeField] private Camera _mainCam;

    private bool _isActivated;
    
    private Transform CamTransform => _mainCam.transform;
    protected override void OnGameEnd(RobotCommandRunner winner)
    {
        Robot winnerRobot = winner.Robot;
        CamTransform.parent = winnerRobot.transform;
        _imgResizer.IsActive = true;
        StartCoroutine(ActivateWinCameraByTime(_camDelay));
    }

    private IEnumerator ActivateWinCameraByTime(float delay, bool isActive = true)
    {
        yield return new WaitForSeconds(delay);
        _isActivated = isActive;
    }

    private void FixedUpdate()
    {
        if (!_isActivated)
            return;
        CamTransform.localPosition =
            Vector3.Lerp(CamTransform.localPosition, _neededLocalPos, _speed * Time.fixedDeltaTime);
        CamTransform.localRotation = Quaternion.Lerp(CamTransform.localRotation, Quaternion.Euler(_neededLocalEulers), _speed * Time.fixedDeltaTime);
    }
}
