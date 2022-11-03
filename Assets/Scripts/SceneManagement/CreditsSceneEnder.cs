using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.SceneHandling
{
    public class CreditsSceneEnder : MonoBehaviour /*Class that ends the Credits scene.*/
    {
        void Update() /*If the player presses space, end the scene.*/
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EndScene();
            }
        }

        private void OnTriggerEnter2D() /*Ends the scene when the collider at the bottom of the credits hits the collider at the top of the screen.*/
        {
            EndScene();
        }

        private void EndScene() /*Loads the Main Menu scene.*/
        {
            SceneHandler sceneHandler = FindObjectOfType<SceneHandler>();
            sceneHandler.LoadScene(sceneHandler.mainMenuSceneIndex);
        }
    }
}