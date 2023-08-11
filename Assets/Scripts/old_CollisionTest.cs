using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class old_CollisionTest : MonoBehaviour
{
    private Collision _collision;

    private Dictionary<int, Dictionary<int, ContactPoint[]>> _contactPoints = new Dictionary<int, Dictionary<int, ContactPoint[]>>();

    public float TimeBetweenSendingContacts = 0.1f;
    private float t;

    private int _thisID;

    private void Awake()
    {
        TimeBetweenSendingContacts = SamplingRate.Value;
        _thisID = gameObject.GetInstanceID();
    }

    void FixedUpdate()
    {
        t += Time.deltaTime;

        if (t > TimeBetweenSendingContacts)
        {
            DebugContactPoints();
            t -= TimeBetweenSendingContacts;
        }
    }

    private int _firstID;
    private int _secondID;
    private float _tempTime;

    void DebugContactPoints()
    {
        _tempTime = Time.realtimeSinceStartup;
        for (int i = 0; i < _contactPoints.Count; i++)
        {
            _firstID = _contactPoints.ElementAt(i).Key;
            for (int j = 0; j < _contactPoints.ElementAt(i).Value.Count; j++)
            {
                _secondID = _contactPoints.ElementAt(i).Value.ElementAt(j).Key;

                foreach(ContactPoint cp in _contactPoints.ElementAt(i).Value.ElementAt(j).Value)
                {
                    Debug.Log(_tempTime + " || " + _firstID + ", " + _secondID + ": " + cp.point.ToString("F4"));

                }
            }
        }
    }

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "OwnHand")
        {
            return;
        }
        int otherID = collision.gameObject.GetInstanceID();

        if (_contactPoints.ContainsKey(_thisID))
        {
            if (_contactPoints[_thisID].ContainsKey(otherID))
            {
                _contactPoints[_thisID].Remove(otherID);
            }
            _contactPoints[_thisID].Add(otherID, collision.contacts);
        }
        else
        {
            _contactPoints.Add(_thisID, New2Dictionary(otherID, collision.contacts));
        }

        //Debug.Log(_thisID + ", " + otherID, collision.gameObject);
    }

    private Dictionary<int, ContactPoint[]> New2Dictionary(int key, ContactPoint[] contactPoints)
    {
        Dictionary<int, ContactPoint[]> contactInsert = new Dictionary<int, ContactPoint[]>(1);
        contactInsert.Add(key, contactPoints);

        return contactInsert;
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "OwnHand")
        {
            return;
        }

        int otherID = collision.gameObject.GetInstanceID();

        if (_contactPoints.ContainsKey(_thisID))
        {
            if (_contactPoints[_thisID].ContainsKey(otherID))
            {
                _contactPoints[_thisID].Remove(otherID);
            }

            if (_contactPoints[_thisID].Count == 0)
            {
                _contactPoints.Remove(_thisID);
            }
        }
    }*/
}
