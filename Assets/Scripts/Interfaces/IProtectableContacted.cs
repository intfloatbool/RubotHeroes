using UnityEngine;

public interface IProtectableContacted : IProtectable
{
    void OnContact(Vector3 positon);
}
