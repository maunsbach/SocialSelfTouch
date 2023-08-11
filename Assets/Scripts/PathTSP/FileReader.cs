using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileReader
{
    public static List<Vector3> ReadFile(string path)
    {
        StreamReader reader = new StreamReader(path);

        List<string> lines = new List<string>();

        while (!reader.EndOfStream)
        {
            lines.Add(reader.ReadLine());
        }

        int numberOfLines = lines.Count;

        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < numberOfLines; i++)
        {
            points.Add(StringToVector3(lines[i]));
        }

        return points;
    }

    // https://answers.unity.com/questions/1134997/string-to-vector3.html
    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
}
