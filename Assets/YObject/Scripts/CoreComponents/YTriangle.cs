using System.Threading.Tasks;
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

    public UnityEngine.Color color1Corrector = new UnityEngine.Color(0.5f, 0.25f, 0.25f);
    public UnityEngine.Color color2Corrector = new UnityEngine.Color(0.5f, 0.25f, 0.25f);
    public UnityEngine.Color color3Corrector = new UnityEngine.Color(0.5f, 0.25f, 0.25f);


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

        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        //transform.localScale = OneDivideVector(transform.parent.localScale);
        meshFilter.sharedMesh.Clear();

        Vector3[] meshVertices = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            meshVertices[i] = vertices[i].transform.localPosition;
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
        ValidateLayerParent();
        SetTriangleColors();
        UpdateMesh();
    }

    private async void UpdateMeshAsync()
    {
        await Task.Delay(1000);
        UpdateMesh();
    }

    public void SetTriangleColors()
    {
        color1 = color1 == 0 ? 1 : color1;
        color2 = color2 == 0 ? 1 : color2;
        color3 = color3 == 0 ? 1 : color3;

        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Shader Graphs/Triangle"));//Resources.Load<YProjectSettings>("YProjectSettings").triangleShader);

        UnityEngine.Color c1 = YColorManager.GetColors()[color1];
        UnityEngine.Color c2 = YColorManager.GetColors()[color2];
        UnityEngine.Color c3 = YColorManager.GetColors()[color3];



        UnityEngine.Color.RGBToHSV(c1, out float h11, out float s11, out float v11);
        UnityEngine.Color.RGBToHSV(color1Corrector, out float h12, out float s12, out float v12);
        h11 += h12;
        if (h11 > 1)
            h11 -= 1;
        s11 = Mathf.Clamp01(s11 + (s12 - 0.5f) * 2);
        v11 = Mathf.Clamp01(v11 + (v12 - 0.5f) * 2);
        c1 = UnityEngine.Color.HSVToRGB(h11, s11, v11);

        UnityEngine.Color.RGBToHSV(c2, out float h21, out float s21, out float v21);
        UnityEngine.Color.RGBToHSV(color2Corrector, out float h22, out float s22, out float v22);
        h21 += h22;
        if (h21 > 1)
            h21 -= 1;
        s21 = Mathf.Clamp01(s21 + (s22 - 0.5f) * 2);
        v21 = Mathf.Clamp01(v21 + (v22 - 0.5f) * 2);
        c2 = UnityEngine.Color.HSVToRGB(h21, s21, v21);

        UnityEngine.Color.RGBToHSV(c3, out float h31, out float s31, out float v31);
        UnityEngine.Color.RGBToHSV(color3Corrector, out float h32, out float s32, out float v32);
        h31 += h32;
        if (h31 > 1)
            h31 -= 1;
        s31 = Mathf.Clamp01(s31 + (s32 - 0.5f) * 2);
        v31 = Mathf.Clamp01(v31 + (v32 - 0.5f) * 2);
        c3 = UnityEngine.Color.HSVToRGB(h31, s31, v31);


        meshRenderer.sharedMaterial.SetColor("_Color1", c1);
        meshRenderer.sharedMaterial.SetColor("_Color2", c2);
        meshRenderer.sharedMaterial.SetColor("_Color3", c3);
    }

    public (int,int,int) GetCorrectorGDHues()
    {
        UnityEngine.Color.RGBToHSV(color1Corrector, out float h1, out float _, out float _);
        UnityEngine.Color.RGBToHSV(color2Corrector, out float h2, out float _, out float _);
        UnityEngine.Color.RGBToHSV(color3Corrector, out float h3, out float _, out float _);
        h1 = h1 * 360 > 180 ? h1 * 360 - 360 : h1 * 360;
        h2 = h2 * 360 > 180 ? h2 * 360 - 360 : h2 * 360;
        h3 = h3 * 360 > 180 ? h3 * 360 - 360 : h3 * 360;
        return ((int)h1, (int)h2, (int)h3);
    }
    public (float, float, float) GetCorrectorGDSaturations()
    {
        UnityEngine.Color.RGBToHSV(color1Corrector, out float _, out float s1, out float _);
        UnityEngine.Color.RGBToHSV(color2Corrector, out float _, out float s2, out float _);
        UnityEngine.Color.RGBToHSV(color3Corrector, out float _, out float s3, out float _);
        return (s1 * 2 - 1, s2 * 2 - 1, s3 * 2 - 1);
    }
    public (float, float, float) GetCorrectorGDValues()
    {
        UnityEngine.Color.RGBToHSV(color1Corrector, out float _, out float _, out float v1);
        UnityEngine.Color.RGBToHSV(color2Corrector, out float _, out float _, out float v2);
        UnityEngine.Color.RGBToHSV(color3Corrector, out float _, out float _, out float v3);
        return (v1 * 2 - 1, v2 * 2 - 1, v3 * 2 - 1);
    }

    public void ValidateLayerParent()
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
        SetTriangleColors();
    }
    private Vector3 OneDivideVector(Vector3 vector)
    {
        vector.x = 1 / vector.x;
        vector.y = 1 / vector.y;
        vector.z = 1 / vector.z;
        return vector;
    }

}