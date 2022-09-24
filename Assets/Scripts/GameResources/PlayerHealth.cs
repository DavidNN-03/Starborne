using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.Saving;
using Starborne.UI;
using Starborne.Core;

namespace Starborne.GameResources
{
    public class PlayerHealth : MonoBehaviour, IHealth
    {
        [SerializeField] float health;
        [SerializeField] float maxHealth;
        GameUI gameUI;

        public event Action onDeath;

        void Awake()
        {
            gameUI = FindObjectOfType<GameUI>();
        }

        void Start()
        {
            Character characterStats = EssentialObjects.instance.GetComponentInChildren<CharacterHandler>().GetCharacterStats();

            maxHealth = characterStats.maxHP;
            health = maxHealth;
        }

        void Update()
        {
            gameUI.UpdateHealthText(health, maxHealth);
        }

        public void Collision()
        {
            health = 0f;
            onDeath();
        }

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(0f, health - damage);
            if (health <= 0f)
            {
                onDeath();
            }
        }

        public float GetHealth()
        {
            return health;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }
    }
}