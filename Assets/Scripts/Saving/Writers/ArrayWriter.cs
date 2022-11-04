using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Starborne.Containers;

namespace Starborne.Saving
{
    public class ArrayWriter : MonoBehaviour /*Class that writes a JSSON-file containing an object with an array of strings.*/
    {
        [SerializeField] private string path; /*The path of the file that should be created.*/
        [SerializeField] private string[] elements; /*The array of strings that should be saved.*/

        private void Start() /*Create an ArrayContainer and add the elements array. Convert the object to JSON and write it to the new file.*/
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