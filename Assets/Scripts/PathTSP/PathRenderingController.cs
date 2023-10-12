using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathRenderingController : MonoBehaviour
{
    public PathInterpolator PathInterpolator;

    public float MinSmoothingSeparation = 0.007f;

    private List<Vector3> Points = new List<Vector3>();

    float time1;
    float time2;

    private void StartTSP(List<Vector3> contacts)
    {
        MathT.SmoothPoints(contacts, MinSmoothingSeparation);

        Points = new List<Vector3>(contacts);
        Points.Add(Points[0]);

        time1 = Time.realtimeSinceStartup;

        float distance = MathT.TwoOpt(ref Points);

        time2 = Time.realtimeSinceStartup;
        //Debug.Log(time2 - time1);

        PathInterpolator.InterpolatePath(Points, distance);
    }

    public void SetPoints(List<Vector3> contactPoints)
    {
        if (contactPoints.Count == 0)
        {
            Points.Clear();
            PathInterpolator.InterpolatePath(new List<Vector3>(), 0f);
            return;
        }

        StartTSP(contactPoints);
    }

    private List<Vector3> FromContactPointsToVector3(ContactPoint[] contactPoints)
    {
        List<Vector3> contacts = new List<Vector3>();

        foreach (ContactPoint c in contactPoints)
        {
            contacts.Add(c.point);
        }

        return contacts;
    }
}
