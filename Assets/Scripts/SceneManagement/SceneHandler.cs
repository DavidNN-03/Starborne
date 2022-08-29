using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Starborne.SceneHandling
{
    public class SceneHandler : MonoBehaviour
    {
        public int mainMenuSceneIndex;
        public int charSelectSceneIndex;
        public int levelSelectSceneIndex;
        public int levelOneSceneIndex;
        public int levelTwoSceneIndex;
        public int creditsSceneIndex;

        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public void LoadScene(int index, float delay)
        {
            StartCoroutine(LoadSceneAfterSeconds(index, delay));
        }

        IEnumerator LoadSceneAfterSeconds(int index, float delay)
        {
            yield return new WaitForSeconds(delay);
            LoadScene(index);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}