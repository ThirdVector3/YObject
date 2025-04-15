using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(YMeshRenderer))]
public class YMeshRendererEditor : Editor
{
    public override void OnInspectorGUI()
    {
        YMeshRenderer yMeshRenderer = (YMeshRenderer)target;

        SerializedProperty meshToCreateField = serializedObject.FindProperty("meshToCreate");
        SerializedProperty LODsField = serializedObject.FindProperty("LODs");
        SerializedProperty LODSystemEnabled = serializedObject.FindProperty("LODSystemEnabled");
        SerializedProperty cullDistance = serializedObject.FindProperty("cullDistance");

        EditorGUILayout.PropertyField(meshToCreateField, new GUIContent("Mesh To Create"));

        if (GUILayout.Button("Create Mesh"))
        {
            yMeshRenderer.CreateMesh();
        }
        EditorGUILayout.PropertyField(LODSystemEnabled, new GUIContent("LODSystemEnabled"));
        if (LODSystemEnabled.boolValue)
            EditorGUILayout.PropertyField(LODsField, new GUIContent("LODs"));
        else
            EditorGUILayout.PropertyField(cullDistance, new GUIContent("cullDistance"));

        serializedObject.ApplyModifiedProperties();
    }
}