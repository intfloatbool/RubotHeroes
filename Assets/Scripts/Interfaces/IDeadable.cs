using System;

public interface IDeadable
{
    event Action OnDeath;
}
