using System.Collections.Generic;

[System.Serializable]
public class DataHand : Dictionary<HandLimbs, DataJoints>
{
    public DataHand () 
    {
        Add(HandLimbs.Index, new DataJoints());
        Add(HandLimbs.Middle, new DataJoints());
        Add(HandLimbs.Ring, new DataJoints());
        Add(HandLimbs.Pinky, new DataJoints());
        Add(HandLimbs.Thumb, new DataJoints());
        Add(HandLimbs.Palm, new DataJoints());
    }
}