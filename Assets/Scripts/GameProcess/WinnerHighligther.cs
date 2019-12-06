using System;
using System.Collections;
using UnityEngine;

public class WinnerHighligther : OnGameEndTrigger
{
    [SerializeField] private float _camDelay = 2f;
    private float _speed = 0.2f;
    private Vector3 _neededLocalPos = new Vector3(2, 4, -2);
    private Vector3 _neededLocalEulers = new Vector3(45, -45, 0);
    [SerializeField] private Camera _mainCam;
    [SerializeField] private GameObject[] _objectsToActivate;
    [SerializeField] private GameStarter _gameStarter;   
    private bool _isActivated;
    
    private Transform CamTransform => _mainCam.transform;

    protected override void Awake()
    {
        base.Awake();
        this.CheckLinks(new Behaviour[]
        {
            _mainCam,
            _gameStarter
        });
    }
    protected override void OnGameEnd(RobotCommandRunner winner)
    {
        Robot winnerRobot = winner.Robot;
        winnerRobot.RobotStatus.IsImmortal = true;
        CamTransform.parent = winnerRobot.transform;
        StartCoroutine(ActivateWinCameraByTime(_camDelay));
        TurnOffPlayerStatusByWinner(winner.Owner);
        
        for (int i = 0; i < _objectsToActivate.Length; i++)
        {
            _objectsToActivate[i].SetActive(true);
        }
    }

    private void TurnOffPlayerStatusByWinner(PlayerOwner ownerOfWinner)
    {
        PlayerOwner looser = ownerOfWinner == PlayerOwner.PLAYER_1 ? PlayerOwner.PLAYER_2 : PlayerOwner.PLAYER_1;
        GameStarter.PlayerContainer playerContainer = _gameStarter.GetPlayer(looser);
        playerContainer.StatusPanel.gameObject.SetActive(false);
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
