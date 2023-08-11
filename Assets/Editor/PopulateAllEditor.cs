using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PopulateAll))]
public class PopulateAllEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Colliders"))
        {
            PopulateAll populateAll = (PopulateAll)target;
            populateAll.Generate();
        }

        if (GUILayout.Button("Destroy Colliders"))
        {
            PopulateAll populateAll = (PopulateAll)target;
            populateAll.DestroyAll();
        }
    }
}