using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(YMeshRenderer))]
public class YMeshRendererEditor : Editor
{
    public override void OnInspectorGUI()
    {
        YMeshRenderer yMeshRenderer = (YMeshRenderer)target;

        SerializedProperty meshToCreateField = serializedObject.FindProperty("meshToCreate");

        EditorGUILayout.PropertyField(meshToCreateField, new GUIContent("Mesh To Create"));

        if (GUILayout.Button("Create Mesh"))
        {
            yMeshRenderer.CreateMesh();
        }


        serializedObject.ApplyModifiedProperties();
    }
}