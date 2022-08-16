using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public Character characterStats;

    void Start()
    {

    }

    public void SetCharacterStats(Character character)
    {
        characterStats = character;
    }

    public Character GetCharacterStats()
    {
        return characterStats;
    }
}
