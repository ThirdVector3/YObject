using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "YProjectSettings", menuName = "ScriptableObjects/YProjectSettings")]
public class YProjectSettings : ScriptableObject
{
    public Shader triangleShader;
    public YVertex vertexPrefab;
    public YTriangle trianglePrefab;
    public Color[] colorChannels;

    private void OnValidate()
    {
        if (colorChannels == null)
        {
            colorChannels = new Color[1000];
            for (int i = 0; i < colorChannels.Length; i++)
            {
                colorChannels[i] = Color.white;
            }
        }
        if (colorChannels.Length < 1000)
        {
            List<Color> cc = colorChannels.ToList();
            var cc2 = new Color[1000 - colorChannels.Length];
            for (int i = 0; i < cc2.Length; i++)
            {
                cc2[i] = Color.white;
            }
            cc.AddRange(cc2);
            colorChannels = cc.ToArray();
        }
        colorChannels[0] = Color.black;
        colorChannels[0].a = 0;
        colorChannels[2] = Color.black;

        foreach (var triangle in FindObjectsByType<YTriangle>(FindObjectsSortMode.None))
        {
            triangle.SetTriangleColors();
        }
    }
}
