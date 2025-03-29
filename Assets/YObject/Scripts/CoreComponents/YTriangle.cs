using UnityEditor;
using UnityEngine;

public class YTriangle : MonoBehaviour
{
    public bool layerParent;
    public int layer = 0;
    public YVertex[] vertices = new YVertex[3];
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    //private MeshCollider meshCollider;

    public int color1;
    public int color2;
    public int color3;


    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        //meshCollider = GetComponent<MeshCollider>();
        //color1 = MainProjectData.gameColors[0];
        //color2 = MainProjectData.gameColors[0];
        //color3 = MainProjectData.gameColors[0];

        //ValidateLayerParent();
    }

    private void FixedUpdate()
    {
        UpdateColor();
    }
    public void CreateMesh()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        UpdateMesh();
    }
    public void UpdateMesh()
    {

        if (vertices.Length != 3 || vertices[0] == null || vertices[1] == null || vertices[2] == null)
            return;
        if (meshFilter == null)
        {
            meshFilter = GetComponent<MeshFilter>();
        }
        if (meshFilter.sharedMesh == null)
        {
            meshFilter.sharedMesh = new Mesh();
        }
        transform.position = Vector3.zero;
        transform.eulerAngles = Vector3.zero;
        //transform.localScale = OneDivideVector(transform.parent.localScale);
        meshFilter.sharedMesh.Clear();

        Vector3[] meshVertices = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            meshVertices[i] = vertices[i].transform.position;
        }
        int[] triangle = new int[] { 0, 1, 2 };

        meshFilter.sharedMesh.vertices = meshVertices;
        meshFilter.sharedMesh.triangles = triangle;
        meshFilter.sharedMesh.uv = new Vector2[] {
            Vector2.zero, Vector2.up, Vector2.right
        };
        meshFilter.sharedMesh.RecalculateNormals();
        //meshCollider.sharedMesh = meshFilter.sharedMesh;
        UpdateColor();
    }

    private void OnValidate()
    {
        //UpdateMesh();
        ValidateLayerParent();
        SetTriangleColors();
    }

    public void SetTriangleColors()
    {
        color1 = color1 == 0 ? 1 : color1;
        color2 = color2 == 0 ? 1 : color2;
        color3 = color3 == 0 ? 1 : color3;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Resources.Load<YProjectSettings>("YProjectSettings").triangleShader);
        meshRenderer.sharedMaterial.SetColor("_Color1", Resources.Load<YProjectSettings>("YProjectSettings").colorChannels[color1]);
        meshRenderer.sharedMaterial.SetColor("_Color2", Resources.Load<YProjectSettings>("YProjectSettings").colorChannels[color2]);
        meshRenderer.sharedMaterial.SetColor("_Color3", Resources.Load<YProjectSettings>("YProjectSettings").colorChannels[color3]);
    }

    private void ValidateLayerParent()
    {
        if (layerParent)
        {
            foreach (var item in FindObjectsOfType<YTriangle>())
            {
                if (item.transform.parent != transform.parent || item.transform == transform)
                    continue;

                if (item.layerParent && item.layer == layer)
                    item.layerParent = false;
            }
        }
        else
        {
            bool noLayerParents = true;
            foreach (var item in FindObjectsOfType<YTriangle>())
            {
                if (item.transform.parent != transform.parent || item.transform == transform)
                    continue;

                if (item.layerParent && item.layer == layer)
                {
                    noLayerParents = false;
                    break;
                }
            }

            if (noLayerParents)
            {
                foreach (var item in FindObjectsOfType<YTriangle>())
                {
                    if (item.transform.parent != transform.parent || item.transform == transform)
                        continue;

                    if (!item.layerParent && item.layer == layer)
                    {
                        item.layerParent = true;
                        break;
                    }
                }
            }
        }
    }

    private void UpdateColor()
    {
        //meshRenderer.material.SetColor("_Color1", color1.color);
        //meshRenderer.material.SetColor("_Color2", color2.color);
        //meshRenderer.material.SetColor("_Color3", color3.color);
    }
    private Vector3 OneDivideVector(Vector3 vector)
    {
        vector.x = 1 / vector.x;
        vector.y = 1 / vector.y;
        vector.z = 1 / vector.z;
        return vector;
    }

}