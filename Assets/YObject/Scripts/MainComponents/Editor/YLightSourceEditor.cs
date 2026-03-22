using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(YLightSource))]
public class YLightSourceEditor : Editor
{
    private YLightSource lightSource;

    private void OnEnable()
    {
        lightSource = (YLightSource)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();

        if (GUILayout.Button("Bake All Lights"))
        {
            lightSource.BakeAllLights();
        }
    }
}
