using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CharacterPathsWriter : MonoBehaviour
{
    [SerializeField] string[] paths;

    void Start()
    {
        ArrayContainer arrayContainer = new ArrayContainer();
        arrayContainer.array = paths;

        Debug.Log(arrayContainer);
        string json = JsonUtility.ToJson(arrayContainer);
        Debug.Log(arrayContainer);
        string path = "Assets/Resources/Paths.json";

        StreamWriter t = new StreamWriter(path, true);
        t.Write(json);
        t.Close();
    }
}
