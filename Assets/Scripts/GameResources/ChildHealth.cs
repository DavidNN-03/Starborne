using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.GameResources
{
    public class ChildHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private float health;
        EnemyHealth parentHealth;
        bool isDead = false;

        public event Action onDeath;

        void Awake()
        {
            parentHealth = GetComponentInParent<EnemyHealth>();
        }

        public void TakeDamage(float damage)
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