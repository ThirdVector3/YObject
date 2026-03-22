using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

[System.Serializable]
public class MeshLODData
{
    [SerializeField] public Mesh mesh;
    [SerializeField] public float distance;
}

[System.Serializable]
public class MeshData
{
    //public int[][] triangleColors;
    //public Color[][] triangleColorCorrectors;
    [SerializeField] private int[] triangleColorsFlat;
    [SerializeField] private int[] triangleColorsLengths;

    [SerializeField] private Color[] triangleColorCorrectorsFlat;
    [SerializeField] private int[] triangleColorCorrectorsLengths;

    [SerializeField] private float[] triangleLightLevelsFlat;
    [SerializeField] private int[] triangleLightLevelsLengths;

    public int[][] triangleColors
    {
        get
        {
            if (triangleColorsFlat == null || triangleColorsLengths == null)
                return null;

            int[][] result = new int[triangleColorsLengths.Length][];
            int index = 0;
            for (int i = 0; i < triangleColorsLengths.Length; i++)
            {
                result[i] = new int[triangleColorsLengths[i]];
                for (int j = 0; j < triangleColorsLengths[i]; j++)
                {
                    result[i][j] = triangleColorsFlat[index++];
                }
            }
            return result;
        }
        set
        {
            if (value == null)
            {
                triangleColorsFlat = null;
                triangleColorsLengths = null;
                return;
            }

            List<int> flat = new List<int>();
            List<int> lengths = new List<int>();

            foreach (var arr in value)
            {
                if (arr == null) continue;
                lengths.Add(arr.Length);
                flat.AddRange(arr);
            }

            triangleColorsFlat = flat.ToArray();
            triangleColorsLengths = lengths.ToArray();
        }
    }
    public Color[][] triangleColorCorrectors
    {
        get
        {
            if (triangleColorCorrectorsFlat == null || triangleColorCorrectorsLengths == null)
                return null;

            Color[][] result = new Color[triangleColorCorrectorsLengths.Length][];
            int index = 0;
            for (int i = 0; i < triangleColorCorrectorsLengths.Length; i++)
            {
                result[i] = new Color[triangleColorCorrectorsLengths[i]];
                for (int j = 0; j < triangleColorCorrectorsLengths[i]; j++)
                {
                    result[i][j] = triangleColorCorrectorsFlat[index++];
                }
            }
            return result;
        }
        set
        {
            if (value == null)
            {
                triangleColorCorrectorsFlat = null;
                triangleColorCorrectorsLengths = null;
                return;
            }

            List<Color> flat = new List<Color>();
            List<int> lengths = new List<int>();

            foreach (var arr in value)
            {
                if (arr == null) continue;
                lengths.Add(arr.Length);
                flat.AddRange(arr);
            }

            triangleColorCorrectorsFlat = flat.ToArray();
            triangleColorCorrectorsLengths = lengths.ToArray();
        }
    }
    public float[][] triangleLightLevels
    {
        get
        {
            if (triangleLightLevelsFlat == null || triangleLightLevelsLengths == null)
                return null;

            float[][] result = new float[triangleLightLevelsLengths.Length][];
            int index = 0;
            for (int i = 0; i < triangleLightLevelsLengths.Length; i++)
            {
                result[i] = new float[triangleLightLevelsLengths[i]];
                for (int j = 0; j < triangleLightLevelsLengths[i]; j++)
                {
                    result[i][j] = triangleLightLevelsFlat[index++];
                }
            }
            return result;
        }
        set
        {
            if (value == null)
            {
                triangleLightLevelsFlat = null;
                triangleLightLevelsLengths = null;
                return;
            }

            List<float> flat = new List<float>();
            List<int> lengths = new List<int>();

            foreach (var arr in value)
            {
                if (arr == null) continue;
                lengths.Add(arr.Length);
                flat.AddRange(arr);
            }

            triangleLightLevelsFlat = flat.ToArray();
            triangleLightLevelsLengths = lengths.ToArray();
        }
    }

    public int[] triangleLayers;

    public Vector3[] vertices;
    public int[] triangles;
    public Vector3[] normals;
    public Vector2[] uv;
    public Color[] colors;

    public MeshData()
    {
        triangleColorsFlat = new int[0];
        triangleColorsLengths = new int[0];
        triangleColorCorrectorsFlat = new Color[0];
        triangleColorCorrectorsLengths = new int[0];
        triangleLightLevelsFlat = new float[0];
        triangleLightLevelsLengths = new int[0];

        triangleLayers = new int[0];

        vertices = new Vector3[0];
        triangles = new int[0];
        normals = new Vector3[0];
        uv = new Vector2[0];
        colors = new Color[0];
    }
}


