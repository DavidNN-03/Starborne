using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Starborne.Saving
{
    public class CharacterReader : MonoBehaviour
    {
        public Character GetCharacter(string path)
        {
            StreamReader reader = new StreamReader(path);
            string jcharacter = reader.ReadToEnd();
            Character character = JsonUtility.FromJson<Character>(jcharacter);
            return character;
        }
    }
}