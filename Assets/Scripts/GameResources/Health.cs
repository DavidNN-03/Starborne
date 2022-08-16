using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.Saving;

namespace Starborne.GameResources
{
    public class Health : MonoBehaviour
    {
        private float health;
        private float maxHealth;

        void Start()
        {
            Character characterStats = FindObjectOfType<CharacterHandler>().GetCharacterStats();

            maxHealth = characterStats.maxHP;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
        }
    }
}