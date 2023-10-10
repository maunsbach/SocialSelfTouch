using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathInterpolator : MonoBehaviour
{
    public CoordinateSpaceTransformer ReceiverCoordinateSpaceTransformer;
    public CoordinateSpaceTransformer SenderCoordinateSpaceTransformer;

    public float DefaultInterpolationSeparation = 0.00035f;
    private float _interpolationSeparation;

    public float MinimumFrequency = 60f;
    private float _maximumDistance;

    public float MaximumFrequency = 200f;
    private float _minimumDistance;

    private float _uHSampleRate = 40000;

    private List<Vector3> InterpolatedPath = new List<Vector3>();

    public bool DynamicCapping = true;

    private void Awake()
    {
        _minimumDistance = DefaultInterpolationSeparation * _uHSampleRate / MaximumFrequency;
        _maximumDistance = DefaultInterpolationSeparation * _uHSampleRate / MinimumFrequency;
    }

    public void InterpolatePath(List <Vector3> path, float distance)
    {
        if (DynamicCapping == true)
        {
            _interpolationSeparation = FetchInterpolationSeparationFromDistance(distance);
        }
        else
        {
            _interpolationSeparation = DefaultInterpolationSeparation;
        }

        InterpolatedPath.Clear();
        InterpolatedPath = MathT.InterpolatePaths(path, _interpolationSeparation);

        ReceiverCoordinateSpaceTransformer.TransformPath(new List<Vector3>(InterpolatedPath));
        SenderCoordinateSpaceTransformer.TransformPath(new List<Vector3>(InterpolatedPath));
    }

    private float FetchInterpolationSeparationFromDistance(float distance)
    {
        _minimumDistance = DefaultInterpolationSeparation * _uHSampleRate / MaximumFrequency;
        _maximumDistance = DefaultInterpolationSeparation * _uHSampleRate / MinimumFrequency;

        if (distance < _minimumDistance)
        {
            return MaximumFrequency * distance / _uHSampleRate;
        }
        else if (distance > _maximumDistance)
        {
            return MinimumFrequency * distance / _uHSampleRate;
        }
        else
        {
            return DefaultInterpolationSeparation;
        }
    }
}
