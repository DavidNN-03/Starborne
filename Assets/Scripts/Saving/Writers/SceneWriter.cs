using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Starborne.Containers;

namespace Starborne.Saving
{
    public class SceneWriter : MonoBehaviour /*Class that writes the file for a game level. This class is obsolete.*/
    {
        [SerializeField] string fileName; /*Name of the file to be created.*/
        [SerializeField] string sceneName; /*Name of the level.*/
        [SerializeField] TransformContainer playerTransform; /*Where the player should be instantiated.*/
        [SerializeField] TransformContainer[] asteroidTransforms; /*Where the asteroids should be instantiated.*/
        [SerializeField] TransformContainer[] dreadnoughtTransforms; /*Where the dreadnoughts should be instantiated.*/

        SceneData sceneData = new SceneData(); /*The object that will contain the data.*/

        void Start() /*Add the data to sceneData, convert it to json, and save it to a file.*/
        {
            //sceneData.playerTransform = playerTransform;
            //sceneData.asteroidTransforms = asteroidTransforms;
            //sceneData.dreadnoughtTransforms = dreadnoughtTransforms;
            //sceneData.sceneName = sceneName; 

            string json = JsonUtility.ToJson(sceneData);
            string path = "Assets/Resources/Scenes/" + fileName + ".json";

            StreamWriter t = new StreamWriter(path, false);
            t.Write(json);
            t.Close();
        }
    }
}