using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(GenerateCapsuleColliders))]
public class CapsuleGeneratorHelper : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Capsule"))
        {
            GenerateCapsuleColliders generateCapsuleColliders = (GenerateCapsuleColliders)target;
            generateCapsuleColliders.Generate();
        }
    }
}