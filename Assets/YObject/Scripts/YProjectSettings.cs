using AYellowpaper.SerializedCollections;
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
    [SerializedDictionary("ID", "AudioClip")]
    public SerializedDictionary<int, AudioClip> songs;
    public UnityEngine.Color[] colorChannels;

    private void OnValidate()
    {
        if (colorChannels == null)
        {
            colorChannels = new UnityEngine.Color[1000];
            for (int i = 0; i < colorChannels.Length; i++)
            {
                colorChannels[i] = UnityEngine.Color.white;
            }
        }
        if (colorChannels.Length < 1000)
        {
            List<UnityEngine.Color> cc = colorChannels.ToList();
            var cc2 = new UnityEngine.Color[1000 - colorChannels.Length];
            for (int i = 0; i < cc2.Length; i++)
            {
                cc2[i] = UnityEngine.Color.white;
            }
            cc.AddRange(cc2);
            colorChannels = cc.ToArray();
        }
        colorChannels[0] = UnityEngine.Color.black;
        colorChannels[0].a = 0;
        colorChannels[2] = UnityEngine.Color.black;

        foreach (var triangle in FindObjectsByType<YTriangle>(FindObjectsSortMode.None))
        {
            triangle.SetTriangleColors();
        }
    }
}
