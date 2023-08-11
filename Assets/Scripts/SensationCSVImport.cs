using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SensationCSVImport : MonoBehaviour
{
    public TextAsset CSVFile;

    public DataSphereID[] sphereIDs;

    void Awake()
    {
        ReadCSV();   
    }

    private void ReadCSV()
    {
        string text = CSVFile.text;

        string[] lines = text.Split("\n");
        Debug.Log(lines.Length);

        sphereIDs = new DataSphereID[lines.Length - 2];

        int startTime = int.Parse(lines[1].Split(",")[0]);

        string[] data = new string[5];
        DataSphereID id;

        for (int i = 1; i < lines.Length-1; i++)
        {
            data = lines[i].Split(",");

            id = new DataSphereID();
            id.TimeStamp = int.Parse(data[0]) - startTime;
            id.HandLimb = (HandLimbs)System.Enum.Parse(typeof(HandLimbs), data[1]);
            id.HandJoint = (HandJoints)System.Enum.Parse(typeof(HandJoints), data[2]);
            id.Row = int.Parse(data[3]);
            id.Column = int.Parse(data[4]);

            sphereIDs[i - 1] = id;
        }
    }
}
