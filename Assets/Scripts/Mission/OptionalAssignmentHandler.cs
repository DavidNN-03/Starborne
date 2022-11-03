using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.Core;
using Starborne.Saving;
using Starborne.GameResources;

namespace Starborne.Mission
{
    public class OptionalAssignmentHandler : MonoBehaviour, ILateInit /*Handles the optional assignments in the different levels. This class will be taken between scenes by the EssentialObjects class.*/
    {
        private float maxHealth; /*Max health of the player.*/
        private float endHealth; /*Health of the player when the level ended.*/
        private float startTime; /*Time when the game scene had been built.*/
        private float gameDuration; /*Time when the player completed the level.*/
        private bool levelWon; /*Whether or not the player completed the level.*/

        private AssignmentsContainer assignments; /*Contains the optional assignments.*/

        public void LateAwake() /*Get the assignments from the SceneDataHandler.*/
        {
            assignments = EssentialObjects.instance.GetComponentInChildren<SceneDataHandler>().GetSceneData().assignments;
        }

        public void LateStart() /*Get value for startTime.*/
        {
            startTime = Time.time;
        }

        public void CaptureData() /*Save data when the game is over.*/
        {
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            endHealth = playerHealth.GetHealth();
            maxHealth = playerHealth.GetMaxHealth();
            gameDuration = Time.time - startTime;
        }

        public float GetMaxHealth() /*Returns maxHealth.*/
        {
            return maxHealth;
        }


        public float GetHealth() /*Returns endHealth.*/
        {
            return endHealth;
        }

        public float GetGameDuration() /*Returns gameDuration.*/
        {
            return gameDuration;
        }

        public AssignmentsContainer GetAssignments() /*Returns assignments.*/
        {
            return assignments;
        }

        public bool GetLevelWon() /*Returns levelWon.*/
        {
            return levelWon;
        }

        public void SetLevelWon(bool state) /*Sets levelWon to a given value.*/
        {
            levelWon = state;
        }
    }
}