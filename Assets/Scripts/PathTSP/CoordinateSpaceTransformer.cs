using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordinateSpaceTransformer : MonoBehaviour
{
    public PathSensation PathSensation;
    public Transform ServiceProvider;
    public bool IsSender;
    private Vector3 UltraLeapAlignment = new Vector3(0f, 0.1210f, 0f);

    public void TransformPath(List<Vector3> path)
    {
        //Debug.Log(path.Count);
        TransformContacts(path);

        PathSensation.SetPath(path);
    }


    private void TransformContacts(List<Vector3> contacts)
    {
        for (int i = 0; i < contacts.Count; i++)
        {
            OffsetPosition(contacts, i);
            TransformPosition(contacts, i);
        }
    }

    private void OffsetPosition(List<Vector3> contacts, int i)
    {
        contacts[i] -= ServiceProvider.position;
    }

    private void TransformPosition(List<Vector3> contacts, int i)
    {
        if (IsSender)
        {
            contacts[i] = new Vector3(contacts[i].x, -(-contacts[i].z - UltraLeapAlignment.y), contacts[i].y);
        }
        else
        {
            contacts[i] = new Vector3(contacts[i].x, -contacts[i].z + UltraLeapAlignment.y, -contacts[i].y);
        }
    }
}
