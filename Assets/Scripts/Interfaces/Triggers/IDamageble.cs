using UnityEngine;

namespace Interfaces.Triggers
{
    public interface IDamageble
    {
        GameObject GameObject { get; }
        void AddDamage(float dmg);
    }
}
