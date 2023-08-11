using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PopulateMesh))]
public class PopulateMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Spheres"))
        {
            PopulateMesh populateMesh = (PopulateMesh)target;
            populateMesh.Generate();
        }

        if (GUILayout.Button("Destroy Colliders"))
        {
            PopulateMesh populateMesh = (PopulateMesh)target;
            populateMesh.DestroyAll();
        }

        if (GUILayout.Button("Bound Spheres"))
        {
            PopulateMesh populateMesh = (PopulateMesh)target;
            populateMesh.BoundSpheres();
        }
    }
}