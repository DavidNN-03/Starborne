using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.SceneHandling;

namespace Starborne.Control
{
    public class MenuHandler : MonoBehaviour /*Class that handles the game's menu. this menu allows the player to leave the game.*/
    {
        [SerializeField] private GameObject menu; /*The parent GameObject of the menu.*/
        CursorLockMode defaultLockMode = CursorLockMode.None;

        private void Start() /*Disactivate the menu.*/
        {
            menu.SetActive(false);
        }

        private void Update() /*If the player pushes the escape key, toggle the menu.*/
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!menu.activeInHierarchy)
                {
                    defaultLockMode = Cursor.lockState;
                }

                bool newState = !menu.activeInHierarchy;
                menu.SetActive(newState);

                if (newState == true)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Cursor.lockState = defaultLockMode;
                }
            }
        }

        public void LoadScene(int buildIndex)
        {
            FindObjectOfType<SceneHandler>().LoadScene(buildIndex);
        }

        public void Quit() /*Close the game. This function is called when clicking the in-game buton.*/
        {
            FindObjectOfType<SceneHandler>().QuitGame();
        }
    }
}
