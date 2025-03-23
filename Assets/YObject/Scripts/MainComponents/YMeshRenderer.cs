using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(YTransform))]
public class YMeshRenderer : YMonoBehaviour
{
    public Mesh meshToCreate;

    public override YTrigger[] Begin()
    {
        return new YTrigger[0];
    }

    public override YGDObject[] Init()
    {
        List<YGDObject> objects = new List<YGDObject>();

        GetComponent<YTransform>().Init();

        int vertexId = 0; 
        foreach (YVertex v in GetComponentsInChildren<YVertex>())
        {
            int id1 = YGameManager.Instance.GetFreeIdFloat();
            YGameManager.Instance.AddVariable($"{gameObject.name}.vertices[{vertexId}].x", id1, true);
            int id2 = YGameManager.Instance.GetFreeIdFloat();
            YGameManager.Instance.AddVariable($"{gameObject.name}.vertices[{vertexId}].y", id2, true);
            int id3 = YGameManager.Instance.GetFreeIdFloat();
            YGameManager.Instance.AddVariable($"{gameObject.name}.vertices[{vertexId}].z", id3, true);
            int id4 = YGameManager.Instance.GetFreeIdFloat();
            YGameManager.Instance.AddVariable($"{gameObject.name}.vertexObjects[{vertexId}]", id3, true);
            YGameManager.Instance.AddGroup(id4);

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

                    { 9992, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.x") },
                    { 9991, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.y") },
                    { 9990, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.z") },
                    { 9989, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.sin.x") },
                    { 9988, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.sin.y") },
                    { 9987, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.sin.z") },
                    { 9986, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.cos.x") },
                    { 9985, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.cos.y") },
                    { 9984, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.cos.z") },
                    { 9983, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.x") },
                    { 9982, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.y") },
                    { 9981, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.z") },
                    { 9980, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.state") },
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


            xItemEdit.groups = new int[] { 1000 };
            yItemEdit.groups = new int[] { 1000 };
            zItemEdit.groups = new int[] { 1000 };
            spawn136.groups = new int[] { 1000 };
            vertexObject.groups = new int[] { id4 };


            objects.Add(xItemEdit);
            objects.Add(yItemEdit);
            objects.Add(zItemEdit);
            objects.Add(spawn136);
            objects.Add(vertexObject);
        }

        Dictionary<int, int> layersIds = new Dictionary<int, int>();

        foreach (YTriangle t in GetComponentsInChildren<YTriangle>())
        {
            if (t.layerParent)
            {
                int group = YGameManager.Instance.GetFreeGroup();
                YGameManager.Instance.AddGroup(group);
                layersIds.Add(t.layer, group);
            }
        }

        foreach (YTriangle t in GetComponentsInChildren<YTriangle>())
        {
            int gradientOffGroup = YGameManager.Instance.GetFreeGroup();
            YGameManager.Instance.AddGroup(gradientOffGroup);
            int gradientOnGroup = YGameManager.Instance.GetFreeGroup();
            YGameManager.Instance.AddGroup(gradientOnGroup);
            int gradientExtraGroup = YGameManager.Instance.GetFreeGroup();
            YGameManager.Instance.AddGroup(gradientExtraGroup);

            if (t.layerParent)
            {
                int colliderGroup = YGameManager.Instance.GetFreeIdFloatAndGroup();
                YGameManager.Instance.AddGroup(colliderGroup);
                YGameManager.Instance.AddVariable(gameObject.name + ".meshRenderer.collider" + colliderGroup, colliderGroup, true);

                var spawn35 = new Spawn(35, false, 0, new Dictionary<int, int> {
                    { 9999, t.vertices[0].zId },
                    { 9998, t.vertices[1].zId },
                    { 9997, t.vertices[2].zId },
                    { 9996, colliderGroup },
                });
                spawn35.groups = new int[] { 1003 };

                var collisionTrigger = new Collision(layersIds[t.layer], 1, gradientOffGroup, false);
                collisionTrigger.groups = new int[] { 1001 };

                var collisionObject = new CollisionObject(gradientOffGroup, false);
                collisionObject.groups = new int[] { colliderGroup };
                collisionObject.groupsParent = new int[] { colliderGroup };

                objects.Add(spawn35);
                objects.Add(collisionTrigger);
                objects.Add(collisionObject);
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
            spawn146.groups = new int[] { 1002 };

            var gradientOff = new Gradient(t.vertices[0].objId, t.vertices[1].objId, t.vertices[2].objId, t.vertices[2].objId, gradientOffGroup, true, 2, t.color1, Gradient.Type.Normal);
            gradientOff.groups = new int[] { gradientOffGroup, layersIds[t.layer] };
            var gradientOff1 = new Gradient(t.vertices[1].objId, t.vertices[2].objId, t.vertices[0].objId, t.vertices[0].objId, gradientOnGroup, true, 2, t.color2, Gradient.Type.Additive);
            gradientOff1.groups = new int[] { gradientOffGroup, layersIds[t.layer] };
            var gradientOff2 = new Gradient(t.vertices[2].objId, t.vertices[0].objId, t.vertices[1].objId, t.vertices[1].objId, gradientExtraGroup, true, 2, t.color3, Gradient.Type.Additive);
            gradientOff2.groups = new int[] { gradientOffGroup, layersIds[t.layer] };


            var gradientOn = new Gradient(t.vertices[0].objId, t.vertices[1].objId, t.vertices[2].objId, t.vertices[2].objId, gradientOffGroup, false, 2, t.color3, Gradient.Type.Normal);
            gradientOn.groups = new int[] { gradientOnGroup, layersIds[t.layer] };
            var gradientOn1 = new Gradient(t.vertices[1].objId, t.vertices[2].objId, t.vertices[0].objId, t.vertices[0].objId, gradientOnGroup, false, 2, t.color2, Gradient.Type.Additive);
            gradientOn1.groups = new int[] { gradientOnGroup, layersIds[t.layer] };
            var gradientOn2 = new Gradient(t.vertices[2].objId, t.vertices[0].objId, t.vertices[1].objId, t.vertices[1].objId, gradientExtraGroup, false, 2, t.color1, Gradient.Type.Additive);
            gradientOn2.groups = new int[] { gradientOnGroup, layersIds[t.layer] };


            objects.Add(spawn146);
            objects.Add(gradientOn);
            objects.Add(gradientOn1);
            objects.Add(gradientOn2);
            objects.Add(gradientOff);
            objects.Add(gradientOff1);
            objects.Add(gradientOff2);
        }

        return objects.ToArray();
    }

    public override YTrigger[] Tick()
    {
        return new YTrigger[0];
    }

    public void CreateMesh()
    {
        if (meshToCreate == null)
            return;


        List<YVertex> spawnedVertices = new List<YVertex>();

        var compressed = CompressMeshData(meshToCreate);


        print(meshToCreate.vertices.Length);
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
