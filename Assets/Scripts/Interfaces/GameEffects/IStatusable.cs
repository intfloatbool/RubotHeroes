using System;

public interface IStatusable
{
    event Action<StatusEffectType> OnStatusEffected;
    void OnStatusEffect(StatusItem.StatusInfo statusEffect);
}