[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class YMeshRenderer : YMonoBehaviour
{
    public class YMeshRendererVertex
    {
        public int xWorld;
        public int yWorld;
        public int zWorld;

        public int xId;
        public int yId;
        public int zId;
        public int objId;
    }
    public class YMeshRendererTriangle
    {
        public bool layerParent;
        public int layer = 0;
        public YMeshRendererVertex[] vertices = new YMeshRendererVertex[3];

        public int color1;
        public int color2;
        public int color3;

        public int color1original = 0;
        public int color2original = 0;
        public int color3original = 0;

        public Color color1Corrector = new Color(0.5f, 0.25f, 0.25f);
        public Color color2Corrector = new Color(0.5f, 0.25f, 0.25f);
        public Color color3Corrector = new Color(0.5f, 0.25f, 0.25f);

        public (int, int, int) GetCorrectorGDHues()
        {
            Color.RGBToHSV(color1Corrector, out float h1, out float _, out float _);
            Color.RGBToHSV(color2Corrector, out float h2, out float _, out float _);
            Color.RGBToHSV(color3Corrector, out float h3, out float _, out float _);
            h1 = h1 * 360 > 180 ? h1 * 360 - 360 : h1 * 360;
            h2 = h2 * 360 > 180 ? h2 * 360 - 360 : h2 * 360;
            h3 = h3 * 360 > 180 ? h3 * 360 - 360 : h3 * 360;
            return ((int)h1, (int)h2, (int)h3);
        }
        public (float, float, float) GetCorrectorGDSaturations()
        {
            Color.RGBToHSV(color1Corrector, out float _, out float s1, out float _);
            Color.RGBToHSV(color2Corrector, out float _, out float s2, out float _);
            Color.RGBToHSV(color3Corrector, out float _, out float s3, out float _);
            return (s1 * 2 - 1, s2 * 2 - 1, s3 * 2 - 1);
        }
        public (float, float, float) GetCorrectorGDValues()
        {
            Color.RGBToHSV(color1Corrector, out float _, out float _, out float v1);
            Color.RGBToHSV(color2Corrector, out float _, out float _, out float v2);
            Color.RGBToHSV(color3Corrector, out float _, out float _, out float v3);
            return (v1 * 2 - 1, v2 * 2 - 1, v3 * 2 - 1);
        }
    }

    // for raycast in editor
    private static readonly MethodInfo intersectRayMeshMethod = typeof(HandleUtility).GetMethod("IntersectRayMesh", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
    private static GameObject lastGameObjectUnderCursor;


    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private Mesh originalMesh;
    private Mesh paintedMesh;
    private int[][] triangleColors;
    private Color[][] triangleColorCorrectors;
    private float[][] triangleLightLevels;
    private int[] triangleLayers;
    private List<Vector3> separatedVertices;
    private List<int> separatedTriangles;
    private List<Vector3> separatedNormals;
    private List<Vector2> separatedUVs;
    private List<Color> separatedColors;

    private int lastMesh = -1;
    private int currentMesh = -1;
    [SerializeField] private MeshLODData[] meshes;
    [SerializeField] private MeshData[] meshDatas;


    [SerializeField] private bool renderLight;
    [SerializeField] private bool realtimeRenderLight;


    [SerializeField] private Color selectedColorCorrector = new Color(0.5f, 0.25f, 0.25f);
    public Color SelectedColorCorrector { get { return selectedColorCorrector; } }
    [SerializeField] private int selectedColorChannel = 1;
    public int SelectedColorChannel { get { return selectedColorChannel; } }
    [SerializeField] private bool layerEditMode = false;
    [SerializeField] private int selectedLayerPaint = 0;


    private static List<int> points = new List<int>();
    private static List<int> collisionBlocks = new List<int>();
    private int isLODActiveID = 0;

    public override void Uninit()
    {
        base.Uninit();
        points = new List<int>();
        collisionBlocks = new List<int>();
    }


    public override void Init()
    {
        List<YGDObject> objects = new List<YGDObject>();

        isLODActiveID = YIDsManager.Instance.AddVariable($"{gameObject.GetInstanceID()}.MeshRenderer.isLODActive", YIDsManager.Instance.GetFreeIdInt(), false);

        if (!YGameManager.Instance.transportingToGd)
            return;

        if (GetComponent<YTransform>())
            GetComponent<YTransform>().Init();
        float minDist = 0;
        int LODIndex = 0;
        foreach (var LOD in meshes)
        {
            objects.AddRange(LODInit(CompressMeshData(meshDatas[LODIndex]), LODIndex, minDist, LOD.distance));

            minDist = LOD.distance;

            LODIndex++;
        }
    }

    private YGDObject[] LODInit(MeshData LOD, int LODIndex, float minDist, float maxDist, bool isNotLOD = false)
    {
        bool isStatic = GetComponent<YTransform>() == null;

        List<YGDObject> objects = new List<YGDObject>();

        int LODGroupId = YGameManager.Instance.IDsManager.GetFreeGroup();
        YGameManager.Instance.IDsManager.AddGroup(LODGroupId);

        int vertexId = 0;
        int perfomanceUpdate = 1010;

        List<YMeshRendererVertex> vertices = new List<YMeshRendererVertex>();
        foreach (var v in LOD.vertices)
        {
            vertices.Add(new YMeshRendererVertex());

            int id1 = YGameManager.Instance.IDsManager.GetFreeIdFloat();
            YGameManager.Instance.IDsManager.AddVariable($"{gameObject.GetInstanceID()}.{LODIndex}.vertices[{vertexId}].x", id1, true);
            int id2 = YGameManager.Instance.IDsManager.GetFreeIdFloat();
            YGameManager.Instance.IDsManager.AddVariable($"{gameObject.GetInstanceID()}.{LODIndex}.vertices[{vertexId}].y", id2, true);
            int id3 = YGameManager.Instance.IDsManager.GetFreeIdFloat();
            YGameManager.Instance.IDsManager.AddVariable($"{gameObject.GetInstanceID()}.{LODIndex}.vertices[{vertexId}].z", id3, true);
            int id4 = YGameManager.Instance.IDsManager.GetFreeIdFloatAndGroup();
            YGameManager.Instance.IDsManager.AddVariable($"{gameObject.GetInstanceID()}.{LODIndex}.vertexObjects[{vertexId}]", id4, true);
            YGameManager.Instance.IDsManager.AddGroup(id4);

            //v.xId = id1;
            //v.yId = id2;
            //v.zId = id3;
            //v.objId = id4;

            vertices.Last().xId = id1;
            vertices.Last().yId = id2;
            vertices.Last().zId = id3;
            vertices.Last().objId = id4;


            int idWorldX = 9978;
            int idWorldY = 9977;
            int idWorldZ = 9976;


            if (renderLight && realtimeRenderLight)
            {
                idWorldX = YGameManager.Instance.IDsManager.GetFreeIdFloat();
                YGameManager.Instance.IDsManager.AddVariable($"{gameObject.GetInstanceID()}.{LODIndex}.vertices[{vertexId}].globalx", idWorldX, true);
                idWorldY = YGameManager.Instance.IDsManager.GetFreeIdFloat();
                YGameManager.Instance.IDsManager.AddVariable($"{gameObject.GetInstanceID()}.{LODIndex}.vertices[{vertexId}].globaly", idWorldY, true);
                idWorldZ = YGameManager.Instance.IDsManager.GetFreeIdFloat();
                YGameManager.Instance.IDsManager.AddVariable($"{gameObject.GetInstanceID()}.{LODIndex}.vertices[{vertexId}].globalz", idWorldZ, true);
            }

            vertices.Last().xWorld = idWorldX;
            vertices.Last().yWorld = idWorldY;
            vertices.Last().zWorld = idWorldZ;


            vertexId++;

            var xItemEdit = new ItemEdit(id1, true, ItemEdit.Operation.Equals, isStatic ? transform.TransformPoint(v).x : v.x);
            var yItemEdit = new ItemEdit(id2, true, ItemEdit.Operation.Equals, isStatic ? transform.TransformPoint(v).y : v.y);
            var zItemEdit = new ItemEdit(id3, true, ItemEdit.Operation.Equals, isStatic ? transform.TransformPoint(v).z : v.z);


            Spawn spawn136;
            if (!isStatic)
            {
                spawn136 = new Spawn(Y3DService.Get().localToCameraPosTranslateFunctionID, false, 0, new Dictionary<int, int>()
                {
                    { 9999, id1 },
                    { 9998, id2 },
                    { 9997, id3 },
                    { 9996, id1 },
                    { 9995, id2 },
                    { 9994, id3 },
                    { 9993, id4 },

                    { 9992, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x") },
                    { 9991, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y") },
                    { 9990, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z") },
                    { 9989, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x") },
                    { 9988, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y") },
                    { 9987, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z") },
                    { 9986, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.w") },
                    { 9985, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.y") },
                    { 9984, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.z") },
                    { 9983, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.x") },
                    { 9982, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.y") },
                    { 9981, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.z") },
                    { 9980, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.state") },

                    { 9976, idWorldX },
                    { 9975, idWorldY },
                    { 9974, idWorldZ },
                });
            }
            else
            {
                spawn136 = new Spawn(Y3DService.Get().localToCameraPosTranslateFunctionID, false, 0, new Dictionary<int, int>()
                {
                    { 9999, id1 },
                    { 9998, id2 },
                    { 9997, id3 },
                    { 9996, id1 },
                    { 9995, id2 },
                    { 9994, id3 },
                    { 9993, id4 },

                    { 9992, 23 },
                    { 9991, 23 },
                    { 9990, 23 },
                    { 9989, 23 },
                    { 9988, 23 },
                    { 9987, 23 },
                    { 9986, 23 },
                    { 9985, 23 },
                    { 9984, 23 },
                    { 9983, 23 },
                    { 9982, 23 },
                    { 9981, 23 },
                    { 9980, 23 },

                    { 9976, idWorldX },
                    { 9975, idWorldY },
                    { 9974, idWorldZ },
                });
            }
            var vertexObject = new SimpleObject();


            xItemEdit.AddGroups(perfomanceUpdate, LODGroupId);// = new int[] { 1000 };
            yItemEdit.AddGroups(perfomanceUpdate, LODGroupId);// = new int[] { 1000 };
            zItemEdit.AddGroups(perfomanceUpdate, LODGroupId);// = new int[] { 1000 };
            spawn136.AddGroups(perfomanceUpdate, LODGroupId);// = new int[] { 1000 };
            vertexObject.AddGroup(id4);// = new int[] { id4 };
            vertexObject.AddGroupParent(id4);// = new int[] { id4 };
            vertexObject.useGroupsGroup = false;


            if (YGameobjectGroupsManager.Instance.CurrentGroupCompile == null)
            {
                xItemEdit.AddGroup(1001);
                yItemEdit.AddGroup(1001);
                zItemEdit.AddGroup(1001);
                spawn136.AddGroup(1001);
            }
            else
            {
                xItemEdit.AddGroup(YGameManager.Instance.groupsBeginGroup[YGameobjectGroupsManager.Instance.CurrentGroupCompile]);
                yItemEdit.AddGroup(YGameManager.Instance.groupsBeginGroup[YGameobjectGroupsManager.Instance.CurrentGroupCompile]);
                zItemEdit.AddGroup(YGameManager.Instance.groupsBeginGroup[YGameobjectGroupsManager.Instance.CurrentGroupCompile]);
                spawn136.AddGroup(YGameManager.Instance.groupsBeginGroup[YGameobjectGroupsManager.Instance.CurrentGroupCompile]);
            }

            objects.Add(xItemEdit);
            objects.Add(yItemEdit);
            objects.Add(zItemEdit);
            objects.Add(spawn136);
            if (!points.Contains(id4))
            {
                objects.Add(vertexObject);
                points.Add(id4);
            }
            perfomanceUpdate++;
            if (perfomanceUpdate > 1015)
                perfomanceUpdate = 1010;
        }

        List<YMeshRendererTriangle> triangles = new List<YMeshRendererTriangle>();
        List<int> layerParentsExist = new List<int>();
        //int lastUsedColor = 999;
        for (int i = 0; i < LOD.triangleLayers.Length; i++)
        {
            bool layerParent = !layerParentsExist.Contains(LOD.triangleLayers[i]);
            if (layerParent)
                layerParentsExist.Add(LOD.triangleLayers[i]);
            if (renderLight && realtimeRenderLight)
            {
                int color1 = YIDsManager.Instance.GetFreeColor();
                YIDsManager.Instance.AddColor(color1);
                int color2 = YIDsManager.Instance.GetFreeColor();
                YIDsManager.Instance.AddColor(color2);
                int color3 = YIDsManager.Instance.GetFreeColor();
                YIDsManager.Instance.AddColor(color3);
                triangles.Add(new YMeshRendererTriangle
                {
                    vertices = new YMeshRendererVertex[] { vertices[LOD.triangles[i * 3]], vertices[LOD.triangles[i * 3 + 1]], vertices[LOD.triangles[i * 3 + 2]] },
                    layer = LOD.triangleLayers[i],
                    color1 = color1,
                    color2 = color2,
                    color3 = color3,
                    color1Corrector = LOD.triangleColorCorrectors[i][0],
                    color2Corrector = LOD.triangleColorCorrectors[i][1],
                    color3Corrector = LOD.triangleColorCorrectors[i][2],
                    layerParent = layerParent,
                    color1original = LOD.triangleColors[i][0],
                    color2original = LOD.triangleColors[i][1],
                    color3original = LOD.triangleColors[i][2],
                });
                //lastUsedColor -= 3;
            }
            else if (renderLight && !realtimeRenderLight)
            {
                triangles.Add(new YMeshRendererTriangle
                {
                    vertices = new YMeshRendererVertex[] { vertices[LOD.triangles[i * 3]], vertices[LOD.triangles[i * 3 + 1]], vertices[LOD.triangles[i * 3 + 2]] },
                    layer = LOD.triangleLayers[i],
                    color1 = LOD.triangleColors[i][0],
                    color2 = LOD.triangleColors[i][1],
                    color3 = LOD.triangleColors[i][2],
                    color1Corrector = LOD.triangleColorCorrectors[i][0] * LOD.triangleLightLevels[i][0],
                    color2Corrector = LOD.triangleColorCorrectors[i][1] * LOD.triangleLightLevels[i][1],
                    color3Corrector = LOD.triangleColorCorrectors[i][2] * LOD.triangleLightLevels[i][2],
                    layerParent = layerParent
                });
            }
            else
            {
                triangles.Add(new YMeshRendererTriangle
                {
                    vertices = new YMeshRendererVertex[] { vertices[LOD.triangles[i * 3]], vertices[LOD.triangles[i * 3 + 1]], vertices[LOD.triangles[i * 3 + 2]] },
                    layer = LOD.triangleLayers[i],
                    color1 = LOD.triangleColors[i][0],
                    color2 = LOD.triangleColors[i][1],
                    color3 = LOD.triangleColors[i][2],
                    color1Corrector = LOD.triangleColorCorrectors[i][0],
                    color2Corrector = LOD.triangleColorCorrectors[i][1],
                    color3Corrector = LOD.triangleColorCorrectors[i][2],
                    layerParent = layerParent
                });
            }
        }


        Dictionary<int, int> layersIds = new Dictionary<int, int>();

        foreach (var t in triangles)
        {
            if (t.layerParent && !layersIds.ContainsKey(t.layer))
            {
                int group = YGameManager.Instance.IDsManager.GetFreeGroup();
                YGameManager.Instance.IDsManager.AddGroup(group);
                layersIds.Add(t.layer, group);
            }
        }

        foreach (var t in triangles)
        {
            int gradientOffGroup = YGameManager.Instance.IDsManager.GetFreeGroup();
            YGameManager.Instance.IDsManager.AddGroup(gradientOffGroup);
            int gradientOnGroup = YGameManager.Instance.IDsManager.GetFreeGroup();
            YGameManager.Instance.IDsManager.AddGroup(gradientOnGroup);
            int gradientExtraGroup = YGameManager.Instance.IDsManager.GetFreeGroup();
            YGameManager.Instance.IDsManager.AddGroup(gradientExtraGroup);

            int gradientID1 = YGameManager.Instance.IDsManager.GetFreeGradient();
            YGameManager.Instance.IDsManager.AddGradient(gradientID1);
            int gradientID2 = YGameManager.Instance.IDsManager.GetFreeGradient();
            YGameManager.Instance.IDsManager.AddGradient(gradientID2);
            int gradientID3 = YGameManager.Instance.IDsManager.GetFreeGradient();
            YGameManager.Instance.IDsManager.AddGradient(gradientID3);

            if (t.layerParent)
            {
                int colliderGroup = YGameManager.Instance.IDsManager.GetFreeIdFloatAndGroup();
                YGameManager.Instance.IDsManager.AddGroup(colliderGroup);
                YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + "." + LODIndex + ".meshRenderer.collider" + colliderGroup, colliderGroup, true);

                var spawn35 = new Spawn(35, false, 0, new Dictionary<int, int> {
                    { 9999, t.vertices[0].zId },
                    { 9998, t.vertices[1].zId },
                    { 9997, t.vertices[2].zId },
                    { 9996, colliderGroup },
                });
                spawn35.AddGroups(1003, LODGroupId);// = new int[] { 1003 };

                var collisionTrigger = new CollisionTrigger(layersIds[t.layer], 1, gradientOffGroup, false);

                if (YGameobjectGroupsManager.Instance.CurrentGroupCompile == null)
                    collisionTrigger.AddGroup(1001);
                else
                    collisionTrigger.AddGroup(YGameManager.Instance.groupsBeginGroup[YGameobjectGroupsManager.Instance.CurrentGroupCompile]);

                objects.Add(spawn35);
                objects.Add(collisionTrigger);



                if (!collisionBlocks.Contains(colliderGroup))
                {
                    var collisionObject = new CollisionObject(gradientOffGroup, false);
                    collisionObject.AddGroup(colliderGroup);// = new int[] { colliderGroup };
                    collisionObject.AddGroupParent(colliderGroup);// = new int[] { colliderGroup };
                    collisionObject.useGroupsGroup = false;
                    objects.Add(collisionObject);
                    collisionBlocks.Add(colliderGroup);
                    //points.Add(colliderGroup);
                }


            }

            var spawn146 = new Spawn(146, false, 0, new Dictionary<int, int> {
                    { 9999, t.vertices[0].zId },
                    { 9998, t.vertices[1].zId },
                    { 9997, t.vertices[2].zId },
                    { 999, t.vertices[0].xId },
                    { 998, t.vertices[0].yId },
                    { 997, t.vertices[1].xId },
                    { 996, t.vertices[1].yId },
                    { 995, t.vertices[2].xId },
                    { 994, t.vertices[2].yId },

                    { 989, gradientOffGroup },
                    { 987, gradientOnGroup },
                });
            spawn146.AddGroup(1002);// = new int[] { 1002 };
            spawn146.AddGroup(LODGroupId);

            if (renderLight && realtimeRenderLight)
            {
                var colorSet1 = new ColorTrigger(t.color1, 0, Color.red, t.color1original);
                var colorSet2 = new ColorTrigger(t.color2, 0, Color.red, t.color2original);
                var colorSet3 = new ColorTrigger(t.color3, 0, Color.red, t.color3original);
                colorSet1.AddGroups(1002, LODGroupId);
                colorSet2.AddGroups(1002, LODGroupId);
                colorSet3.AddGroups(1002, LODGroupId);



                var spawn523 = new Spawn(523, false, 0, new Dictionary<int, int> {
                        { 9999, t.vertices[0].zWorld },
                        { 9998, t.vertices[1].zWorld },
                        { 9997, t.vertices[2].zWorld },
                        { 999, t.vertices[0].xWorld },
                        { 998, t.vertices[0].yWorld },
                        { 997, t.vertices[1].xWorld },
                        { 996, t.vertices[1].yWorld },
                        { 995, t.vertices[2].xWorld },
                        { 994, t.vertices[2].yWorld },

                        { 993, t.color1 },
                        { 992, t.color2 },
                        { 991, t.color3 },
                    });
                spawn523.AddGroup(1002);
                spawn523.AddGroup(LODGroupId);
            }

            var gradientOff = new Gradient(t.vertices[0].objId, t.vertices[1].objId, t.vertices[2].objId, t.vertices[2].objId, gradientID1, true, 2, t.color1, Gradient.Type.Normal);
            gradientOff.AddGroups(new int[] { gradientOffGroup, layersIds[t.layer], LODGroupId });
            var gradientOff1 = new Gradient(t.vertices[1].objId, t.vertices[2].objId, t.vertices[0].objId, t.vertices[0].objId, gradientID2, true, 2, t.color2, Gradient.Type.Additive);
            gradientOff1.AddGroups(new int[] { gradientOffGroup, layersIds[t.layer], LODGroupId });
            var gradientOff2 = new Gradient(t.vertices[2].objId, t.vertices[0].objId, t.vertices[1].objId, t.vertices[1].objId, gradientID3, true, 2, t.color3, Gradient.Type.Additive);
            gradientOff2.AddGroups(new int[] { gradientOffGroup, layersIds[t.layer], LODGroupId });


            var gradientOn = new Gradient(t.vertices[0].objId, t.vertices[1].objId, t.vertices[2].objId, t.vertices[2].objId, gradientID1, false, 2, t.color3, Gradient.Type.Normal, hue2: t.GetCorrectorGDHues().Item3, sat2: t.GetCorrectorGDSaturations().Item3, val2: t.GetCorrectorGDValues().Item3);
            gradientOn.AddGroups(new int[] { gradientOnGroup, layersIds[t.layer], LODGroupId });
            var gradientOn1 = new Gradient(t.vertices[1].objId, t.vertices[2].objId, t.vertices[0].objId, t.vertices[0].objId, gradientID2, false, 2, t.color2, Gradient.Type.Additive, hue2: t.GetCorrectorGDHues().Item2, sat2: t.GetCorrectorGDSaturations().Item2, val2: t.GetCorrectorGDValues().Item2);
            gradientOn1.AddGroups(new int[] { gradientOnGroup, layersIds[t.layer], LODGroupId });
            var gradientOn2 = new Gradient(t.vertices[2].objId, t.vertices[0].objId, t.vertices[1].objId, t.vertices[1].objId, gradientID3, false, 2, t.color1, Gradient.Type.Additive, hue2: t.GetCorrectorGDHues().Item1, sat2: t.GetCorrectorGDSaturations().Item1, val2: t.GetCorrectorGDValues().Item1);
            gradientOn2.AddGroups(new int[] { gradientOnGroup, layersIds[t.layer], LODGroupId });


            //objects.Add(spawn146);
            //if (renderLight)
            //{
            //    objects.Add(colorSet1);
            //    objects.Add(colorSet2);
            //    objects.Add(colorSet3);
            //    objects.Add(spawn523);
            //}
            //objects.Add(gradientOn);
            //objects.Add(gradientOn1);
            //objects.Add(gradientOn2);
            //objects.Add(gradientOff);
            //objects.Add(gradientOff1);
            //objects.Add(gradientOff2);
        }

        if (!isNotLOD && GetComponent<YTransform>())
        {

            List<YTrigger> triggers = new List<YTrigger>();

            List<YTrigger> triggers1 = new List<YTrigger>()
            {
                new ItemEdit(9999, true, ItemEdit.Operation.Equals, 1, 1, true, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Subtract),
                new ItemEdit(9998, true, ItemEdit.Operation.Equals, 1, 2, true, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Subtract),
                new ItemEdit(9997, true, ItemEdit.Operation.Equals, 1, 3, true, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Subtract),

                new ItemEdit(9999, true, ItemEdit.Operation.Equals, 1, 9999, true, 9999, true, ItemEdit.Operation.Multiply),
                new ItemEdit(9999, true, ItemEdit.Operation.Add, 1, 9998, true, 9998, true, ItemEdit.Operation.Multiply),
                new ItemEdit(9999, true, ItemEdit.Operation.Add, 1, 9997, true, 9997, true, ItemEdit.Operation.Multiply),

                new ItemEdit(9500, true, ItemEdit.Operation.Equals, 1, 9997, true, 0, true, ItemEdit.Operation.Add),
            };

            List<YTrigger> triggers2 = new List<YTrigger>()
            {
                new Stop(LODGroupId),
                new Toggle(LODGroupId, false),
                new ItemEdit(isLODActiveID, false, ItemEdit.Operation.Equals, 0)
            };

            List<YTrigger> triggers3 = new List<YTrigger>()
            {
                new Toggle(LODGroupId, true),
                //new Spawn(LODGroupId, false, 0, new Dictionary<int, int>())
                new ItemCompare(isLODActiveID, 0, false, false, 1, 0, ItemCompare.Operation.Equals,
                new YTrigger[]
                {
                    new Spawn(LODGroupId, false, 0, new Dictionary<int, int>()),
                    new ItemEdit(isLODActiveID, false, ItemEdit.Operation.Equals, 1)
                },
                new YTrigger[0]),
            };

            var trig = new ItemCompare(9999, 0, true, true, 1, minDist * minDist, ItemCompare.Operation.More, triggers3.ToArray(), triggers2.ToArray());

            triggers.AddRange(triggers1);
            triggers.Add(new ItemCompare(9999, 0, true, true, 1, maxDist * maxDist, ItemCompare.Operation.More, triggers2.ToArray(), new YTrigger[] { trig }));

            foreach (var trigger in triggers)
                trigger.AddGroups(1003, LODGroupId);

            objects.AddRange(triggers);
        }

        return objects.ToArray();
    }


    public void Initialize()
    {

        if (meshFilter == null)
            meshFilter = GetComponent<MeshFilter>();
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();

        if (currentMesh == -1)
            return;


        meshFilter.mesh = meshes[currentMesh].mesh;

        originalMesh = meshFilter.sharedMesh;

        CreateSeparatedMesh();

        int triangleCount = separatedTriangles.Count / 3;
        triangleColorCorrectors = new Color[triangleCount][];
        triangleLightLevels = new float[triangleCount][];
        triangleColors = new int[triangleCount][];
        triangleLayers = new int[triangleCount];

        for (int i = 0; i < triangleColorCorrectors.Length; i++)
        {
            triangleColorCorrectors[i] = new Color[3];
            triangleColorCorrectors[i][0] = new Color(0.5f, 0.25f, 0.25f);
            triangleColorCorrectors[i][1] = new Color(0.5f, 0.25f, 0.25f);
            triangleColorCorrectors[i][2] = new Color(0.5f, 0.25f, 0.25f);
        }
        for (int i = 0; i < triangleColors.Length; i++)
        {
            triangleColors[i] = new int[3];
            triangleColors[i][0] = 1;
            triangleColors[i][1] = 1;
            triangleColors[i][2] = 1;
        }
        for (int i = 0; i < triangleLightLevels.Length; i++)
        {
            triangleLightLevels[i] = new float[3];
            triangleLightLevels[i][0] = 1;
            triangleLightLevels[i][1] = 1;
            triangleLightLevels[i][2] = 1;
        }

        UpdateMeshColors();

        SaveMeshData(); 
    }
    public void InitializeAll()
    {
        //currentMesh = -1;
        //LoadMeshData();
        int tmpMesh = currentMesh;
        for (int i = 0;i < meshes.Length; i++)
        {
            currentMesh = i;
            Initialize();
        }
        currentMesh = tmpMesh;
        LoadMeshData();
    }
    private void CreateSeparatedMesh()
    {
        Vector3[] originalVertices = originalMesh.vertices;
        int[] originalTriangles = originalMesh.triangles;
        Vector3[] originalNormals = originalMesh.normals;
        Vector2[] originalUVs = originalMesh.uv;

        separatedVertices = new List<Vector3>();
        separatedTriangles = new List<int>();
        separatedNormals = new List<Vector3>();
        separatedUVs = new List<Vector2>();
        separatedColors = new List<Color>();

        for (int i = 0; i < originalTriangles.Length; i += 3)
        {
            int triangleIndex = i / 3;

            for (int j = 0; j < 3; j++)
            {
                int originalVertexIndex = originalTriangles[i + j];

                separatedVertices.Add(originalVertices[originalVertexIndex]);
                separatedNormals.Add(originalNormals != null && originalNormals.Length > originalVertexIndex ?
                    originalNormals[originalVertexIndex] : Vector3.up);
                separatedUVs.Add(originalUVs != null && originalUVs.Length > originalVertexIndex ?
                    originalUVs[originalVertexIndex] : Vector2.zero);
                separatedColors.Add(Color.white);

                separatedTriangles.Add(separatedVertices.Count - 1);
            }
        }

        paintedMesh = new Mesh();
        paintedMesh.vertices = separatedVertices.ToArray();
        paintedMesh.triangles = separatedTriangles.ToArray();
        paintedMesh.normals = separatedNormals.ToArray();
        paintedMesh.uv = separatedUVs.ToArray();
        paintedMesh.colors = separatedColors.ToArray();

        paintedMesh.RecalculateBounds();
        paintedMesh.RecalculateTangents();

        meshFilter.mesh = paintedMesh;
    }
    public static bool RaycastAgainstScene(out RaycastHit hit)
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

        // First, try raycasting against scene geometry with or without colliders (it doesn't matter)
        // Credit: https://forum.unity.com/threads/editor-raycast-against-scene-meshes-without-collider-editor-select-object-using-gui-coordinate.485502
        GameObject gameObjectUnderCursor;
        switch (Event.current.type)
        {
            // HandleUtility.PickGameObject doesn't work with some EventTypes in OnSceneGUI
            case EventType.Layout:
            case EventType.Repaint:
            case EventType.ExecuteCommand: gameObjectUnderCursor = lastGameObjectUnderCursor; break;
            default: gameObjectUnderCursor = HandleUtility.PickGameObject(Event.current.mousePosition, false); break;
        }

        if (gameObjectUnderCursor)
        {
            Mesh meshUnderCursor = null;
            if (gameObjectUnderCursor.TryGetComponent(out MeshFilter meshFilter))
                meshUnderCursor = meshFilter.sharedMesh;
            if (!meshUnderCursor && gameObjectUnderCursor.TryGetComponent(out SkinnedMeshRenderer skinnedMeshRenderer))
                meshUnderCursor = skinnedMeshRenderer.sharedMesh;

            if (meshUnderCursor)
            {
                // Remember this GameObject so that it can be used inside problematic EventTypes, as well
                lastGameObjectUnderCursor = gameObjectUnderCursor;

                object[] rayMeshParameters = new object[] { ray, meshUnderCursor, gameObjectUnderCursor.transform.localToWorldMatrix, null };
                if ((bool)intersectRayMeshMethod.Invoke(null, rayMeshParameters))
                {
                    hit = (RaycastHit)rayMeshParameters[3];
                    return true;
                }
            }
            else
                lastGameObjectUnderCursor = null;
        }

        // Raycast against scene geometry with colliders
        object raycastResult = HandleUtility.RaySnap(ray);
        if (raycastResult != null && raycastResult is RaycastHit)
        {
            hit = (RaycastHit)raycastResult;
            return true;
        }

        hit = new RaycastHit();
        return false;
    }
    public void PaintTriangle(Color color, int colorChannel)
    {
        RaycastHit hit;
        if (RaycastAgainstScene(out hit))
        {
            if (hit.transform == transform) return;

            int triangleIndex = hit.triangleIndex;

            if (!layerEditMode)
            {
                Color[] colors = triangleColorCorrectors[triangleIndex];
                int[] colorChannels = triangleColors[triangleIndex];

                float[] distances = new float[3];
                distances[0] = Vector3.Distance(transform.TransformPoint(separatedVertices[separatedTriangles[triangleIndex * 3]]), hit.point);
                distances[1] = Vector3.Distance(transform.TransformPoint(separatedVertices[separatedTriangles[triangleIndex * 3 + 1]]), hit.point);
                distances[2] = Vector3.Distance(transform.TransformPoint(separatedVertices[separatedTriangles[triangleIndex * 3 + 2]]), hit.point);

                float minDistance = Mathf.Min(distances);

                for (int i = 0; i < 3; i++)
                {
                    if (minDistance == distances[i])
                    {
                        colors[i] = color;
                        colorChannels[i] = colorChannel;
                        break;
                    }
                }
                PaintTriangleByIndex(triangleIndex, colors, colorChannels);
            }
            else
                EditTriangleLayerByIndex(triangleIndex, selectedLayerPaint);

            if (!Application.isPlaying)
                SaveMeshData();
        }
    }
    public void PaintTriangleByIndex(int triangleIndex, Color[] colors, int[] colorChannels)
    {
        if (triangleIndex >= 0 && triangleIndex < triangleColors.Length)
        {
            triangleColors[triangleIndex] = colorChannels;
            triangleColorCorrectors[triangleIndex] = colors;
            UpdateTriangleColor(triangleIndex, GetChannelPlusCorrectorColors(colorChannels, colors));
        }
    }
    public void EditTriangleLayerByIndex(int triangleIndex, int layer)
    {
        if (triangleIndex >= 0 && triangleIndex < triangleColors.Length)
        {
            triangleLayers[triangleIndex] = layer;
            Color layerEditColor = Color.HSVToRGB((0.347f * layer) % 1f, 1, 1);
            UpdateTriangleColor(triangleIndex, new Color[] { layerEditColor, layerEditColor, layerEditColor });
        }
    }
    private void UpdateTriangleColor(int triangleIndex, Color[] colors)
    {
        int startVertexIndex = triangleIndex * 3;

        for (int i = 0; i < 3; i++)
        {
            if (startVertexIndex + i < separatedColors.Count)
            {
                separatedColors[startVertexIndex + i] = colors[i];
            }
        }

        paintedMesh.colors = separatedColors.ToArray();

    }
    private void UpdateMeshColors()
    {
        for (int i = 0; i < triangleColors.Length; i++)
        {
            if (layerEditMode)
            {
                Color layerEditColor = Color.HSVToRGB((0.347f * triangleLayers[i]) % 1f, 1, 1);
                UpdateTriangleColor(i, new Color[] { layerEditColor, layerEditColor, layerEditColor });
            }
            else
                UpdateTriangleColor(i, GetLightedTriangleColors(triangleColors[i], triangleColorCorrectors[i], new Vector3[] {
                    separatedVertices[separatedTriangles[i*3]],
                    separatedVertices[separatedTriangles[i*3+1]],
                    separatedVertices[separatedTriangles[i*3+2]]
                }, i));
        }

    }
    public void BakeLighting()
    {
        for (int i = 0; i < triangleColors.Length; i++)
        {
            var vertices = new Vector3[] { 
                transform.TransformPoint(separatedVertices[separatedTriangles[i * 3]]),
                transform.TransformPoint(separatedVertices[separatedTriangles[i * 3 + 1]]), 
                transform.TransformPoint(separatedVertices[separatedTriangles[i * 3 + 2]])
            };

            float[] lights = new float[3];
            foreach (var lightSource in FindObjectsByType<YLightSource>(FindObjectsSortMode.None))
            {
                var lightLevels = lightSource.GetLightLevel(vertices);
                lights[0] += lightLevels[0];
                lights[1] += lightLevels[1];
                lights[2] += lightLevels[2];
            }

            triangleLightLevels[i] = lights;
        }
        SaveMeshData();
    }
    private Color[] GetLightedTriangleColors(int[] channels, Color[] correctors, Vector3[] vertices, int triangleIndex)
    {
        Color[] colors = GetChannelPlusCorrectorColors(channels, correctors);

        if (renderLight && realtimeRenderLight)
        {
            vertices = new Vector3[] { transform.TransformPoint(vertices[0]), transform.TransformPoint(vertices[1]), transform.TransformPoint(vertices[2]) };

            float[] lights = new float[3];
            foreach (var lightSource in FindObjectsByType<YLightSource>(FindObjectsSortMode.None))
            {
                var lightLevels = lightSource.GetLightLevel(vertices);
                lights[0] += lightLevels[0];
                lights[1] += lightLevels[1];
                lights[2] += lightLevels[2];
            }

            colors[0] *= lights[0];
            colors[1] *= lights[1];
            colors[2] *= lights[2];
        }
        if (renderLight && !realtimeRenderLight)
        {
            colors[0] *= triangleLightLevels[triangleIndex][0];
            colors[1] *= triangleLightLevels[triangleIndex][1];
            colors[2] *= triangleLightLevels[triangleIndex][2];
        }

        return colors;
    }
    private Color[] GetChannelPlusCorrectorColors(int[] channels, Color[] correctors)
    {
        Color[] colors = new Color[channels.Length];

        for (int i = 0; i < channels.Length; i++)
        {
            colors[i] = GetChannelPlusCorrectorColor(channels[i], correctors[i]);
        }


        return colors;
    }
    private Color GetChannelPlusCorrectorColor(int channel, Color corrector)
    {
        channel = channel < 1 ? 1 : channel;

        UnityEngine.Color c1 = YColorManager.GetColors()[channel];

        UnityEngine.Color.RGBToHSV(c1, out float h11, out float s11, out float v11);
        UnityEngine.Color.RGBToHSV(corrector, out float h12, out float s12, out float v12);
        h11 += h12;
        if (h11 > 1)
            h11 -= 1;
        s11 = Mathf.Clamp01(s11 + (s12 - 0.5f) * 2);
        v11 = Mathf.Clamp01(v11 + (v12 - 0.5f) * 2);
        c1 = UnityEngine.Color.HSVToRGB(h11, s11, v11);

        return c1;
    }
    public void SaveMeshData()
    {
        if (currentMesh == -1)
            return;
        MeshData data = new MeshData
        {
            triangleColors = triangleColors,
            triangleColorCorrectors = triangleColorCorrectors,
            triangleLightLevels = triangleLightLevels,
            triangleLayers = triangleLayers,
            vertices = paintedMesh.vertices,
            triangles = paintedMesh.triangles,
            normals = paintedMesh.normals,
            uv = paintedMesh.uv,
            colors = separatedColors.ToArray()
        };
        meshDatas[currentMesh] = data;
    }
    public void LoadMeshData()
    {
        MeshData data;
        if (currentMesh == -1)
            data = new MeshData();
        else
            data = meshDatas[currentMesh];
        triangleColors = data.triangleColors;
        triangleColorCorrectors = data.triangleColorCorrectors;
        triangleLightLevels = data.triangleLightLevels;
        triangleLayers = data.triangleLayers;

        paintedMesh = new Mesh();
        paintedMesh.vertices = data.vertices;
        paintedMesh.triangles = data.triangles;
        paintedMesh.normals = data.normals;
        paintedMesh.uv = data.uv;

        meshFilter.mesh = paintedMesh;
        
        separatedVertices = new List<Vector3>(data.vertices);
        separatedTriangles = new List<int>(data.triangles);
        separatedNormals = new List<Vector3>(data.normals);
        separatedUVs = new List<Vector2>(data.uv);
        separatedColors = new List<Color>(data.colors);

        UpdateMeshColors();
    }
    private MeshData CompressMeshData(MeshData meshData)
    {
        List<Vector3> vertices = new List<Vector3>();

        List<int> triangles = new List<int>();

        List<int> vertexIndex = new List<int>();

        foreach (var v in meshData.vertices)
        {
            if (!vertices.Contains(v))
            {
                vertexIndex.Add(vertices.Count);
                vertices.Add(v);
            }
            else
            {
                vertexIndex.Add(vertices.IndexOf(v));
            }
        }

        for (int i = 0; i < meshData.triangles.Length; i++)
        {
            triangles.Add(vertexIndex[meshData.triangles[i]]);
        }

        return new MeshData {
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray(),
            colors = meshData.colors,
            normals = meshData.normals,
            uv = meshData.uv,
            triangleLayers = meshData.triangleLayers,
            triangleColorCorrectors = meshData.triangleColorCorrectors,
            triangleLightLevels = meshData.triangleLightLevels,
            triangleColors = meshData.triangleColors
        };
    }
    private void OnValidate()
    {
        if (meshFilter == null)
            meshFilter = GetComponent<MeshFilter>();
        if (meshRenderer == null)
            meshRenderer = GetComponent<MeshRenderer>();

        meshRenderer.material = Resources.Load<YProjectSettings>("YProjectSettings").meshRendererMaterial;


        if (meshDatas != null && meshDatas.Length != meshes.Length)
        {
            MeshData[] datas = new MeshData[meshes.Length];
            for (int i = 0; i < datas.Length; i++)
            {
                if (meshDatas.Length <= i)
                    break;
                datas[i] = meshDatas[i];
            }
            meshDatas = datas;
        }
        else if (meshDatas == null)
        {
            meshDatas = new MeshData[0];
        }

        LoadMeshData();
    }
    private void Update()
    {
        currentMesh = -1;
        if (meshes != null)
        {
            for (int i = 0; i < meshes.Length; i++)
            {
                float lastDistance = 0;
                if (i > 0)
                    lastDistance = meshes[i - 1].distance;

                float distanceToCamera = !Application.isPlaying ? Vector3.Distance(transform.position, SceneView.lastActiveSceneView.camera.transform.position)
                    : Vector3.Distance(transform.position, YMainCamera.Instance.transform.position);

                if (meshes[i].distance > distanceToCamera &&
                        lastDistance < distanceToCamera)
                {
                    currentMesh = i;
                    break;
                }
            }
        }

        if (lastMesh != currentMesh)
        {
            LoadMeshData();
            lastMesh = currentMesh;
        }
        UpdateMeshColors();
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            UnityEditor.EditorApplication.QueuePlayerLoopUpdate();
        }
    }
}
