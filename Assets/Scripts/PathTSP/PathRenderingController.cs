using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathRenderingController : MonoBehaviour
{
    public PathSensation PathSensation;

    public Transform ServiceProvider;
    public bool IsSender;

    public float MinSmoothingSeparation = 0.007f;

    public float MaxPathLength = 0.2f;
    public float InterpolationSeparation = 0.000175f;

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

    private List<Vector3> InterpolatedPath = new List<Vector3>();

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

        InterpolatedPath.Clear();
        InterpolatedPath = MathT.InterpolatePaths(Points, InterpolationSeparation);

        //Debug.Log(InterpolatedPath.Count);
        /*for (int i = 0; i < InterpolatedPath.Count; i++)
        {
            Debug.DrawRay(InterpolatedPath[i], Vector3.up, Color.white);
        }*/

        //Debug.Log(InterpolatedPath.Count);

        PathSensation.SetPath(InterpolatedPath);
    }

    float time1;
    float time2;

    public void SetPoints(List<Vector3> contactPoints)
    {
        if (contactPoints.Count == 0)
        {
            Paths.Clear();
            InterpolatedPath.Clear();
            Points.Clear();
            PathSensation.SetPath(new List<Vector3>());
            return;
        }

        //time1 = Time.realtimeSinceStartup;
        //List<Vector3> contacts = FromContactPointsToVector3(contactPoints);

        TransformContacts(contactPoints);

        StartTSP(contactPoints);

        time2 = Time.realtimeSinceStartup;

        //Debug.Log(contacts.Count + ", " + (time2 - time1));

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

    private void TwoOptRecursive(List<Vector3> path)
    {
        float distance = MathT.TwoOpt(ref path);

        //if (distance < MaxPathLength || path.Count <= 3)
        //{
            //path.Add(path[0]);
            Paths.Add(path);
        /*}
        
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

        }*/
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
