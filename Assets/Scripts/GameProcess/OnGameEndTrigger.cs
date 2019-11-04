using UnityEngine;

public abstract class OnGameEndTrigger : MonoBehaviour
{
    [SerializeField] protected GameProcessStatus _gameProcessStatus;

    protected virtual void Awake()
    {
        _gameProcessStatus.OnWinnerDetected += OnGameEnd;
    }

    protected abstract void OnGameEnd(RobotCommandRunner winner);
}
