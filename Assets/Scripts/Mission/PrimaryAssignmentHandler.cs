using System.Collections;
using System.Collections.Generic;
using Starborne.GameResources;
using Starborne.UI;
using UnityEngine;
using Starborne.SceneHandling;

namespace Starborne.Mission
{
    public class PrimaryAssignmentHandler : MonoBehaviour, ILateInit /*Class that handles the primary assignment of the player. The primary assignment is the same in every level: destroy all the enemies.*/
    {
        [SerializeField] private float changeSceneOnWinDelay = 1f; /*Amount of seconds between winning the game and loading the next scene.*/
        private int enemyCount; /*Amount of enemies when the game starts.*/
        private int enemiesKilled; /*Amout of enemies killed.*/

        private EnemyHealth[] enemies; /*Array of the enemies.*/
        private GameUI gameUI; /*The GameUI.*/

        public void LateAwake() /*Find values for gameUI, enemies, enemyCount, and set enemiesKilled to 0.*/
        {
            gameUI = FindObjectOfType<GameUI>();
            enemies = GameObject.FindObjectsOfType<EnemyHealth>();
            enemyCount = enemies.Length;
            enemiesKilled = 0;
        }

        public void LateStart() /*Add EnemyDestroyed to every enemy's onDeath event. Also, call CheckWin.*/
        {
            foreach (EnemyHealth enemyHealth in enemies)
            {
                enemyHealth.onDeath += EnemyDestroyed;
            }
            CheckWin();
        }

        private void EnemyDestroyed() /*Is called when an enemy's onDeath is invoked. Increments enemiesKilled and calls CheckWin.*/
        {
            enemiesKilled++;
            CheckWin();
        }

        private void CheckWin() /*Checks if the player has won and updates the UI. If enemiesKilled is equal to enemyCount, call Win.*/
        {
            if (enemiesKilled == enemyCount)
            {
                Win();
            }
            UpdateUI();
        }

        private void UpdateUI() /*Update the mission text.*/
        {
            gameUI.UpdateMissionText(enemiesKilled, enemyCount);
        }

        private void Win() /*This function is called when the player wins the game. Finds the OptionalAssignmentHandler and calls CaptureData and SetLevelWon(true), unlocks the cursor, finds the SceneHandler, and loads the game-over scene.*/
        {
            OptionalAssignmentHandler optionalAssignmentHandler = FindObjectOfType<OptionalAssignmentHandler>();
            optionalAssignmentHandler.CaptureData();
            optionalAssignmentHandler.SetLevelWon(true);
            Cursor.lockState = CursorLockMode.None;
            SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();
            sceneHandler.LoadScene(sceneHandler.gameOverSceneIndex, changeSceneOnWinDelay);
        }
    }
}