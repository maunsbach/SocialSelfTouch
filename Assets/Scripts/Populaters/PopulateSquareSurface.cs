using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateSquareSurface : MonoBehaviour
{
    public GameObject _prefab;
    private float radius;
    public float SpheresPerCM = 2f;
    public HandLimbs HandLimb;
    public HandJoints Joint;

    private Vector3 _size;

    void Begin()
    {
        radius = _prefab.transform.localScale.x / SpheresPerCM;
        Vector3 center = transform.position;
        _size = transform.lossyScale;
        Vector3 startPosition = center - 0.5f * _size.x * transform.right - 0.5f * _size.z * transform.forward;

        InstantiateSpheres(startPosition);
    }

    void InstantiateSpheres(Vector3 startPosition)
    {
        int sphereWidth = (int)Mathf.Ceil(_size.x * 100f * SpheresPerCM);
        int sphereDepth = (int)Mathf.Ceil(_size.z * 100f * SpheresPerCM);

        for (int i = 0; i < sphereWidth; i++)
        {
            for (int j = 0; j < sphereDepth; j++)
            {
                GameObject sphere = Instantiate(_prefab);
                Vector3 position = startPosition+ i * radius * transform.right + j * radius * transform.forward;
                sphere.transform.position = position;
                sphere.transform.parent = transform;
                sphere.transform.localScale /= SpheresPerCM;
                sphere.transform.name = HandLimb.ToString() + "_" + Joint.ToString() + "_c" + j + "_r" + i;
                sphere.GetComponent<SphereID>().SetValues(HandLimb, Joint, j, i);

            }
        }
    }

    private void ClearSpheres()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }

    public void Generate()
    {
        Begin();

    }

    public void DestroyAll()
    {
        ClearSpheres();
    }
}
