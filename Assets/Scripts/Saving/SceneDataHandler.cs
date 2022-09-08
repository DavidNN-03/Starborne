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

        public void SetSceneData(string sceneDataPath)
        {
            sceneData = LoadSceneData(sceneDataPath);

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
            sceneData = LoadSceneData(defaultScenePath);

            return sceneData;
        }

        private SceneData LoadSceneData(string path)
        {
            StreamReader reader = new StreamReader(path);
            string jscene = reader.ReadToEnd();
            sceneData = JsonUtility.FromJson<SceneData>(jscene);
            return sceneData;
        }
    }
}