using System;

public interface IHealth /*This interface is implemented in all classes related to the health of game entities.*/
{
    public event Action onDeath; /*This event should be called when the entity dies.*/
    public void TakeDamage(float damage); /*This function should be called to deal damage to the entity.*/
}
