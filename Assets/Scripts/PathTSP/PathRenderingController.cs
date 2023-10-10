using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathRenderingController : MonoBehaviour
{
    public PathInterpolator PathInterpolator;
    public bool IsSender;

    public float MinSmoothingSeparation = 0.007f;

    private Vector3 UltraLeapAlignment = new Vector3(0f, 0.1210f, 0f);

    private Vector3 _transformPosition;
    public Transform PointOffset;

    private List<Vector3> Points = new List<Vector3>();
    //public List<Vector3> PointsPreTSP;
    public List<Vector3> DebugPointsTSP;

    //public List<Vector3> Cluster1Points = new List<Vector3>();
    //public List<Vector3> Cluster2Points = new List<Vector3>();

    public List<List<Vector3>> Paths = new List<List<Vector3>>();
    public List<float> PathLengths = new List<float>();

    //private List<Vector3> InterpolatedPath = new List<Vector3>();


    private void StartTSP(List<Vector3> contacts)
    {
        //Points = contacts;
        Paths.Clear();

        MathT.SmoothPoints(contacts, MinSmoothingSeparation);

        Points = new List<Vector3>(contacts);

        Points.Add(Points[0]);
        float distance = MathT.TwoOpt(ref Points);
        //TwoOptRecursive(Points);

        //DebugPointsTSP = new List<Vector3>(Points);

        PathInterpolator.InterpolatePath(Points, distance);
    }

    public void SetPoints(List<Vector3> contactPoints)
    {
        if (contactPoints.Count == 0)
        {
            Paths.Clear();
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

    /*private void TwoOptRecursive(List<Vector3> path)
    {
        float distance = MathT.TwoOpt(ref path);
        Debug.Log(distance);

        if (distance < MaxPathLength || path.Count <= 3)
        {
            path.Add(path[0]);
            Paths.Add(path);
        }
        else 
        {
            KMeansResults result = KMeans.Cluster(path, 2, 50, 0);

            List<Vector3> cluster1 = new List<Vector3>();
            for (int i = 0; i < result.clusters[0].Length; i++)
            {
                cluster1.Add(path[result.clusters[0][i]]);
            }
            TwoOptRecursive(cluster1);

            List<Vector3> cluster2 = new List<Vector3>();
            for (int i = 0; i < result.clusters[1].Length; i++)
            {
                cluster2.Add(path[result.clusters[1][i]]);
            }
            TwoOptRecursive(cluster2);
        }
    }*/
}
