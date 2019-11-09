using UnityEngine;

public class GameEventBase : MonoBehaviour
{
    [SerializeField] protected GameEventType _eventType;
    public GameEventType EventType => _eventType;
    public void SetActiveEvent(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
