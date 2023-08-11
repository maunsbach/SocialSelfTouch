using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    private Collision _collision;

    private Dictionary<int, Vector3> _contactPoints = new Dictionary< int, Vector3>();

    private float _samplingRate;
    private float t;

    private int _thisID;

    private void Awake()
    {
        _thisID = gameObject.GetInstanceID();
        _samplingRate = SamplingRate.Value;
    }

    void FixedUpdate()
    {
        t += Time.deltaTime;

        if (t > _samplingRate)
        {
            DebugContactPoints();
            t -= _samplingRate;
        }
    }

    private int _firstID;
    private int _secondID;
    private float _tempTime;

    void DebugContactPoints()
    {
        Debug.Log("COUNT: " + _contactPoints.Count);
        for (int i = 0; i < _contactPoints.Count; i++)
        {
            Debug.Log(_contactPoints.ElementAt(i).Value);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        int otherID = other.gameObject.GetInstanceID();
        
        if (_contactPoints.ContainsKey(otherID))
        {
            Debug.Log("Already contains new collision");
        }
        else
        {
            _contactPoints.Add(otherID, other.gameObject.transform.position);
        }
    }

    void OnTriggerExit(Collider other)
    {
        int otherID = other.gameObject.GetInstanceID();

        if (_contactPoints.ContainsKey(otherID))
        {
            _contactPoints.Remove(otherID);
        }
        else
        {
            Debug.Log("Already contains new collision");
        }
    }
}
