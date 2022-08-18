using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.Saving;

namespace Starborne.GameResources
{
    public class PlayerHealth : MonoBehaviour
    {
        private float health;
        private float maxHealth;

        public event Action onDeath;

        void Start()
        {
            Character characterStats = FindObjectOfType<CharacterHandler>().GetCharacterStats();

            maxHealth = characterStats.maxHP;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if(health <= 0)
            {
                onDeath();
            }
        }
    }
}