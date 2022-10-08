public enum MovementType
{
    noDampening, /*will continue forward with constant speed when no buttons are pressed*/
    dampenedBaseSpeed, /*will raise/lower speed to a preset value when no buttons are pressed*/
    dampenedStatic /*will lower speed to 0 when no buttons are pressed*/

}
