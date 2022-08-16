using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Starborne.Saving
{
    public class CharacterWriter : MonoBehaviour
    {
        [SerializeField] string fileName;
        [SerializeField] string charName;
        [SerializeField] float maxHP;
        [SerializeField] float shotsPerSecond;
        [SerializeField] float damagePerShot;
        [SerializeField] float enginePower;
        [SerializeField] float maxSpeed;
        [SerializeField] float turnSensitivity;

        Character currentCharacter = new Character();

        void Start()
        {
            currentCharacter.name = charName;
            currentCharacter.maxHP = maxHP;
            currentCharacter.shotsPerSecond = shotsPerSecond;
            currentCharacter.damagePerShot = damagePerShot;
            currentCharacter.enginePower = enginePower;
            currentCharacter.maxSpeed = maxSpeed;
            currentCharacter.turnSensitivity = turnSensitivity;

            string json = JsonUtility.ToJson(currentCharacter);
            string path = "Assets/Resources/" + fileName + ".json";

            StreamWriter t = new StreamWriter(path, true);
            t.Write(json);
            t.Close();
        }
    }
}