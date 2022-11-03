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
    public class GameOverSceneHandler : MonoBehaviour /*Class that manages the game-over-scene. The class will display the name of the level that was played, the optional assignments and whether or not they were completed.*/
    {
        [SerializeField] private TextMeshProUGUI sceneNameText; /*Text that will display the name of the played level.*/
        [SerializeField] private Image[] starsImages; /*Array of the images that show whether or not the belonging optional assignment was completed.*/
        [SerializeField] private TextMeshProUGUI[] texts; /*Array of the texts that will display the level's optional assignments.*/
        [SerializeField] private Sprite starSprite; /*The sprite that will be displayed if an optional assignment was completed.*/
        [SerializeField] private Color successColor; /*The color of the starSprite if the optional assignment was completed*/
        [SerializeField] private Sprite noStarSprite; /*The sprite that will be displayed if an optional assignment was failed.*/
        [SerializeField] private Color failureColor; /*The color of the starSprite if the optional assignment was failed*/

        private SceneData sceneData; /*The played level's SceneData.*/
        private OptionalAssignmentHandler optionalAssignmentHandler; /*The OptionalAssignmentHandler that contains whether the level was completed, the OptionalAssignentsContainer, the amount of time the player spend in the level, the max health of the player, and the amount of health the player had left.*/

        private void Awake() /*Find the references for optionalAssignmentHandler and sceneData. These will be on descendants of the GameObject referenced in EssentialObjects.instance.*/
        {
            optionalAssignmentHandler = EssentialObjects.instance.GetComponentInChildren<OptionalAssignmentHandler>();
            sceneData = EssentialObjects.instance.GetComponentInChildren<SceneDataHandler>().GetSceneData();
        }

        private void Start() /*Display the name of the level, get the AssignmentsContainer, max health, final health, time spend in the level, and whether or not the level was completed. If the level was not completed, disactivate all the star images and the texts and return. Otherwise, call CheckMission with every optional assignment and save the scene data by finding the SceneDataHandler and calling SaveSceneData.*/
        {
            sceneNameText.text = sceneData.sceneName;

            AssignmentsContainer assignments = optionalAssignmentHandler.GetAssignments();

            float maxHealth = optionalAssignmentHandler.GetMaxHealth();
            float endHealth = optionalAssignmentHandler.GetHealth();
            float sceneDuration = optionalAssignmentHandler.GetGameDuration();

            bool levelWon = optionalAssignmentHandler.GetLevelWon();

            if (!levelWon)
            {
                for (int i = 0; i < texts.Length; i++)
                {
                    starsImages[i].gameObject.SetActive(false);
                    texts[i].gameObject.SetActive(false);
                }

                return;
            }

            CheckMission(assignments.x, starsImages[0], texts[0], maxHealth, endHealth, sceneDuration);
            CheckMission(assignments.y, starsImages[1], texts[1], maxHealth, endHealth, sceneDuration);
            CheckMission(assignments.z, starsImages[2], texts[2], maxHealth, endHealth, sceneDuration);
            FindObjectOfType<SceneDataHandler>().SaveSceneData();
        }

        private void CheckMission(SecondaryAssignment assignment, Image image, TextMeshProUGUI text, float maxHealth, float endHealth, float sceneDuration) /*Checks whether or not the mission was completed, display the result, and set the sprite and color of the stara images accordingly.*/
        {
            if (assignment.assignmentType == AssignmentType.completeWithHealth)
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
            else if (assignment.assignmentType == AssignmentType.completeWithinSeconds)
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