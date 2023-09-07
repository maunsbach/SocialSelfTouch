using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private PathRenderingController _pathRenderingController;
    public PathRenderingController _senderPathRenderingController;
    private PathRecorder _pathRecorder;

    public bool DrawDebug;

    public float _samplingRate;
    private int _sampleTime;
    private float t;

    private Dictionary<int, Vector3> _contactPoints = new Dictionary<int, Vector3>();
    private Dictionary<int, SphereID> _contactIDs = new Dictionary<int, SphereID>();
    private Dictionary<int, int> _contactPointCount = new Dictionary<int, int>();

    private void Awake()
    {
        _samplingRate = SamplingRate.Value;
        //Physics.reuseCollisionCallbacks = true;
        //GameObject PathBrain = GameObject.Find("PathBrain");

        _pathRenderingController = gameObject.GetComponent<PathRenderingController>();
        _pathRecorder = gameObject.GetComponent<PathRecorder>();


    }

    void FixedUpdate()
    {
        t += Time.deltaTime;

        if (t > _samplingRate)
        {
            SendContactPoints();
            t -= _samplingRate;
        }
    }

    private void SendContactPoints()
    {
        //if (_contactPoints.Values.Count > 0)
        //{
        //    Debug.Log(_contactPoints.Values.Count);
        //}
        _pathRenderingController.SetPoints(_contactPoints.Values.ToList());
        _senderPathRenderingController.SetPoints(_contactPoints.Values.ToList());
        //_pathRecorder.SetPoints(_contactIDs.Values.ToArray(), _sampleTime);
        _sampleTime++;

        Vector3[] list = _contactPoints.Values.ToArray();

        if (DrawDebug)
        {
            for (int i = 0; i<list.Length; i++)
            {
                Vector3 p = list[i];
                Debug.DrawLine(p, p + 0.1f*Vector3.up, Color.red, 0.2f);
            }
        }
    }

    public void AddContactPoint(int otherID, Vector3 point, SphereID sphereID)
    {
        if (_contactPoints.ContainsKey(otherID))
        {
            if (_contactPointCount.ContainsKey(otherID))
            {
                _contactPointCount[otherID]++;
            }
            else
            {
                _contactPointCount.Add(otherID, 1);
            }
        }
        else
        {
            _contactPoints.Add(otherID, point);
            _contactIDs.Add(otherID, sphereID);
            _contactPointCount.Add(otherID, 1);
        }
    }

    /*public void AddContactPoint(int otherID, Vector3 point)
    {
        point = point - ReceiverServiceProvider.position;
        if (_contactPoints.ContainsKey(otherID))
        {
            if (_contactPointCount.ContainsKey(otherID))
            {
                _contactPointCount[otherID]++;
            }
            else
            {
                _contactPointCount.Add(otherID, 1);
            }
        }
        else
        {
            _contactPoints.Add(otherID, point);
            _contactPointCount.Add(otherID, 1);
        }
    }*/

    public void RemoveContactPoint(int otherID)
    {
        if (_contactPoints.ContainsKey(otherID))
        {
            _contactPointCount[otherID]--;

            if (_contactPointCount[otherID] == 0)
            {
                _contactPoints.Remove(otherID);
                _contactIDs.Remove(otherID);
                _contactPointCount.Remove(otherID);
            }
        }
    }

    public void DeleteAllContactPoints()
    {
        _contactPoints.Clear();
        _contactPointCount.Clear();
    }

}
