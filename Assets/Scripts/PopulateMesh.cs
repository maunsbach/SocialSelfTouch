using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateMesh : MonoBehaviour
{
    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Vector3 _center;
    private Vector3 _extent;

    public GameObject _prefab;
    private float radius;
    public float SpheresPerCM = 2f;
    private void Begin()
    {
        _skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        _center = _skinnedMeshRenderer.bounds.center;
        _extent = _skinnedMeshRenderer.bounds.extents;

        radius = _prefab.transform.localScale.x / SpheresPerCM;

        Vector3 startPosition = _center - _extent.x * transform.right - _extent.y * transform.forward - _extent.z * transform.up;

        //GameObject sphere = Instantiate(_prefab);
        //sphere.transform.position = startPosition;
        //sphere.transform.parent = transform;

        InstantiateSpheres(startPosition);
    }

    void InstantiateSpheres(Vector3 startPosition)
    {
        int sphereWidth = (int)Mathf.Ceil(_extent.x * 2f * 100f * SpheresPerCM);
        int sphereHeight = (int)Mathf.Ceil(_extent.z * 2f * 100f * SpheresPerCM);
        int sphereDepth = (int)Mathf.Ceil(_extent.y * 2f * 100f * SpheresPerCM);

        Debug.Log(sphereWidth);
        Debug.Log(sphereHeight);
        for (int i = 0; i < sphereDepth; i++)
        {
            for (int j = 0; j < sphereWidth; j++)
            {
                for (int k = 0; k < sphereHeight; k++)
                {
                    GameObject sphere = Instantiate(_prefab);
                    Vector3 position = startPosition + i * radius * transform.forward + j * radius * transform.right + k * radius * transform.up;
                    sphere.transform.position = position;
                    sphere.transform.parent = transform;
                    sphere.transform.localScale /= SpheresPerCM;
                }
            }
        }

        Debug.Log("Spheres: " + transform.childCount);
    }

    void OnCollisionEnter(Collision other)
    {
        other.transform.parent = transform.parent.GetChild(0);
        //Debug.Log(other.name);
      //Destroy(other.gameObject);  
    }

    public void Generate()
    {
        Begin();
    }

    public void BoundSpheres()
    {
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        int i = 0;

        while (i < transform.childCount)
        {
            Vector3 position = transform.GetChild(i).position;

            Vector3 closestPoint = meshCollider.ClosestPoint(position);

            if (Vector3.Distance(closestPoint, position) > 0.001f)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
            else
            {
                i++;
            }
        }
    }

    public void DestroyAll()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
