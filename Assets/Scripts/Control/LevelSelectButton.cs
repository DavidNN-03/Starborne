using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.SceneHandling;

namespace Starborne.Control
{
    public class LevelSelectButton : MonoBehaviour
    {
        SceneHandler sceneHandler;

        void Awake()
        {
            sceneHandler = FindObjectOfType<SceneHandler>();
        }

        public void LoadLevel(int level)
        {
            sceneHandler.LoadScene(level - 1 + sceneHandler.levelOneSceneIndex);
        }
    }
}