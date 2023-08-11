using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateCapsule : MonoBehaviour
{
    public GameObject _prefab;
    private float _prefabRadius;

    public bool DeleteCapsuleCollider = true;

    // Start is called before the first frame update
    void Awake()
    {
        float radius = gameObject.GetComponent<CapsuleCollider>().radius;
        float _height = gameObject.GetComponent<CapsuleCollider>().height;
        _prefabRadius = _prefab.transform.localScale.x * 0.5f;

        InstantiateSpheres(radius, _height);

        if (DeleteCapsuleCollider)
        {
            Destroy(gameObject.GetComponent<CapsuleCollider>());
        }
    }

    void InstantiateSpheres(float radius, float height)
    {
        int heightCount = (int)Mathf.Ceil(height * 100f);
        //int radiusCount = 4;

        float heightStart = 0f - 0.5f * height + _prefabRadius;
        float heightSeparation = height / heightCount;

        InstantiateLine(heightCount, heightStart, heightSeparation, 0.5f * radius * Vector3.left);
        InstantiateLine(heightCount, heightStart, heightSeparation, 0.5f * radius * Vector3.right);
        InstantiateLine(heightCount, heightStart, heightSeparation, 0.5f * radius * Vector3.up);
        InstantiateLine(heightCount, heightStart, heightSeparation, 0.5f * radius * Vector3.down);
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
