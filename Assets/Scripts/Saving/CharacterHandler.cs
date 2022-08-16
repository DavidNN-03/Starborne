using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Starborne.Saving
{
    public class CharacterHandler : MonoBehaviour
    {
        public Character characterStats;

        public void SetCharacterStats(Character character)
        {
            characterStats = character;
        }

        public Character GetCharacterStats()
        {
            return characterStats;
        }
    }
}