using System;

public interface IHealth
{
    public event Action onDeath;
    void TakeDamage(float damage);
}
