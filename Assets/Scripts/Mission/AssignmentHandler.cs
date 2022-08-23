using System.Collections;
using System.Collections.Generic;
using Starborne.GameResources;
using Starborne.UI;
using UnityEngine;

namespace Starborne.Mission
{
    public class AssignmentHandler : MonoBehaviour
    {
        EnemyHealth[] enemies;
        GameUI gameUI;

        int enemyCount;
        int enemiesKilled;

        void Awake()
        {
            gameUI = FindObjectOfType<GameUI>();
            enemies = GameObject.FindObjectsOfType<EnemyHealth>();
            enemyCount = enemies.Length;
            enemiesKilled = 0;
        }

        void Start()
        {
            foreach (EnemyHealth enemyHealth in enemies)
            {
                enemyHealth.onDeath += EnemyDestroyed;
            }
            CheckWin();
        }

        private void EnemyDestroyed()
        {
            enemiesKilled++;
            CheckWin();
        }

        private void CheckWin()
        {
            if (enemiesKilled == enemyCount)
            {
                //won
            }
            UpdateUI();
        }

        private void UpdateUI()
        {
            gameUI.UpdateMissionText(enemiesKilled, enemyCount);
        }
    }
}