using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Starborne.Saving
{
    public class CharacterHandler : MonoBehaviour
    {
        [SerializeField] string defaultCharacterPath = "Assets/Resources/fighterLight.json";
        public Character characterStats = null; //Unity automatically creates an empty character, so a null-check doesnt work?
        private bool characterAssigned = false;

        public void SetCharacterStats(Character character)
        {
            characterStats = character;
            characterAssigned = true;
        }

        public Character GetCharacterStats()
        {
            if (characterAssigned)
            {
                //return assigned character
                return characterStats;
            }

            //return default character
            StreamReader reader = new StreamReader(defaultCharacterPath);
            string jcharacter = reader.ReadToEnd();
            characterStats = JsonUtility.FromJson<Character>(jcharacter);
            return characterStats;
        }
    }
}