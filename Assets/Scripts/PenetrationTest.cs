using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PenetrationTest : MonoBehaviour
{
    public Collider colliderA;
    public Collider colliderB;

    public float Distance;
    public Vector3 Direction;
    public bool IsPenetrating;

    private void Update()
    {
        IsPenetrating = Physics.ComputePenetration(colliderA, colliderA.transform.position, colliderA.transform.rotation, colliderB, colliderB.transform.position, colliderB.transform.rotation, out Direction, out Distance);
       
    }

    private void OnDrawGizmos()
    {
        if (IsPenetrating)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(colliderB.transform.position, Direction * Distance);
        }

        transform.position = colliderB.transform.position + (0.5f*colliderB.transform.lossyScale.x-Distance) * Direction;
    }

}
