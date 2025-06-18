
using GeometryDashAPI.Levels.GameObjects.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

[SelectionBase]
[ExecuteInEditMode]
[RequireComponent(typeof(YTransform))]
public class YMeshRenderer : YMonoBehaviour
{
    private const bool UPDATE_LODS_IN_EDITOR = false;

    private static List<int> points = new List<int>();
    private static List<int> collisionBlocks = new List<int>();

    public Mesh meshToCreate;


    public bool LODSystemEnabled = true;
    public YMeshLOD[] LODs = new YMeshLOD[0];

    public float cullDistance = 10;


    private int isLODActiveID = 0;

    protected virtual void Update()
    {
        if (gameObject.isStatic)
            LODSystemEnabled = false;
        float minDist = 0;
        foreach (var LOD in LODs)
        {

            if (!Application.isPlaying && !UPDATE_LODS_IN_EDITOR)
            {
                LOD.parent.gameObject.SetActive(true);
                break;
            }

            float dist = Vector3.Distance(YMainCamera.Instance.transform.position, transform.position);
            if (dist >= minDist && dist < LOD.distance)
                LOD.parent.gameObject.SetActive(true);
            else
                LOD.parent.gameObject.SetActive(false);

            minDist = LOD.distance;
        }
    }

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


        for (int i = 0; i < LODs.Length; i++)
        {
            if (LODs.Length > i + 1)
            {
                if (LODs[i].parent.GetComponentsInChildren<YVertex>().Length < LODs[i + 1].parent.GetComponentsInChildren<YVertex>().Length ||
                    LODs[i].parent.GetComponentsInChildren<YTriangle>().Length < LODs[i + 1].parent.GetComponentsInChildren<YTriangle>().Length)
                {
                    throw new Exception($"LODs in incorrect order in {gameObject.name}");
                }
            }
        }

        if (!YGameManager.Instance.transportingToGd)
            return;

