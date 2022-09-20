using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Starborne.Mission;
using Starborne.Core;
using Starborne.Saving;
using System;
using UnityEngine.UI;

namespace Starborne.Control
{
    public class GameOverSceneHandler : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI sceneNameText;
        [SerializeField] Image[] starsImages;
        [SerializeField] TextMeshProUGUI[] texts;
        [SerializeField] Sprite starSprite;
        [SerializeField] Color successColor;
        [SerializeField] Sprite noStarSprite;
        [SerializeField] Color failureColor;

        SceneData sceneData;
        OptionalAssignmentHandler optionalAssignmentHandler;

        void Awake()
        {
            optionalAssignmentHandler = EssentialObjects.instance.GetComponentInChildren<OptionalAssignmentHandler>();
            sceneData = EssentialObjects.instance.GetComponentInChildren<SceneDataHandler>().GetSceneData();
        }

        void Start()
        {
            sceneNameText.text = sceneData.sceneName;

            AssignmentsContainer assignments = optionalAssignmentHandler.GetAssignments();

            float maxHealth = optionalAssignmentHandler.GetMaxHealth();
            float endHealth = optionalAssignmentHandler.GetHealth();
            float sceneDuration = optionalAssignmentHandler.GetSceneDuration();

            bool levelWon = optionalAssignmentHandler.GetLevelWon();

            if (!levelWon) return;

            CheckMission(assignments.x, starsImages[0], texts[0], maxHealth, endHealth, sceneDuration);
            CheckMission(assignments.y, starsImages[1], texts[1], maxHealth, endHealth, sceneDuration);
            CheckMission(assignments.z, starsImages[2], texts[2], maxHealth, endHealth, sceneDuration);
            FindObjectOfType<SceneDataHandler>().SaveSceneData();
        }

        private void CheckMission(SecondaryAssignment assignment, Image image, TextMeshProUGUI text, float maxHealth, float endHealth, float sceneDuration)
        {
            if (assignment.assignmentType == SecondaryAssignment.AssignmentType.completeWithHealth)
            {
                if (endHealth / maxHealth >= assignment.value) //won
                {
                    image.sprite = starSprite;
                    image.color = successColor;
                    assignment.completed = true;
                }
                else //lost
                {
                    image.sprite = noStarSprite;
                    image.color = failureColor;
                }
                text.text = "Complete level with " + assignment.value * 100 + "% health";
            }
            else if (assignment.assignmentType == SecondaryAssignment.AssignmentType.completeWithinSeconds)
            {
                if (sceneDuration <= assignment.value) //win
                {
                    image.sprite = starSprite;
                    image.color = successColor;
                    assignment.completed = true;
                }
                else //won
                {
                    image.sprite = noStarSprite;
                    image.color = failureColor;
                }
                text.text = "Complete within " + assignment.value + " seconds";
            }
        }
    }
}