using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

namespace Starborne.SceneHandling
{
    public class LoadingScreenHandler : MonoBehaviour
    {
        [SerializeField] float minDelay;
        [SerializeField] float maxDelay;

        IEnumerator Start()
        {
            TextMeshProUGUI text = FindObjectOfType<TextMeshProUGUI>();

            string pathsPath = "Assets/Resources/Tips.json";
            StreamReader streamReader = new StreamReader(pathsPath);
            string jPaths = streamReader.ReadToEnd();
            ArrayContainer arrayContainer = JsonUtility.FromJson<ArrayContainer>(jPaths);

            text.text = arrayContainer.array[Random.Range(0, arrayContainer.array.Length)];

            SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            sceneHandler.LoadScene(sceneHandler.gameSceneIndex);
        }
    }
}