using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.GameResources
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float health;

        public event Action onDeath;

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                onDeath();
            }
        }
    }
}