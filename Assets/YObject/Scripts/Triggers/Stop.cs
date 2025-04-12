using UnityEngine;

public class Stop : YTrigger
{
    private int id;

    public Stop(int id)
    {
        this.id = id;
    }

    public override void Activate()
    {

    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        return $"1,1616,2,{pos.Value.x},3,{pos.Value.y}{GetGroupsString(groups, groupsParent)},62,1,87,1,36,1,51,{id};";
    }
}