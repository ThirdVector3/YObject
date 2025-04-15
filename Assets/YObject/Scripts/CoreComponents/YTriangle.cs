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

    public Color color1Corrector = new Color(1, 0.5f, 0.5f);
    public Color color2Corrector = new Color(1, 0.5f, 0.5f);
    public Color color3Corrector = new Color(1, 0.5f, 0.5f);


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

        Color c1 = Resources.Load<YProjectSettings>("YProjectSettings").colorChannels[color1];
        Color c2 = Resources.Load<YProjectSettings>("YProjectSettings").colorChannels[color2];
        Color c3 = Resources.Load<YProjectSettings>("YProjectSettings").colorChannels[color3];



        Color.RGBToHSV(c1, out float h11, out float s11, out float v11);
        Color.RGBToHSV(color1Corrector, out float h12, out float s12, out float v12);
        h11 += h12;
        if (h11 > 1)
            h11 -= 1;
        s11 = Mathf.Clamp01(s11 + (s12 - 0.5f) * 2);
        v11 *= v12;
        c1 = Color.HSVToRGB(h11, s11, v11);

        Color.RGBToHSV(c2, out float h21, out float s21, out float v21);
        Color.RGBToHSV(color2Corrector, out float h22, out float s22, out float v22);
        h21 += h22;
        if (h21 > 1)
            h21 -= 1;
        s21 = Mathf.Clamp01(s21 + (s22 - 0.5f) * 2);
        v21 *= v22;
        c2 = Color.HSVToRGB(h21, s21, v21);

        Color.RGBToHSV(c3, out float h31, out float s31, out float v31);
        Color.RGBToHSV(color3Corrector, out float h32, out float s32, out float v32);
        h31 += h32;
        if (h31 > 1)
            h31 -= 1;
        s31 = Mathf.Clamp01(s31 + (s32 - 0.5f) * 2);
        v31 *= v32;
        c3 = Color.HSVToRGB(h31, s31, v31);


        meshRenderer.sharedMaterial.SetColor("_Color1", c1);
        meshRenderer.sharedMaterial.SetColor("_Color2", c2);
        meshRenderer.sharedMaterial.SetColor("_Color3", c3);
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