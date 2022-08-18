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
        [SerializeField] float rollSensitivity;
        [SerializeField] float pitchSensitivity;
        [SerializeField] float yawSensitivity;

        Character currentCharacter = new Character();

        void Start()
        {
            currentCharacter.name = charName;
            currentCharacter.maxHP = maxHP;
            currentCharacter.shotsPerSecond = shotsPerSecond;
            currentCharacter.damagePerShot = damagePerShot;
            currentCharacter.enginePower = enginePower;
            currentCharacter.maxSpeed = maxSpeed;
            currentCharacter.rollSensitivity = rollSensitivity;
            currentCharacter.pitchSensitivity = pitchSensitivity;
            currentCharacter.yawSensitivity = yawSensitivity;

            string json = JsonUtility.ToJson(currentCharacter);
            string path = "Assets/Resources/" + fileName + ".json";

            StreamWriter t = new StreamWriter(path, false);
            t.Write(json);
            t.Close();
        }
    }
}