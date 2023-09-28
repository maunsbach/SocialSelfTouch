using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollisionDetectorChild : MonoBehaviour
{
    private CollisionDetector _collisionDetector;

    private void Awake()
    {
        Physics.reuseCollisionCallbacks = true;
        GameObject PathBrain = GameObject.Find("PathBrain");
        _collisionDetector = PathBrain.GetComponent<CollisionDetector>();

    }

    void OnTriggerEnter(Collider other)
    {
        int otherID = other.gameObject.GetInstanceID();
        SphereID sphereID = other.gameObject.GetComponent<SphereID>();
        _collisionDetector.AddContactPoint(otherID, other.transform.position, sphereID);

        other.gameObject.GetComponent<DebugSphere>().ShowSphere();
    }

    void OnTriggerExit(Collider other)
    {
        int otherID = other.gameObject.GetInstanceID();
        _collisionDetector.RemoveContactPoint(otherID);

        other.gameObject.GetComponent<DebugSphere>().DisableSphere();
    }

}
