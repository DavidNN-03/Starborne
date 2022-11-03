using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.SceneHandling;

namespace Starborne.Control
{
    public class SceneChangeButton : MonoBehaviour /*Class that can be placed on a button. When the button is pushed, this class finds the SceneHandler and calls LaodScene to load the scene of a given build index.*/
    {
        public void LoadScene(int sceneBuildIndex) /*Load scene of a given build index.*/
        {
            FindObjectOfType<SceneHandler>().LoadScene(sceneBuildIndex);
        }
    }
}