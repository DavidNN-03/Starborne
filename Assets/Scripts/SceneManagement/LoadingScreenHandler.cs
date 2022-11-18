using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using Starborne.Containers;

namespace Starborne.SceneHandling
{
    public class LoadingScreenHandler : MonoBehaviour /*Class that manages the Loading Screen scene.*/
    {
        [SerializeField] private float minDelay; /*The Loading Screen scene will last for minimum this amount of seconds.*/
        [SerializeField] private float maxDelay; /*The Loading Screen scene will last for maximum this amount of seconds.*/

        private IEnumerator Start() /*Adds some text the screen, and loads the Game scene after a random number of seconds between minDelay and maxDelay.*/
        {
            TextMeshProUGUI text = FindObjectOfType<TextMeshProUGUI>();

            string pathsPath = "Assets/Resources/Tips.json";
            StreamReader streamReader = new StreamReader(pathsPath);
            string jPaths = streamReader.ReadToEnd();
            ArrayContainer arrayContainer = JsonUtility.FromJson<ArrayContainer>(jPaths);

            /*
            string pathsPath = "Tips";
            var jPaths = Resources.Load<TextAsset>(pathsPath);
            ArrayContainer arrayContainer = JsonUtility.FromJson<ArrayContainer>(jPaths.text);
            */

            text.text = arrayContainer.array[Random.Range(0, arrayContainer.array.Length)];

            SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            sceneHandler.LoadScene(sceneHandler.gameSceneIndex);
        }
    }
}