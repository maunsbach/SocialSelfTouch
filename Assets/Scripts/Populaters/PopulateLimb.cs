using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateLimb : MonoBehaviour
{
    public GameObject _prefab;
    private float _prefabRadius;

    // Start is called before the first frame update
    void Start()
    {
        float distance = Vector3.Distance(transform.position, transform.parent.position);
        Vector3 heading = transform.parent.position - transform.position;
        float magnitude = heading.magnitude;
        Vector3 direction = heading / magnitude;

        _prefabRadius = _prefab.transform.localScale.x * 0.5f;

        InstantiateSpheres(distance, direction);
    }

    void InstantiateSpheres(float distance, Vector3 direction)
    {
        int sphereCount = (int)Mathf.Ceil(distance * 100f);
        //int radiusCount = 4;

        float depthStart = _prefabRadius;
        float distanceSeparation = distance / sphereCount;
        Vector3 directionIncrement = distanceSeparation * direction;

        for (int i = 0; i < sphereCount; i++)
        {
            GameObject sphere = Instantiate(_prefab);
            sphere.transform.position = transform.position + directionIncrement * i;
            sphere.transform.parent = transform;
        }

        //InstantiateLine(sphereCount, depthStart, distanceSeparation,ctor3.left);
        //InstantiateLine(heightCount, heightStart, heightSeparation, 0.5f * radius * Vector3.right);
        //InstantiateLine(heightCount, heightStart, heightSeparation, 0.5f * radius * Vector3.up);
        //InstantiateLine(heightCount, heightStart, heightSeparation, 0.5f * radius * Vector3.down);
    }

    private void InstantiateLine(int heightCount, float heightStart, float heightSeparation, Vector3 radiusPos)
    {
        for (int i = 0; i < heightCount; i++)
        {
            GameObject sphere = Instantiate(_prefab, transform);
            sphere.transform.localPosition = new Vector3(0f, 0f, heightStart + (heightSeparation * i)) + radiusPos;
        }
    }
}
