using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathInterpolator : MonoBehaviour
{
    public CoordinateSpaceTransformer ReceiverCoordinateSpaceTransformer;
    public CoordinateSpaceTransformer SenderCoordinateSpaceTransformer;

    public float MaxPathLength = 0.2f;
    public float DefaultInterpolationSeparation = 0.00035f;
    private float _dynamicInterpolationSeparation;

    public float MinimumFrequency = 60f;
    private float _minimumInterpolationSeparation;
    public float MaximumFrequency = 200f;
    private float _maximumInterpolationSeparation;

    private List<Vector3> InterpolatedPath = new List<Vector3>();

    public void InterpolatePath(List <Vector3> path, float distance)
    {

        InterpolatedPath.Clear();
        InterpolatedPath = MathT.InterpolatePaths(path, DefaultInterpolationSeparation);

        ReceiverCoordinateSpaceTransformer.TransformPath(new List<Vector3>(InterpolatedPath));
        SenderCoordinateSpaceTransformer.TransformPath(new List<Vector3>(InterpolatedPath));
    }
}
