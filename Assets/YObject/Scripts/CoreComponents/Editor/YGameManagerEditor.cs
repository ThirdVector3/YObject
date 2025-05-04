using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(YGameManager))]
public class YGameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        YGameManager yGameManager = (YGameManager)target;

        SerializedProperty levelSavingTypeProperty = serializedObject.FindProperty("levelSavingType");
        SerializedProperty levelNameProperty = serializedObject.FindProperty("levelName");
        SerializedProperty sampleLevelNameProperty = serializedObject.FindProperty("sampleLevelName");
        SerializedProperty updateLevelProperty = serializedObject.FindProperty("updateLevel");
        SerializedProperty lastIDProperty = serializedObject.FindProperty("lastID");


        EditorGUILayout.PropertyField(levelNameProperty, new GUIContent("Level Name"));
        EditorGUILayout.PropertyField(sampleLevelNameProperty, new GUIContent("Sample Level Name"));
        EditorGUILayout.PropertyField(levelSavingTypeProperty, new GUIContent("Level Saving Type"));
        EditorGUILayout.PropertyField(updateLevelProperty, new GUIContent("Update Level"));
        EditorGUILayout.PropertyField(lastIDProperty, new GUIContent("Last ID"));



        EditorGUILayout.Space(10);

        if (GUILayout.Button("Create Level"))
        {
            yGameManager.SaveLevel();
        }
        if (GUILayout.Button("Create Sample Level"))
        {
            yGameManager.CreateSampleLevel();
        }


        serializedObject.ApplyModifiedProperties();
    }
}
