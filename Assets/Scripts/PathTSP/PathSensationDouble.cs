using UnityEngine;
using System;
using Ultraleap.Haptics;
using SVector3 = System.Numerics.Vector3;
using System.Collections.Generic;

public class PathSensationDouble : MonoBehaviour
{
    public bool AverageAlignmentOn;
    public Vector3 AverageAlignment = new Vector3(1.1786f, 1.0461f, -0.0612f);
    public Vector3 CenterPoint = new Vector3(0f, 0f, 0.2f);

    public float Intensity = 1f;

    private bool _newPathAvailable;
    private List<Vector3> _incomingPath;
    private List<Vector3> _path = new List<Vector3>();

    private int _pathIncrement;
    private int _pointIncrement;

    private StreamingEmitter _emitter;

    void Start()
    {
        AverageAlignment -= CenterPoint;

        using Library lib = new Library();
        lib.Connect();
        //using IDevice deviceSender = lib.FindDevice("USX:000008DB");

        //Debug.Log(deviceSender.CheckConnection().ToString());

        //using IDevice deviceReceiver = lib.FindDevice("USX:00000705");
        //Debug.Log(deviceReceiver.CheckConnection().ToString());


        /*
        _emitter = new StreamingEmitter(lib);
        _emitter.Devices.Add(device);

        _emitter.SetControlPointCount(1, AdjustRate.None);
        _emitter.EmissionCallback = Callback;

        _emitter.Start();
        Debug.Log("Start");*/

    }

    float x;
    float y;
    float z;

    // This callback is called every time the device is ready to accept new control point information
    private void Callback(StreamingEmitter emitter, StreamingEmitter.Interval interval, DateTimeOffset submissionDeadline)
    {
        if (_newPathAvailable == true)
        {
            //Debug.Log("Getting New Path");
            _path = new List<Vector3>(_incomingPath);
            _pointIncrement = 0;
            _newPathAvailable = false;
        }


        if (_path.Count < 1)
        {
            //Debug.Log("Empty path");
            return;
        }

        try
        {
            foreach (var sample in interval)
            {

                if (AverageAlignmentOn)
                {
                    x = _path[_pointIncrement].x - AverageAlignment.x;
                    y = _path[_pointIncrement].y - AverageAlignment.y;
                    z = _path[_pointIncrement].z - AverageAlignment.z;
                }
                else
                {
                    x = _path[_pointIncrement].x;
                    y = _path[_pointIncrement].y;
                    z = _path[_pointIncrement].z;
                }

                var p = new SVector3(x, y, z);

                //Debug.Log("Post: " + x + ", " + y + ", " + z);

                sample.Points[0].Position = p;
                sample.Points[0].Intensity = Intensity;

                _pointIncrement++;
                if (_pointIncrement == _path.Count)
                {
                    _pointIncrement = 0;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

    }

    internal void SetEmptyPath()
    {
        Intensity = 0f;
    }

    // Ensure the emitter is stopped when disabled
    void OnDisable()
    {
        if (_emitter != null)
        {
            _emitter.Stop();
        }
    }

    // Ensure the emitter is immediately disposed when destroyed
    void OnDestroy()
    {
        if (_emitter != null)
        {
            _emitter.Stop();
        }

    }

    public void SetPath(List<Vector3> path)
    {
        Intensity = 1f;
        _newPathAvailable = true;
        _incomingPath = path;

    }
}