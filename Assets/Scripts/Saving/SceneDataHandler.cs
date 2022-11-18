using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Starborne.Saving
{
    public class SceneDataHandler : MonoBehaviour /*Class that stores the selected level.*/
    {
        [SerializeField] private string defaultScenePath = "Assets/Resources/Scenes/LVL2.json"; /*Path of the level that will be loaded if a level isn't assigned*/
        private string sceneDataPath; /*Path to the selected level.*/
        private SceneData sceneData = null; /*SceneData of the selected level.*/
        private bool sceneAssigned = false; /*Whether or not a level has been selected.*/

        public void SetSceneData(string newSceneDataPath) /*Sets sceneDataPath to a given value.*/
        {
            sceneDataPath = newSceneDataPath;

            LoadSceneData(sceneDataPath);

            sceneAssigned = true;
        }

        public SceneData GetSceneData() /*Returns sceneData if it has been assigned a value. Otherwise, returns the default scene data.*/
        {
            if (sceneAssigned)
            {
                //return assigned character
                return sceneData;
            }

            //return default character
            LoadSceneData(defaultScenePath);

            return sceneData;
        }

        public void SaveSceneData() /*Saves the changes made to the scene data to the file it came from.*/
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

        private void LoadSceneData(string path) /*Sets sceneData to a the value of a JSON-file at the given path.*/
        {
            StreamReader reader = new StreamReader(path);
            string jScene = reader.ReadToEnd();
            sceneData = JsonUtility.FromJson<SceneData>(jScene);
            /*
            var jScene = Resources.Load<TextAsset>(path);
            sceneData = JsonUtility.FromJson<SceneData>(jScene.text);
            */
        }
    }
}