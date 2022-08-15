using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    Character characterStats;

    public void SetCharacterStats(Character character)
    {
        characterStats = character;
    }

    public Character GetCharacterStats()
    {
        return characterStats;
    }
}
