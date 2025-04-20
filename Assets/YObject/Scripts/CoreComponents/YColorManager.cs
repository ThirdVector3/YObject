using UnityEngine;

public static class YColorManager
{
    private static UnityEngine.Color[] colorChannelsCopy = new UnityEngine.Color[0];
    public static UnityEngine.Color[] GetColors()
    {
        if (colorChannelsCopy == null || colorChannelsCopy.Length == 0)
            colorChannelsCopy = (UnityEngine.Color[])Resources.Load<YProjectSettings>("YProjectSettings").colorChannels.Clone();
        return colorChannelsCopy;
    }
    public static void InitColors()
    {
        colorChannelsCopy = (UnityEngine.Color[])Resources.Load<YProjectSettings>("YProjectSettings").colorChannels.Clone();
    }
    public static void SetColor(int id, UnityEngine.Color color)
    {
        colorChannelsCopy[id] = color;
    }
}
