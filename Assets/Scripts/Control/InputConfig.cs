using UnityEngine;

[CreateAssetMenu(fileName = "InputConfig", menuName = "ScriptbleObjects/InputConfig")]
public class InputConfig : ScriptableObject
{
    public InputSet throttleInputSet;
    public bool inverThrottle = false;
    public InputSet rollInputSet;
    public bool invertRoll = false;
    public InputSet pitchInputSet;
    public bool invertPitch = false;
    public InputSet yawInputSet;
    public bool invertYaw = false;
}
