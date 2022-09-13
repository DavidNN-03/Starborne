using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Starborne.Saving
{
    public class SceneDataHandler : MonoBehaviour
    {
        [SerializeField] string defaultScenePath = "Assets/Resources/Scenes/LVL2.json";
        string sceneDataPath;
        SceneData sceneData = null;
        private bool sceneAssigned = false;

        public void SetSceneData(SceneData newSceneData, string path)
        {
            sceneData = newSceneData;
            sceneAssigned = true;
            sceneDataPath = path;
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

        public void SetStars(int count)
        {
            sceneData.stars = count;
            SaveSceneData();
        }

        private void SaveSceneData()
        {
            string json = JsonUtility.ToJson(sceneData);
            string path = "";

            if (sceneAssigned)
            {
                path = sceneDataPath;
            }
            else
            {
                path = defaultScenePath;
            }

            StreamWriter t = new StreamWriter(path, false);
            t.Write(json);
            t.Close();
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