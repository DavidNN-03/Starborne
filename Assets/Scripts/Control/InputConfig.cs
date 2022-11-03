using UnityEngine;

namespace Starborne.Control
{
    [CreateAssetMenu(fileName = "InputConfig", menuName = "ScriptbleObjects/InputConfig")]
    public class InputConfig : ScriptableObject /*This class allows the designers to save and quickly swap between several input configurations. This is not possible in-game.*/
    {
        public InputSet throttleInputSet; /*The InputSet used to control the throttle.*/
        public bool invertThrottle = false; /*Whether or not the throttleInputSet should be inverted.*/
        public InputSet rollInputSet; /*The InputSet used to control the roll*/
        public bool invertRoll = false; /*Whether or not the rollInputSet should be inverted.*/
        public InputSet pitchInputSet; /*The InputSet used to control the pitch*/
        public bool invertPitch = false; /*Whether or not the pitchInputSet should be inverted.*/
        public InputSet yawInputSet; /*The InputSet used to control the yaw*/
        public bool invertYaw = false; /*Whether or not the yawInputSet should be inverted.*/
    }
}