using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(YMeshRenderer))]
public class YMeshRendererEditor : Editor
{
    private YMeshRenderer meshRenderer;
    private int selectedColorChannel = 1;
    private bool isPainting = false;


    private void OnEnable()
    {
        meshRenderer = (YMeshRenderer)target;
        meshRenderer.GetComponent<MeshRenderer>().hideFlags = HideFlags.HideInInspector;
        meshRenderer.GetComponent<MeshFilter>().hideFlags = HideFlags.HideInInspector;

        meshRenderer.LoadMeshData();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("Hint: to draw press P", MessageType.Info);
        //base.OnInspectorGUI();

        EditorGUILayout.Space();

        if (GUILayout.Button("Initialize Mesh"))
        {
            meshRenderer.Initialize();
            SceneView.RepaintAll();
        }
        if (GUILayout.Button("Initialize All Meshes"))
        {
            meshRenderer.InitializeAll();
            SceneView.RepaintAll();
        }

        EditorGUILayout.Space();

        var serializedObject = new SerializedObject(target);

        var meshes = serializedObject.FindProperty("meshes");
        var selectedColorCorrector = serializedObject.FindProperty("selectedColorCorrector");
        var selectedColorChannel = serializedObject.FindProperty("selectedColorChannel");
        var layerEditMode = serializedObject.FindProperty("layerEditMode");
        var selectedLayerPaint = serializedObject.FindProperty("selectedLayerPaint");
        var renderLight = serializedObject.FindProperty("renderLight");
        var realtimeRenderLight = serializedObject.FindProperty("realtimeRenderLight");
        serializedObject.Update();



        EditorGUILayout.PropertyField(meshes, true);
        EditorGUILayout.PropertyField(renderLight, true);
        if (renderLight.boolValue)
            EditorGUILayout.PropertyField(realtimeRenderLight, true);

        EditorGUILayout.LabelField("Painting", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(selectedColorCorrector, true);
        EditorGUILayout.PropertyField(selectedColorChannel, true);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Layer painting", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(layerEditMode, true);
        EditorGUILayout.PropertyField(selectedLayerPaint, true);



        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        Event e = Event.current;

        HandlePaintingControls(e);
    }

    private void HandlePaintingControls(Event e)
    {
        if (e.type == EventType.KeyDown && e.keyCode == KeyCode.P)
        {
            isPainting = true;
        }
        if (e.type == EventType.KeyUp && e.keyCode == KeyCode.P)
        {
            isPainting = false;
        }

        if (isPainting && e.button == 0 && !e.control)
        {
            Undo.RecordObject(meshRenderer, "Paint triangle");
            meshRenderer.PaintTriangle(meshRenderer.SelectedColorCorrector, meshRenderer.SelectedColorChannel);

            e.Use();
            SceneView.RepaintAll();
        }
    }
}