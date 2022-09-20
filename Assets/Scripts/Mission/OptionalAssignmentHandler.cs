using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.Core;
using Starborne.Saving;
using Starborne.GameResources;

namespace Starborne.Mission
{
    public class OptionalAssignmentHandler : MonoBehaviour, ILateInit
    {
        float maxHealth;
        float endHealth;
        float startTime;
        float winTime;
        bool levelWon;

        AssignmentsContainer assignments;

        public void LateAwake()
        {
            assignments = EssentialObjects.instance.GetComponentInChildren<SceneDataHandler>().GetSceneData().assignments;
        }

        public void LateStart()
        {
            startTime = Time.time;
        }

        public void CaptureData()
        {
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            endHealth = playerHealth.GetHealth();
            maxHealth = playerHealth.GetMaxHealth();
            winTime = Time.time - startTime;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }


        public float GetHealth()
        {
            return endHealth;
        }

        public float GetSceneDuration()
        {
            return winTime;
        }

        public AssignmentsContainer GetAssignments()
        {
            return assignments;
        }

        public bool GetLevelWon()
        {
            return levelWon;
        }

        public void SetLevelWon(bool state)
        {
            levelWon = state;
        }
    }
}