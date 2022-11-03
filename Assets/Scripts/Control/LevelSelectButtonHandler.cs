using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using Starborne.Containers;
using Starborne.Saving;

namespace Starborne.Control
{
    public class LevelSelectButtonHandler : MonoBehaviour /*Instantiates and gives the level select buttons their values.*/
    {
        [SerializeField] private Transform buttonsParent; /*The parent of the instantiated level select buttons.*/
        [SerializeField] private GameObject buttonPrefab; /*Prefab of the level select button.*/
        [SerializeField] private float widthBetweenButtons; /*The distance between the centers of consecutive level select buttons on the X-axis.*/
        [SerializeField] private float startXPos; /*The position of the X-axis of the first level select button.*/
        [SerializeField] private float minHeight; /*The min value of the level select buttons' position on the Y-axis.*/
        [SerializeField] private float maxHeight; /*The max value of the level select buttons' position on the Y-axis.*/

        private void Start() /*Get all the paths to the scenes' JSON-files, get all the scenes' JSON-files, instantiate the level select buttons, and set their values.*/
        {
            string pathsPath = "Assets/Resources/ScenePaths.json";
            StreamReader streamReader = new StreamReader(pathsPath);
            string jPaths = streamReader.ReadToEnd();
            ArrayContainer arrayContainer = JsonUtility.FromJson<ArrayContainer>(jPaths);

            int previousStarCount = 0;

            for (int i = 0; i < arrayContainer.array.Length; i++)
            {
                StreamReader reader = new StreamReader(arrayContainer.array[i]);
                string jscene = reader.ReadToEnd();
                SceneData sceneData = JsonUtility.FromJson<SceneData>(jscene);

                int stars = GetStars(sceneData);
                bool isUnlocked = i == 0 || previousStarCount > 0;

                Vector3 spawnPos = new Vector3(startXPos + widthBetweenButtons * i, Random.Range(minHeight, maxHeight), 0);
                GameObject g = Instantiate(buttonPrefab, spawnPos, Quaternion.identity, buttonsParent);
                LevelSelectButton levelSelectButton = g.GetComponent<LevelSelectButton>();
                levelSelectButton.SetScenePath(arrayContainer.array[i]);
                levelSelectButton.SetStarCount(stars);
                levelSelectButton.SetUnlocked(isUnlocked);
                g.GetComponentInChildren<TextMeshProUGUI>().text = sceneData.sceneName;
                previousStarCount = stars;
            }
        }

        private int GetStars(SceneData sceneData) /*Get the amount of stars unlocked in a given scene.*/
        {
            int stars = 0;

            if (sceneData.assignments.x.completed) stars++;
            if (sceneData.assignments.y.completed) stars++;
            if (sceneData.assignments.z.completed) stars++;

            return stars;
        }
    }
}