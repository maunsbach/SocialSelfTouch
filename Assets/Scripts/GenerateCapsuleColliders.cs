using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class GenerateCapsuleColliders : MonoBehaviour
{
    private Transform _childTransform;
    private CapsuleCollider _collider;
    public float Radius = 0.008f;


    /*private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(transform.name + " X " + other.name);

        Debug.DrawRay(other.transform.position, other.transform.up, Color.white);
    }*/

    public void Generate()
    {
        GetObjects();

        Vector3 center = _childTransform.localPosition * 0.5f;
        _collider.center = center;
        _collider.height = Mathf.Abs(2f * center.x);
        _collider.radius = Radius;
        Debug.Log(center);
    }

    private void GetObjects()
    {
        _collider = gameObject.GetComponent<CapsuleCollider>();
        _collider.direction = 0;
        _childTransform = transform.GetChild(0);
    }
}
