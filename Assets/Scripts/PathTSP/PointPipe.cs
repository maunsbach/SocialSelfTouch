using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointPipe : MonoBehaviour
{
    private PathRenderingController _pathRenderingController;
    public int PointCount;

    public float _samplingRate;
    private float t;

    private List<Vector3> _contactPoints = new List<Vector3>();

    public Transform LeapOrigin;

    private void Awake()
    {
        _samplingRate = SamplingRate.Value;
        _pathRenderingController = gameObject.GetComponent<PathRenderingController>();
    }

    void FixedUpdate()
    {
        t += Time.deltaTime;

        if (t > _samplingRate)
        {
            PointCount = _contactPoints.Count();
            SendContactPoints();
            t -= _samplingRate;
            RemoveContactPoints();
        }
    }

    private void SendContactPoints()
    {
        //_pathRenderingController.SetPoints(new List<Vector3>(_contactPoints));
    }

    public void AddContactPoint(Vector3 point)
    {
        _contactPoints.Add(LeapOrigin.InverseTransformPoint(point));
    }

    public void RemoveContactPoints()
    {
        _contactPoints.Clear();


    }

}
