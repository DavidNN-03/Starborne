using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterWriter : MonoBehaviour
{
    [SerializeField] string fileName;
    [SerializeField] string charName;
    [SerializeField] float maxHP;
    [SerializeField] float shotsPerSecond;
    [SerializeField] float damagePerShot;
    [SerializeField] float speed;
    [SerializeField] float turnSensitivity;

    Character currentCharacter = new Character();

    void Start()
    {
        currentCharacter.name = charName;
        currentCharacter.maxHP = maxHP;
        currentCharacter.shotsPerSecond = shotsPerSecond;
        currentCharacter.damagePerShot = damagePerShot;
        currentCharacter.speed = speed;
        currentCharacter.turnSensitivity = turnSensitivity;

        string json = JsonUtility.ToJson(currentCharacter);
        string path = "Assets/Resources/" + fileName + ".json";

        StreamWriter t = new StreamWriter(path, true);
        t.Write(json);
        t.Close();
    }
}
