using UnityEngine;

public class YWaitForSeconds : YTrigger
{
    public float seconds;

    public YWaitForSeconds(float seconds) : base()
    { 
        this.seconds = seconds;
    }

    public override void Activate()
    {

    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        return "";
    }
}