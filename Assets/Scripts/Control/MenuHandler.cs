using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.SceneHandling;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] GameObject menu;

    void Start()
    {
        menu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeInHierarchy);
        }
    }

    public void Quit()
    {
        FindObjectOfType<SceneHandler>().QuitGame();
    }
}
