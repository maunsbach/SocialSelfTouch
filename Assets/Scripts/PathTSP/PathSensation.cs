using UnityEngine;
using System;
using Ultraleap.Haptics;
using SVector3 = System.Numerics.Vector3;
using System.Collections.Generic;

public class PathSensation : MonoBehaviour
{
    public string UHDeviceID = "USX:00000705";
    public bool DebugOn;

    public float Intensity = 1f;

    private bool _newPathAvailable;
    private List<Vector3> _incomingPath;
    private List<Vector3> _path = new List<Vector3>();

    private int _pathIncrement;
    private int _pointIncrement;

    private StreamingEmitter _emitter;
    private Ultraleap.Haptics.Transform _transform;

    void Start()
    {
        using Library lib = new Library();
        lib.Connect();
        // Receiver: "USX:00000705"
        // Sender: "USX:000008DB"
        using IDevice device = lib.FindDevice(UHDeviceID);
        _emitter = new StreamingEmitter(lib);
        _emitter.Devices.Add(device);
        Debug.Log(device.Identifier);

        _transform = device.GetKitTransform();

        _emitter.SetControlPointCount(1, AdjustRate.None);
        _emitter.EmissionCallback = Callback;

        _emitter.Start();
        Debug.Log("Start");

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
            var p = new SVector3(x, y, z);
            foreach (var sample in interval)
            {
                x = _path[_pointIncrement].x;
                y = _path[_pointIncrement].y;
                z = _path[_pointIncrement].z;


                sample.Points[0].Position = p;
                sample.Points[0].Intensity = Intensity;
                //sample.Points[0].Intensity = 0.5f * Mathf.Sin(_pointIncrement*0.05f) + 0.5f;

                _pointIncrement++;
                if (_pointIncrement == _path.Count)
                {
                    _pointIncrement = 0;
                }
            }
            if (DebugOn)
            {
                Debug.Log("Post: " + p.X + ", " + p.Y + ", " + p.Z);
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

    void OnDisable()
    {
        if (_emitter != null)
        {
            _emitter.Stop();
        }
    }

    void OnDestroy()
    {
        if (_emitter != null)
        {
            _emitter.Stop();
        }

    }

    public void SetPath(List<Vector3> path)
    {
        //Debug.Log("Setting path. Count: " + path.Count);
        Intensity = 1f;
        _newPathAvailable = true;
        _incomingPath = path;

    }
}