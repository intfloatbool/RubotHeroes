using System;
using UnityEngine;

public class WinnerHighligther : OnGameEndTrigger
{
    private float _speed = 0.5f;
    private Vector3 _neededLocalPos = new Vector3(5, 10, 0);
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
        _isActivated = true;
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
