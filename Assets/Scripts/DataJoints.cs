using System.Collections.Generic;

[System.Serializable]
public class DataJoints : Dictionary<HandJoints, DataRow>
{
    public DataJoints()
    {
        Add(HandJoints.a, new DataRow());
        Add(HandJoints.b, new DataRow());
        Add(HandJoints.c, new DataRow());
        Add(HandJoints.surface, new DataRow());
    }
}