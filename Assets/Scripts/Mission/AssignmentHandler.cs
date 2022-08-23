using System.Collections;
using System.Collections.Generic;
using Starborne.GameResources;
using Starborne.UI;
using UnityEngine;
using Starborne.SceneHandling;

namespace Starborne.Mission
{
    public class AssignmentHandler : MonoBehaviour
    {
        [SerializeField] float changeSceneOnWinDelay = 1f;
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
                Win();
            }
            UpdateUI();
        }

        private void UpdateUI()
        {
            gameUI.UpdateMissionText(enemiesKilled, enemyCount);
        }

        private void Win()
        {
            Cursor.lockState = CursorLockMode.None;
            SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();
            sceneHandler.LoadScene(sceneHandler.charSelectSceneIndex, changeSceneOnWinDelay);
        }
    }
}