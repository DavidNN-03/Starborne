using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Starborne.Saving
{
    public class SceneDataHandler : MonoBehaviour
    {
        [SerializeField] string defaultScenePath = "Assets/Resources/Scenes/LVL2.json";
        public SceneData sceneData = null;
        private bool sceneAssigned = false;

        public void SetSceneData(SceneData newSceneData)
        {
            sceneData = newSceneData;
            sceneAssigned = true;
        }

        public SceneData GetSceneData()
        {
            if (sceneAssigned)
            {
                //return assigned character
                return sceneData;
            }

            //return default character
            StreamReader reader = new StreamReader(defaultScenePath);
            string jscene = reader.ReadToEnd();
            sceneData = JsonUtility.FromJson<SceneData>(jscene);
            return sceneData;
        }
    }
}