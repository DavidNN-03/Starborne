using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace Starborne.Saving
{
    public class CharacterPathsWriter : MonoBehaviour
    {
        [SerializeField] string[] paths;

        void Start()
        {
            ArrayContainer arrayContainer = new ArrayContainer();
            arrayContainer.array = paths;

            string json = JsonUtility.ToJson(arrayContainer);
            string path = "Assets/Resources/Paths.json";

            StreamWriter t = new StreamWriter(path, false);
            t.Write(json);
            t.Close();
        }
    }
}