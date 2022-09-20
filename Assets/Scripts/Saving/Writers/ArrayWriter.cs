using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Starborne.Saving
{
    public class ArrayWriter : MonoBehaviour
    {
        [SerializeField] string path;
        [SerializeField] string[] elements;

        void Start()
        {
            ArrayContainer arrayContainer = new ArrayContainer();
            arrayContainer.array = elements;

            string json = JsonUtility.ToJson(arrayContainer);

            StreamWriter t = new StreamWriter(path, false);
            t.Write(json);
            t.Close();
        }
    }
}