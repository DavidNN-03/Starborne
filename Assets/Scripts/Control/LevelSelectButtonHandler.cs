using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

namespace Starborne.Control
{
    public class LevelSelectButtonHandler : MonoBehaviour
    {
        [SerializeField] Transform buttonsParent;
        [SerializeField] GameObject buttonPrefab;
        [SerializeField] float widthBetweenButtons;
        [SerializeField] float startXPos;
        [SerializeField] float minHeight;
        [SerializeField] float maxHeight;

        void Start()
        {
            string pathsPath = "Assets/Resources/ScenePaths.json";
            StreamReader streamReader = new StreamReader(pathsPath);
            string jPaths = streamReader.ReadToEnd();
            ArrayContainer arrayContainer = JsonUtility.FromJson<ArrayContainer>(jPaths);

            Debug.Log(arrayContainer.array.Length);

            for (int i = 0; i < arrayContainer.array.Length; i++)
            {
                StreamReader reader = new StreamReader(arrayContainer.array[i]);
                string jscene = reader.ReadToEnd();
                SceneData sceneData = JsonUtility.FromJson<SceneData>(jscene);

                Vector3 spawnPos = new Vector3(startXPos + widthBetweenButtons * i, Random.Range(minHeight, maxHeight), 0);
                GameObject g = Instantiate(buttonPrefab, spawnPos, Quaternion.identity, buttonsParent);
                g.GetComponent<LevelSelectButton>().SetScenePath(arrayContainer.array[i]);
                g.GetComponentInChildren<TextMeshProUGUI>().text = sceneData.sceneName;
            }
        }
    }
}