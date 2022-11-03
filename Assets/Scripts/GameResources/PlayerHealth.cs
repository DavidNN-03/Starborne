using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.Saving;
using Starborne.UI;
using Starborne.Core;

namespace Starborne.GameResources
{
    public class PlayerHealth : MonoBehaviour, IHealth /*Class that manages the health of the player.*/
    {
        [SerializeField] private float health; /*Player's current health.*/
        [SerializeField] private float maxHealth; /*Player's max health.*/
        private GameUI gameUI; /*The GameUI.*/
        public event Action onDeath; /*This event will be invoked when the player dies.*/

        private void Awake() /*Find the value for gameUI.*/
        {
            gameUI = FindObjectOfType<GameUI>();
        }

        private void Start() /*Get the character stats.*/
        {
            Character characterStats = EssentialObjects.instance.GetComponentInChildren<CharacterHandler>().GetCharacterStats();

            maxHealth = characterStats.maxHP;
            health = maxHealth;
        }

        private void Update() /*Update the UI by calling UpdateHealthText on gameUI.*/
        {
            gameUI.UpdateHealthText(health, maxHealth);
        }

        public void Collision() /*When the player collides with an object that is not a projectile, they immediatly die.*/
        {
            health = 0f;
            onDeath();
        }

        public void TakeDamage(float damage) /*Lower the health by a given amount. If health is lowered to or below 0, invoke onDeath.*/
        {
            health = Mathf.Max(0f, health - damage);
            if (health <= 0f)
            {
                onDeath();
            }
        }

        public float GetHealth() /*Returns health.*/
        {
            return health;
        }

        public float GetMaxHealth() /*Returns maxHealth.*/
        {
            return maxHealth;
        }
    }
}