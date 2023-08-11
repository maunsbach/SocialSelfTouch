using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PopulateCollider))]
public class PopulateColliderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Colliders"))
        {
            PopulateCollider populateCollider = (PopulateCollider)target;
            populateCollider.Generate();
        }

        if (GUILayout.Button("Destroy Colliders"))
        {
            PopulateCollider populateCollider = (PopulateCollider)target;
            populateCollider.DestroyAll();
        }
    }
}