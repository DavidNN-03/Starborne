namespace Starborne.Saving
{
    public enum AssignmentType /*Enum that offers different types of optional assignments.*/
    {
        completeWithHealth, /*The game must be completed with a certain fraction of health remaining.*/
        completeWithinSeconds /*The level must be completed with in a given amount of seconds.*/
    }
}