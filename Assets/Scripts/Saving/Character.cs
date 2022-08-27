using System;
using UnityEngine;

[Serializable]
public class Character
{
    public string name;
    public float maxHP;
    public float shotsPerSecond;
    public float damagePerShot;
    public float enginePower;
    public float dampeningSpeedChange;
    public float maxSpeed;
    public float baseSpeed;
    public float rollSensitivity;
    public float pitchSensitivity;
    public float yawSensitivity;
    public string meshFileName;
    public string materialFileName;
    public Vector3 meshScale;
    public Vector3[] gunPositions;
}