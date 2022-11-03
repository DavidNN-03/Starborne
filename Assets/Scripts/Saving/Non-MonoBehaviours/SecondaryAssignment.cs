using System;

namespace Starborne.Saving
{
    [Serializable]
    public class SecondaryAssignment /*Class that stores the data for optional assignments.*/
    {
        public AssignmentType assignmentType; /*The type of assignment.*/
        public float value; /*The value that the must or must not meet or exceed.*/
        public bool completed; /*Whether or not the assignment has been completed.*/
    }
}