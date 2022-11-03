using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.GameResources
{
    public class ChildHealth : MonoBehaviour, IHealth /*Class that tracks the health of an entity that is part of a larger entity. When this entity is damages, the larger entity is also damaged.*/
    {
        [SerializeField] private float health; /*This entity's health.*/
        private EnemyHealth parentHealth; /*The entity that should also take damage when this entity takes damage.*/
        private bool isDead = false; /*Tracks whether or not this entity is dead.*/
        public event Action onDeath; /*This event is invoked when the entity dies.*/

        private void Awake()
        {
            parentHealth = GetComponentInParent<EnemyHealth>(); /*Get the value for parentHealth which will be an EnemyHealth component in this GameObject's parent.*/
        }

        public void TakeDamage(float damage) /*Take a given amount of damage and deal the same damage to parentHealth. If health is lowered to 0 or below, set isDead to true and call onDeath.*/
        {
            health -= damage;
            parentHealth.TakeDamage(damage);
            if (!isDead && health <= 0) //the isDead check should remove the bug that onDeath can be called multiple times before the object is destroyed
            {
                isDead = true;
                onDeath();
            }
        }
    }
}