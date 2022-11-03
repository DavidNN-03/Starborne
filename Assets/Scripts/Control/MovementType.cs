public enum MovementType /*Different types of movement and dampening*/
{
    noDampening, /*Player will continue forward with constant speed when no buttons are pressed.*/
    dampenedBaseSpeed, /*Player will raise/lower speed to a preset value when no buttons are pressed.*/
    dampenedStatic /*Player will lower speed to 0 when no buttons are pressed.*/

}
