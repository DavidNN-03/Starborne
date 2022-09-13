using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.SceneHandling;
using System.IO;
using Starborne.Saving;

namespace Starborne.Control
{
    public class LevelSelectButton : MonoBehaviour
    {
        string scenePath;

        public void SetScenePath(string newScenePath)
        {
            scenePath = newScenePath;
        }

        public string GetScenePath()
        {
            return scenePath;
        }

        public void SendPathToSceneDataHandler()
        {
            FindObjectOfType<SceneDataHandler>().SetSceneData(scenePath);
            SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();
            sceneHandler.LoadScene(sceneHandler.gameSceneIndex);
        }
    }
}