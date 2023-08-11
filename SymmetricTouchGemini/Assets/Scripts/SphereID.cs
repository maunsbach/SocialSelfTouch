using UnityEngine;

public enum HandLimbs
{
    Thumb,
    Index,
    Middle,
    Ring,
    Pinky,
    Palm
}

public enum HandJoints
{
    a,
    b,
    c,
    surface
}
public class SphereID : MonoBehaviour
{
    [field: SerializeField]
    public HandLimbs HandLimb{ get; private set; }

    [field: SerializeField]
    public HandJoints HandJoint { get; private set; }

    [field: SerializeField]
    public int Column { get; private set; }

    [field: SerializeField] 
    public int Row { get; private set; }

    public void SetValues(HandLimbs handLimb, HandJoints joint, int column, int row)
    {
        HandLimb = handLimb;
        HandJoint = joint;
        Column = column;
        Row = row;
    }
}
