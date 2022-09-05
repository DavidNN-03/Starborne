using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.SceneHandling;

namespace Starborne.Control
{
    public class LevelSelectButton : MonoBehaviour
    {
        int levelIndex;
        SceneHandler sceneHandler;

        void Awake()
        {
            sceneHandler = FindObjectOfType<SceneHandler>();
        }

        public void SetLevelIndex(int i)
        {
            levelIndex = i;
        }

        void Press()
        {
            sceneHandler = FindObjectOfType<SceneHandler>();

            //sceneHandler.
        }
    }
}