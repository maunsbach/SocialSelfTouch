using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateCollider : MonoBehaviour
{
    public GameObject _prefab;
    private float _prefabRadius;
    private Transform _child;
    public float SpheresPerCM = 2f;
    public float HeightRaise = 0.004f;
    public HandLimbs HandLimb;
    public HandJoints Joint;

    void Begin()
    {
        _child = transform.GetChild(0);

        float distance = Vector3.Distance(transform.position, _child.position);
        Vector3 heading = transform.position - _child.position;
        float magnitude = heading.magnitude;
        Vector3 direction = heading / magnitude;

        _prefabRadius = _prefab.transform.localScale.x  / SpheresPerCM;

        //Vector3 upDirection = Quaternion.Euler(0, 0, -90f) * direction;
        //upDirection = new Vector3(0f, upDirection.y, 0f);

        Vector3 startPosition = _child.position + 0.5f * _prefabRadius * direction + HeightRaise * transform.up;

        //Vector3 leftDirection = Quaternion.Euler(0, -90f, 0) * direction;
        //leftDirection = new Vector3(leftDirection.x, 0f, leftDirection.z);
        Vector3 startPositionLeft = startPosition + _prefabRadius * (-transform.forward);

        //Vector3 rightDirection = Quaternion.Euler(0, 90f, 0) * direction;
        //rightDirection = new Vector3(rightDirection.x, 0f, rightDirection.z);
        Vector3 startPositionRight = startPosition + _prefabRadius * transform.forward;

        InstantiateSpheres(startPositionLeft, distance, direction, 0);
        InstantiateSpheres(startPosition, distance, direction, 1);
        InstantiateSpheres(startPositionRight, distance, direction, 2);
    }

    void InstantiateSpheres(Vector3 startPosition, float distance, Vector3 direction, int columnId)
    {
        int sphereCount = (int)Mathf.Ceil(distance * 100f * SpheresPerCM);

        float distanceSeparation = distance / sphereCount;
        Vector3 directionIncrement = distanceSeparation * direction;

        for (int i = 0; i < sphereCount; i++)
        {
            GameObject sphere = Instantiate(_prefab);
            sphere.transform.position = startPosition + directionIncrement * i;
            sphere.transform.parent = transform;
            sphere.transform.localScale /= SpheresPerCM;
            sphere.transform.name = HandLimb.ToString() + "_" + Joint.ToString() + "_c" + columnId + "_r" + i;
            sphere.GetComponent<SphereID>().SetValues(HandLimb, Joint, columnId, i);
        }
    }

    private void InstantiateLine(int heightCount, float heightStart, float heightSeparation, Vector3 radiusPos)
    {
        for (int i = 0; i < heightCount; i++)
        {
            GameObject sphere = Instantiate(_prefab, transform);
            sphere.transform.localPosition = new Vector3(0f, 0f, heightStart + (heightSeparation * i)) + radiusPos;
        }
    }

    private void ClearSpheres()
    {
        int nonColliderCount = 0;
        int i = 0;
        while(transform.childCount > nonColliderCount)
        {
            if (transform.GetChild(i).tag == "SphereCollider")
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
            else
            {
                nonColliderCount++;
                i++;
            }
        }
    }

    public void Generate()
    {
        ClearSpheres();
        Begin();
    }

    public void DestroyAll()
    {
        ClearSpheres();
    }

}
