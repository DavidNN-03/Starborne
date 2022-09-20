using System;

[Serializable]
public class SecondaryAssignment
{
    public enum AssignmentType
    {
        completeWithHealth,
        completeWithinSeconds
    }

    public AssignmentType assignmentType;
    public float value;
    public bool completed;
}
