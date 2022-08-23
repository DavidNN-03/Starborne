using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.GameResources
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float health;
        bool isDead = false;

        public event Action onDeath;

        public void TakeDamage(float damage)
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