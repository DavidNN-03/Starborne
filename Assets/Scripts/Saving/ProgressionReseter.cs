using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Threading.Tasks;

public class ProgressionReseter : MonoBehaviour
{
    SceneData sceneData;
    ArrayContainer arrayContainer;

    private async void Start()
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