using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.SceneHandling
{
    public class CreditsSceneEnder : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EndScene();
            }
        }

        private void OnTriggerEnter2D()
        {
            EndScene();
        }

        private void EndScene()
        {
            SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();
            sceneHandler.LoadScene(sceneHandler.mainMenuSceneIndex);
        }
    }
}