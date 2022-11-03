using System;
using UnityEngine;

namespace Starborne.Saving
{
    [Serializable]
    public class Character /*Class that contains all the data for a playable character.*/
    {
        public string name; /*Name of the character*/
        public float maxHP; /*Max health of the character*/
        public float shotsPerSecond; /*Amount of shots each gun can fire per second.*/
        public float damagePerShot; /*Amount of damage each projectile deals.*/
        public float enginePower; /*Max amount of engine power of the character.*/
        public float dampeningSpeedChange; /*How quickly the character's speed will change due to dampening.*/
        public float maxSpeed; /*Max speed of the character.*/
        public float baseSpeed; /*The speed that the character will accelerate towards when its MovementType is MovementType.dampenedBaseSpeed.*/
        public float rollSensitivity; /*The sensitivity with which the player rolls on its Z-axis.*/
        public float pitchSensitivity; /*The sensitivity with which the player rolls on its X-axis.*/
        public float yawSensitivity; /*The sensitivity with which the player rolls on its Y-axis.*/
        public float rollEffectivenessAtMaxSpeed; /*How effectively the character can rotate on its Z-axis when moving at top speed. If this value is between 0 and 1, the character will rotate more slowly at higher speeds.*/
        public float pitchEffectivenessAtMaxSpeed; /*How effectively the character can rotate on its X-axis when moving at top speed. If this value is between 0 and 1, the character will rotate more slowly at higher speeds.*/
        public float yawEffectivenessAtMaxSpeed; /*How effectively the character can rotate on its Y-axis when moving at top speed. If this value is between 0 and 1, the character will rotate more slowly at higher speeds.*/
        public string meshFileName; /*Name of the mesh file.*/
        public string materialFileName; /*Name of the material file.*/
        public Vector3 meshScale; /*How the mesh should be scaled.*/
        public Vector3[] gunPositions; /*The local position where the guns will be instantiated.*/
    }
}