        GetComponent<YTransform>().Init();
        float minDist = 0;
        if (LODSystemEnabled)
        {
            foreach (var LOD in LODs)
            {
                objects.AddRange(LODInit(LOD, minDist));

                minDist = LOD.distance;
            }
        }
        else
        {
            objects.AddRange(LODInit(new YMeshLOD() { parent = this.transform, distance = cullDistance }, minDist));
        }

    }

    private YGDObject[] LODInit(YMeshLOD LOD, float minDist = 0, bool isNotLOD = false)
    {
        List<YGDObject> objects = new List<YGDObject>();

        int LODGroupId = YGameManager.Instance.IDsManager.GetFreeGroup();
        YGameManager.Instance.IDsManager.AddGroup(LODGroupId);

        int vertexId = 0;
        int perfomanceUpdate = 1010;
        foreach (YVertex v in LOD.parent.GetComponentsInChildren<YVertex>())
        {
            int id1 = YGameManager.Instance.IDsManager.GetFreeIdFloat();
            YGameManager.Instance.IDsManager.AddVariable($"{gameObject.GetInstanceID()}.{LOD.parent.name}.vertices[{vertexId}].x", id1, true);
            int id2 = YGameManager.Instance.IDsManager.GetFreeIdFloat();
            YGameManager.Instance.IDsManager.AddVariable($"{gameObject.GetInstanceID()}.{LOD.parent.name}.vertices[{vertexId}].y", id2, true);
            int id3 = YGameManager.Instance.IDsManager.GetFreeIdFloat();
            YGameManager.Instance.IDsManager.AddVariable($"{gameObject.GetInstanceID()}.{LOD.parent.name}.vertices[{vertexId}].z", id3, true);
            int id4 = YGameManager.Instance.IDsManager.GetFreeIdFloatAndGroup();
            YGameManager.Instance.IDsManager.AddVariable($"{gameObject.GetInstanceID()}.{LOD.parent.name}.vertexObjects[{vertexId}]", id4, true);
            YGameManager.Instance.IDsManager.AddGroup(id4);

            v.xId = id1;
            v.yId = id2;
            v.zId = id3;
            v.objId = id4;

            vertexId++;

            var xItemEdit = new ItemEdit(id1, true, ItemEdit.Operation.Equals, gameObject.isStatic ? v.transform.position.x : v.transform.localPosition.x);
            var yItemEdit = new ItemEdit(id2, true, ItemEdit.Operation.Equals, gameObject.isStatic ? v.transform.position.y : v.transform.localPosition.y);
            var zItemEdit = new ItemEdit(id3, true, ItemEdit.Operation.Equals, gameObject.isStatic ? v.transform.position.z : v.transform.localPosition.z);
            Spawn spawn136;
            if (!gameObject.isStatic)
            {
                spawn136 = new Spawn(136, false, 0, new Dictionary<int, int>()
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
                    { 9989, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.sin.x") },
                    { 9988, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.sin.y") },
                    { 9987, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.sin.z") },
                    { 9986, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.x") },
                    { 9985, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.y") },
                    { 9984, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.z") },
                    { 9983, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.x") },
                    { 9982, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.y") },
                    { 9981, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.z") },
                    { 9980, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.state") },
                });
            }
            else
            {
                spawn136 = new Spawn(136, false, 0, new Dictionary<int, int>()
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


            if (YIDsManager.Instance.GetCurrentGroupName() == null)
            {
                xItemEdit.AddGroup(1001);
                yItemEdit.AddGroup(1001);
                zItemEdit.AddGroup(1001);
                spawn136.AddGroup(1001);
            }
            else
            {
                xItemEdit.AddGroup(YGameManager.Instance.groupsBeginGroup[YIDsManager.Instance.GetCurrentGroupName()]);
                yItemEdit.AddGroup(YGameManager.Instance.groupsBeginGroup[YIDsManager.Instance.GetCurrentGroupName()]);
                zItemEdit.AddGroup(YGameManager.Instance.groupsBeginGroup[YIDsManager.Instance.GetCurrentGroupName()]);
                spawn136.AddGroup(YGameManager.Instance.groupsBeginGroup[YIDsManager.Instance.GetCurrentGroupName()]);
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

        Dictionary<int, int> layersIds = new Dictionary<int, int>();

        foreach (YTriangle t in LOD.parent.GetComponentsInChildren<YTriangle>())
        {
            t.ValidateLayerParent();
            if (t.layerParent && !layersIds.ContainsKey(t.layer))
            {
                int group = YGameManager.Instance.IDsManager.GetFreeGroup();
                YGameManager.Instance.IDsManager.AddGroup(group);
                layersIds.Add(t.layer, group);
            }
        }

        foreach (YTriangle t in LOD.parent.GetComponentsInChildren<YTriangle>())
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
                YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + "." + LOD.parent.name + ".meshRenderer.collider" + colliderGroup, colliderGroup, true);

                var spawn35 = new Spawn(35, false, 0, new Dictionary<int, int> {
                    { 9999, t.vertices[0].zId },
                    { 9998, t.vertices[1].zId },
                    { 9997, t.vertices[2].zId },
                    { 9996, colliderGroup },
                });
                spawn35.AddGroups(1003, LODGroupId);// = new int[] { 1003 };

                var collisionTrigger = new Collision(layersIds[t.layer], 1, gradientOffGroup, false);

                if (YIDsManager.Instance.GetCurrentGroupName() == null)
                    collisionTrigger.AddGroup(1001);
                else
                    collisionTrigger.AddGroup(YGameManager.Instance.groupsBeginGroup[YIDsManager.Instance.GetCurrentGroupName()]);

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


            objects.Add(spawn146);
            objects.Add(gradientOn);
            objects.Add(gradientOn1);
            objects.Add(gradientOn2);
            objects.Add(gradientOff);
            objects.Add(gradientOff1);
            objects.Add(gradientOff2);
        }

        if (!isNotLOD)
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
            triggers.Add(new ItemCompare(9999, 0, true, true, 1, LOD.distance * LOD.distance, ItemCompare.Operation.More, triggers2.ToArray(), new YTrigger[] { trig }));

            foreach (var trigger in triggers)
                trigger.AddGroup(1003);

            objects.AddRange(triggers);
        }

        return objects.ToArray();
    }

    public void CreateMesh()
    {
        if (meshToCreate == null)
            return;

        GameObject g = new GameObject($"LOD {LODs.Length}");
        g.transform.parent = this.transform;
        g.transform.localPosition = Vector3.zero;
        g.transform.localRotation = Quaternion.identity;
        g.transform.localScale = Vector3.one;
        Transform transform = g.transform;

        List<YVertex> spawnedVertices = new List<YVertex>();

        var compressed = CompressMeshData(meshToCreate);

        foreach (var pos in compressed.Item1)
        {
            YVertex vertex = Instantiate(Resources.Load<YProjectSettings>("YProjectSettings").vertexPrefab, transform);
            vertex.transform.localPosition = pos;
            spawnedVertices.Add(vertex);
        }
        for (int i = 0; i < compressed.Item2.Length; i += 3)
        {
            YTriangle triangle = Instantiate(Resources.Load<YProjectSettings>("YProjectSettings").trianglePrefab);
            triangle.transform.SetParent(transform);
            triangle.vertices = new YVertex[3];
            triangle.vertices[0] = spawnedVertices[compressed.Item2[i]];
            triangle.vertices[1] = spawnedVertices[compressed.Item2[i + 1]];
            triangle.vertices[2] = spawnedVertices[compressed.Item2[i + 2]];

            //print(meshToCreate.triangles[i] + ", " + meshToCreate.triangles[i+1] + ", " + meshToCreate.triangles[i+2]);
            triangle.CreateMesh();
        }

        List<YMeshLOD> yMeshLODs = LODs.ToList();
        yMeshLODs.Add(new YMeshLOD() { parent = transform, distance = (yMeshLODs.Count > 0 ? yMeshLODs[yMeshLODs.Count - 1].distance + 10 : 10) });
        LODs = yMeshLODs.ToArray();

        meshToCreate = null;
    }

    private (Vector3[], int[]) CompressMeshData(Mesh mesh)
    {
        List<Vector3> vertices = new List<Vector3>();

        List<int> triangles = new List<int>();

        List<int> vertexIndex = new List<int>();

        foreach (var v in mesh.vertices)
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

        for (int i = 0; i < mesh.triangles.Length; i++)
        {
            triangles.Add(vertexIndex[mesh.triangles[i]]);
        }

        return ( vertices.ToArray(), triangles.ToArray() );
    }
}

[Serializable]
public class YMeshLOD
{
    public Transform parent;
    public float distance;
}