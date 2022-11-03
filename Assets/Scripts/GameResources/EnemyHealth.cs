using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.GameResources
{
    public class EnemyHealth : MonoBehaviour, IHealth /*Class that handles the health of the enemies.*/
    {
        [SerializeField] private float health; /*Health of the enemy.*/
        private bool isDead = false; /*Whether or not the enemy is dead.*/

        public event Action onDeath; /*This event is invoked when the enemy dies.*/

        public void TakeDamage(float damage) /*Deals a given amount of damage to the enemy. If health is lowered to or below 0, isDead is switched to true, and onDeath is invoked.*/
        {
            health -= damage;
            if (!isDead && health <= 0) //the isDead check should remove the bug that onDeath can be called multiple times before the object is destroyed
            {
                isDead = true;
                onDeath();
            }
        }
    }
}