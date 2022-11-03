using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Starborne.Saving
{
    public class CharacterWriter : MonoBehaviour /*Class that writes a JSON-file with data for a playable character.*/
    {
        [SerializeField] private string fileName; /*Name of the file to be created.*/
        [SerializeField] private string charName; /*Name of the character*/
        [SerializeField] private float maxHP; /*Max health of the character*/
        [SerializeField] private float shotsPerSecond; /*Amount of shots each gun can fire per second.*/
        [SerializeField] private float damagePerShot; /*Amount of damage each projectile deals.*/
        [SerializeField] private float enginePower; /*Max amount of engine power of the character.*/
        [SerializeField] private float dampeningSpeedChange; /*How quickly the character's speed will change due to dampening.*/
        [SerializeField] private float maxSpeed; /*Max speed of the character.*/
        [SerializeField] private float baseSpeed; /*The speed that the character will accelerate towards when its MovementType is MovementType.dampenedBaseSpeed.*/
        [SerializeField] private float rollSensitivity; /*The sensitivity with which the player rolls on its Z-axis.*/
        [SerializeField] private float pitchSensitivity; /*The sensitivity with which the player rolls on its X-axis.*/
        [SerializeField] private float yawSensitivity; /*The sensitivity with which the player rolls on its Y-axis.*/
        [SerializeField] private float rollEffectivenessAtMaxSpeed; /*How effectively the character can rotate on its Z-axis when moving at top speed. If this value is between 0 and 1, the character will rotate more slowly at higher speeds.*/
        [SerializeField] private float pitchEffectivenessAtMaxSpeed; /*How effectively the character can rotate on its X-axis when moving at top speed. If this value is between 0 and 1, the character will rotate more slowly at higher speeds.*/
        [SerializeField] private float yawEffectivenessAtMaxSpeed; /*How effectively the character can rotate on its Y-axis when moving at top speed. If this value is between 0 and 1, the character will rotate more slowly at higher speeds.*/
        [SerializeField] private string meshFileName; /*Name of the mesh file.*/
        [SerializeField] private string materialFileName; /*Name of the material file.*/
        [SerializeField] private Vector3 meshScale; /*How the mesh should be scaled.*/
        [SerializeField] private Vector3[] gunPositions; /*The local position where the guns will be instantiated.*/

        private Character currentCharacter = new Character(); /*The object that will contain the data. This object will be converted to JSON and saved to a file.*/

        private void Start() /*Add the data to currentCharacter, convert it to JSON, and save it to a file.*/
        {
            currentCharacter.name = charName;
            currentCharacter.maxHP = maxHP;
            currentCharacter.shotsPerSecond = shotsPerSecond;
            currentCharacter.damagePerShot = damagePerShot;
            currentCharacter.enginePower = enginePower;
            currentCharacter.dampeningSpeedChange = dampeningSpeedChange;
            currentCharacter.maxSpeed = maxSpeed;
            currentCharacter.baseSpeed = baseSpeed;
            currentCharacter.rollSensitivity = rollSensitivity;
            currentCharacter.pitchSensitivity = pitchSensitivity;
            currentCharacter.yawSensitivity = yawSensitivity;
            currentCharacter.rollEffectivenessAtMaxSpeed = rollEffectivenessAtMaxSpeed;
            currentCharacter.pitchEffectivenessAtMaxSpeed = pitchEffectivenessAtMaxSpeed;
            currentCharacter.yawEffectivenessAtMaxSpeed = yawEffectivenessAtMaxSpeed;
            currentCharacter.meshFileName = meshFileName;
            currentCharacter.materialFileName = materialFileName;
            currentCharacter.meshScale = meshScale;
            currentCharacter.gunPositions = gunPositions;

            string json = JsonUtility.ToJson(currentCharacter);
            string path = "Assets/Resources/Characters/" + fileName + ".json";

            StreamWriter t = new StreamWriter(path, false);
            t.Write(json);
            t.Close();
        }
    }
}