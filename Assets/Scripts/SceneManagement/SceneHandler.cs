using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Starborne.SceneHandling
{
    public class SceneHandler : MonoBehaviour /*Class that handles the loading of scenes and quitting the game.*/
    {
        public int mainMenuSceneIndex; /*Build index of the Main Menu scene.*/
        public int charSelectSceneIndex; /*Build index of the Character select scene.*/
        public int levelSelectSceneIndex; /*Build index of the Level Select scene.*/
        public int loadingSceneIndex; /*Build index of the Loading Screen scene.*/
        public int gameSceneIndex; /*Build index of the Game scene.*/
        public int gameOverSceneIndex; /*Build index of the Game Over scene.*/
        public int creditsSceneIndex; /*Build index of the Credits scene.*/

        public void LoadScene(int index) /*Loads the scene of a given build index.*/
        {
            SceneManager.LoadScene(index);
        }

        public void LoadScene(int index, float delay) /*Use LoadSceneAfterSeconds to load the scene of a given build index after a given amount of seconds.*/
        {
            StartCoroutine(LoadSceneAfterSeconds(index, delay));
        }

        private IEnumerator LoadSceneAfterSeconds(int index, float delay) /*Load the scene of a given build index after a given amount of seconds.*/
        {
            yield return new WaitForSeconds(delay);
            LoadScene(index);
        }

        public void QuitGame() /*Quit the application.*/
        {
            Application.Quit();
        }
    }
}