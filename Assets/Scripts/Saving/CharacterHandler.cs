using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Starborne.Saving
{
    public class CharacterHandler : MonoBehaviour /*Class that stores the selected character. This component should be placed on the EssentialObjects.instance GameObject or one of its children.*/
    {
        [SerializeField] private string defaultCharacterPath = "Characters/fighterLight.json"; /*This character will be returned if no character has been assigned by the player.*/
        public Character characterStats = null; /*Stores the player's selected character.*/
        private bool characterAssigned = false; /*Whether or not the player has selected a character.*/

        public void SetCharacterStats(Character character) /*Set characterStats to a given value.*/
        {
            characterStats = character;
            characterAssigned = true;
        }

        public Character GetCharacterStats() /*Returns characterStats it has been assigned. Otherwise, returns the default character.*/
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
            /*
            var jCharacter = Resources.Load<TextAsset>(defaultCharacterPath);
            characterStats = JsonUtility.FromJson<Character>(jCharacter.text);
            */
            return characterStats;
        }
    }
}