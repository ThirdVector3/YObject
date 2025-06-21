using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(YTriangle)), CanEditMultipleObjects]
public class YTriangleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Reset correctors"))
        {
            YTriangle yTriangle = (YTriangle)target;
            yTriangle.ResetCorrectors();
        }
    }
}
