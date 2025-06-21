using UnityEngine;

public static class YColorManager
{
    private static UnityEngine.Color[] colorChannelsCopy = new UnityEngine.Color[0];
    public static UnityEngine.Color[] GetColors()
    {
        try
        {
            if (colorChannelsCopy == null || colorChannelsCopy.Length == 0)
                colorChannelsCopy = (UnityEngine.Color[])Resources.Load<YProjectSettings>("YProjectSettings").colorChannels.Clone();
        }
        catch
        { 
            if (colorChannelsCopy == null || colorChannelsCopy.Length < 1000)
            {
                colorChannelsCopy = new Color[1000];
            }
        }

        return colorChannelsCopy;
    }
    public static void InitColors()
    {
        try
        {
            colorChannelsCopy = (UnityEngine.Color[])Resources.Load<YProjectSettings>("YProjectSettings").colorChannels.Clone();
        }
        catch { }
    }
    public static void SetColor(int id, UnityEngine.Color color)
    {
        colorChannelsCopy[id] = color;
    }
}
