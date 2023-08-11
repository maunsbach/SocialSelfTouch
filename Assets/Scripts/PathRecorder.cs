using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PathRecorder : MonoBehaviour
{
    private List<string> _positionData;
    private string _header = "Timestamp,HandLimb,Joint,Row,Column";
    private string _separator = ",";

    void Awake()
    {
        _positionData = new List<string>();
    }
    public void SetPoints(SphereID[] sphereID, int sampleTime)
    {
        //string time = Time.realtimeSinceStartup.ToString();
        string time = sampleTime.ToString();
        string data;

        for (int i = 0; i < sphereID.Length; i++)
        {
            data = time + _separator;
            data += sphereID[i].HandLimb + _separator;
            data += sphereID[i].HandJoint + _separator;
            data += sphereID[i].Row + _separator;
            data += sphereID[i].Column + _separator;
            _positionData.Add(data);
        }
    }

    void OnDestroy()
    {
        //SaveData();    
    }

    private void SaveData()
    {
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/" + "PathData" + System.DateTime.Now.ToString("yyyMMdd-HHmmss") + ".csv");
        Debug.Log(Application.persistentDataPath);

        writer.WriteLine(_header);

        foreach(string s in _positionData)
        {
            writer.WriteLine(s);
        }

        writer.Close();
        Debug.Log("Data written");

    }
}
