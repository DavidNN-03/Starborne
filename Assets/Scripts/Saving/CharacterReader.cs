using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Starborne.Saving
{
    public class CharacterReader : MonoBehaviour /*Basic code showing how to read and typecast a JSON-file*/
    {
        public Character GetCharacter(string path) /*Gets the character at a given path*/
        {
            StreamReader reader = new StreamReader(path);
            string jcharacter = reader.ReadToEnd();
            Character character = JsonUtility.FromJson<Character>(jcharacter);
            return character;
        }
    }
}