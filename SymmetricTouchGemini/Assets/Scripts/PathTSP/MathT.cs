using System;
using System.Collections.Generic;
using UnityEngine;

public static class MathT
{
    public static void SmoothPoints(List<Vector3> points, float minDistance)
    {
        int i = 0;
        while (i < points.Count)
        {
            int j = i + 1;
            while (j < points.Count)
            {
                if (Vector3.Distance(points[i], points[j]) < minDistance)
                {
                    points.RemoveAt(j);
                }
                else
                {
                    j++;
                }
            }
            i++;
        }
    }

    public static float TwoOpt(ref List<Vector3> points)
    {
        float bestDistance = CalculateTotalDistance(points);
        //Debug.Log(bestDistance);
        float newDistance;
        int n = points.Count - 1;
        List<Vector3> newRoute;

        bool foundImprovement = true;

        while (foundImprovement == true) {
            foundImprovement = false;
            for (int i = 0; i < (n - 1); i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    newRoute = TwoOptSwap(points, i, j);
                    newDistance = CalculateTotalDistance(newRoute);
                    if (newRoute.Count != points.Count)
                    {
                        Debug.LogWarning("Routes don't have the same distance");
                    }
                    if (newDistance < bestDistance)
                    {
                        foundImprovement = true;
                        //Debug.Log("Swapped");
                        points = newRoute;
                        bestDistance = newDistance;
                    }
                }
            }
        }

        //Debug.Log(bestDistance);

        return bestDistance;
    }

    
    public static List<Vector3> InterpolatePaths(List<List<Vector3>> paths, float separation)
    {
        List<Vector3> interpolatedPaths = new List<Vector3>();

        Vector3 start;
        Vector3 target;
        Vector3 normal;
        Vector3 interVector;
        float edgeDistance;
        float edgeDistanceMult;
        float separationMult = 1f / separation;

        for (int i = 0; i < paths.Count; i++)
        {
            for (int j = 0; j < (paths[i].Count - 1); j++)
            {
                start = paths[i][j];
                target = paths[i][j + 1];
                edgeDistance = Vector3.Distance(start, target);
                int edgeSeparations = Mathf.RoundToInt(edgeDistance * separationMult);
                normal = target - start;

                edgeDistanceMult = 1f / edgeDistance;
                for (int k = 0; k < edgeSeparations; k++)
                {
                    interVector = start + ((separation * k) * edgeDistanceMult) * normal;
                    interpolatedPaths.Add(interVector);
                }
            }
        }

        return interpolatedPaths;
    }

    public static List<Vector3> InterpolatePaths(List<Vector3> paths, float separation)
    {
        List<Vector3> interpolatedPaths = new List<Vector3>();

        Vector3 start;
        Vector3 target;
        Vector3 normal;
        Vector3 interVector;
        float edgeDistance;
        float edgeDistanceMult;
        float separationMult = 1f / separation;

        for (int i = 0; i < (paths.Count - 1); i++)
        {
            start = paths[i];
            target = paths[i + 1];
            edgeDistance = Vector3.Distance(start, target);
            int edgeSeparations = Mathf.RoundToInt(edgeDistance * separationMult);
            normal = target - start;

            edgeDistanceMult = 1f / edgeDistance;
            for (int k = 0; k < edgeSeparations; k++)
            {
                interVector = start + ((separation * k) * edgeDistanceMult) * normal;
                interpolatedPaths.Add(interVector);
            }
        }

        return interpolatedPaths;
    }

    private static float CalculateTotalDistance(List<Vector3> points)
    {
        float distance = 0;
        
        for (int i = 0; i < points.Count-1; i++)
        {
            distance = distance + Vector3.Distance(points[i], points[i + 1]);
        }
        return distance;
    }

    private static List<Vector3> TwoOptSwap(List<Vector3> points, int v1, int v2)
    {
        List<Vector3> newPoints = new List<Vector3>();

        for (int i = 0; i <= v1; i++)
        {
            newPoints.Add(points[i]);
        }

        for (int i = v2; i >= (v1+1); i--)
        {
            newPoints.Add(points[i]);
        }

        for (int i = (v2 + 1); i < points.Count; i++)
        {
            //Debug.Log(newPoints.Count);
            newPoints.Add(points[i]);

        }


        return newPoints;
    }
}
