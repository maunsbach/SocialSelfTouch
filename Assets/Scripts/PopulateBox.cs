using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateBox : MonoBehaviour
{
    public GameObject _prefab;
    private float _prefabRadius;

    public bool DeleteBoxCollider = true;

    // Start is called before the first frame update
    void Awake()
    {
        Vector3 colliderSize = gameObject.GetComponent<BoxCollider>().size;
        Vector3 colliderCenter = gameObject.GetComponent<BoxCollider>().center;

        _prefabRadius = _prefab.transform.localScale.x * 0.5f;

        InstantiateSpheres(colliderSize, colliderCenter);

        if (DeleteBoxCollider)
        {
            Destroy(gameObject.GetComponent<CapsuleCollider>());
        }
    }

    void InstantiateSpheres(Vector3 size, Vector3 center)
    {
        int xCount = (int)Mathf.Ceil(size.x * 100f);
        int yCount = (int)Mathf.Ceil(size.y * 100f);
        int zCount = (int)Mathf.Ceil(size.z * 100f);

        Vector3 startPositions = ((-0.5f) * size) + new Vector3(_prefabRadius, _prefabRadius, _prefabRadius) + center; 

        float xSeparation = size.x / xCount;
        float ySeparation = size.y / yCount;
        float zSeparation = size.z / zCount;

        for (int ix = 0; ix < xCount; ix++)
        {
            for (int iy = 0; iy < yCount; iy++)
            {
                for (int iz = 0; iz < zCount; iz++)
                {
                    GameObject sphere = Instantiate(_prefab, transform);
                    Vector3 newPos = new Vector3(startPositions.x + xSeparation * ix, startPositions.y + ySeparation * iy, startPositions.z + zSeparation * iz);
                    sphere.transform.localPosition = newPos;
                }
            }
        }

        //InstantiateLine(xSeparation, start, heightSeparation, 0.5f * radius * Vector3.left);
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
