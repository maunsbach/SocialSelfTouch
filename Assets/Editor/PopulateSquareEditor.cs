using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PopulateSquareSurface))]
public class PopulateSquareEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Colliders"))
        {
            PopulateSquareSurface populateSquareSurface = (PopulateSquareSurface)target;
            populateSquareSurface.Generate();
        }

        if (GUILayout.Button("Destroy Colliders"))
        {
            PopulateSquareSurface populateCollider = (PopulateSquareSurface)target;
            populateCollider.DestroyAll();
        }
    }
}