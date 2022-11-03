using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.SceneHandling;

namespace Starborne.Control
{
    public class MenuHandler : MonoBehaviour /*Class that handles the game's menu. this menu allows the player to leave the game.*/
    {
        [SerializeField] private GameObject menu; /*The parent GameObject of the menu.*/

        private void Start() /*Disactivate the menu.*/
        {
            menu.SetActive(false);
        }

        private void Update() /*If the player pushes the escape key, toggle the menu.*/
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menu.SetActive(!menu.activeInHierarchy);
            }
        }

        public void Quit() /*Close the game. This function is called when clicking the in-game buton.*/
        {
            FindObjectOfType<SceneHandler>().QuitGame();
        }
    }
}
