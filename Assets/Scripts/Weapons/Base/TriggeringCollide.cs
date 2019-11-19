using System;
using Interfaces.Triggers;
using UnityEngine;

public abstract class TriggeringCollide : MonoBehaviour
{
    protected abstract void OnCollide(ICollidable collidable);
    protected virtual void OnCollisionEnter(Collision other)
    {
        ICollidable collidable = other.gameObject.GetComponent<ICollidable>();
        if (collidable != null)
        {
            OnCollide(collidable);
        }
    }
}
