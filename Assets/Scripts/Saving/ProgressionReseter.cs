using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Starborne.Containers;

namespace Starborne.Saving
{
    public class ProgressionReseter : MonoBehaviour /*Class that allows the player's progress to be reset.*/
    {
        private ArrayContainer arrayContainer; /*ArrayContainer that contains an array with the paths to all the levels.*/

        private async void Start() /*Gets the value for arrayContainer, change the assignments to be marked incomplete, and save the data.*/
        {
            string pathsPath = "Assets/Resources/ScenePaths.json";
            StreamReader streamReader = new StreamReader(pathsPath);
            string jPaths = await streamReader.ReadToEndAsync();
            arrayContainer = JsonUtility.FromJson<ArrayContainer>(jPaths);

            for (int i = 0; i < arrayContainer.array.Length; i++)
            {
                StreamReader reader = new StreamReader(arrayContainer.array[i]);
                string jscene = await reader.ReadToEndAsync();
                SceneData sceneData = JsonUtility.FromJson<SceneData>(jscene);
                reader.Close();

                sceneData.assignments.x.completed = false;
                sceneData.assignments.y.completed = false;
                sceneData.assignments.z.completed = false;

                string json = JsonUtility.ToJson(sceneData);

                string path = arrayContainer.array[i];

                StreamWriter t = new StreamWriter(path, false);
                await t.WriteAsync(json);
                t.Close();
            }
        }
    }
}