using UnityEngine;

public class DebugLog : YTrigger
{
    public string text;

    public DebugLog(string text) : base()
    {
        this.text = text;
    }

    public override void Activate()
    {
        Debug.Log(text);
    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        return "";
    }
}