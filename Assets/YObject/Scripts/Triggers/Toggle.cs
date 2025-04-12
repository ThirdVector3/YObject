using System.Globalization;
using UnityEngine;

public class Toggle : YTrigger
{
    private int id;
    private bool on;

    public Toggle(int id, bool on)
    {
        this.id = id;
        this.on = on;
    }

    public override void Activate()
    {

    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        return $"1,1049,2,{pos.Value.x},3,{pos.Value.y}{GetGroupsString(groups, groupsParent)},62,1,87,1,36,1,51,{id},56,{(on ? 1 : 0)};";
    }
}
