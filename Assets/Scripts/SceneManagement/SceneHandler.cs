using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public int mainMenuSceneIndex;
    public int charSelectSceneIndex;
    public int levelSelectSceneIndex;
    public int levelOneSceneIndex;

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
