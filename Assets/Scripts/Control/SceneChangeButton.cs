using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Starborne.SceneHandling;

namespace Starborne.Control
{
    public class SceneChangeButton : MonoBehaviour
    {
        public void LoadScene(int sceneBuildIndex)
        {
            FindObjectOfType<SceneHandler>().LoadScene(sceneBuildIndex);
        }
    }
}