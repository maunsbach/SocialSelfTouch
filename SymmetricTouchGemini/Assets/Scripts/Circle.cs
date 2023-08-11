using UnityEngine;
using System;
using Ultraleap.Haptics;
using SVector3 = System.Numerics.Vector3;

public class Circle : MonoBehaviour
{
    static float _radius = 0.02f; // 2cm
    static float _speed = 8.0f; // 8 metres per second

    private DateTimeOffset _startTime;
    private StreamingEmitter _emitter;

    void Start()
    {
        using Library lib = new Library();
        lib.Connect();
        using IDevice device = lib.FindDevice();
        _emitter = new StreamingEmitter(lib);
        _emitter.Devices.Add(device);
        Debug.Log(device.IsConnected.ToString());

        _emitter.SetControlPointCount(1, AdjustRate.AsRequired);
        _emitter.EmissionCallback = Callback;

        _startTime = DateTimeOffset.UtcNow;
        _emitter.Start();
        Debug.Log("Start");

    }

    // This callback is called every time the device is ready to accept new control point information
    private void Callback(StreamingEmitter emitter, StreamingEmitter.Interval interval, DateTimeOffset submissionDeadline)
    {
        //Debug.Log("Callback");
        foreach (var sample in interval)
        {
            double seconds = (sample.Time - _startTime).TotalSeconds;

            var angularVelocity = _speed /_radius;
            var phase = angularVelocity * seconds;

            float x = (float)Math.Cos(phase) * _radius;
            float y = (float)Math.Sin(phase) * _radius;
            float z = 0.2f; // 20cm above the device
            var p = new SVector3(x, y, z);

            sample.Points[0].Position = p;
            sample.Points[0].Intensity = 1.0f;
        }
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
}