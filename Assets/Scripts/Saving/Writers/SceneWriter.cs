using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Starborne.Saving
{
    public class SceneWriter : MonoBehaviour
    {
        [SerializeField] string fileName;
        [SerializeField] TransformContainer playerTransform;
        [SerializeField] TransformContainer[] asteroidTransforms;
        [SerializeField] TransformContainer[] dreadnoughtTransforms;

        SceneData sceneData = new SceneData();

        void Start()
        {
            //sceneData.playerTransform = playerTransform;
            //sceneData.asteroidTransforms = asteroidTransforms;
            //sceneData.dreadnoughtTransforms = dreadnoughtTransforms;

            string json = JsonUtility.ToJson(sceneData);
            string path = "Assets/Resources/Scenes/" + fileName + ".json";

            StreamWriter t = new StreamWriter(path, false);
            t.Write(json);
            t.Close();
        }
    }
}