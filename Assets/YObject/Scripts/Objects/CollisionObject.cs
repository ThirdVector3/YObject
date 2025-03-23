using UnityEngine;

public class CollisionObject : YGDObject
{
    private bool isDynamic;
    private int id;

    public CollisionObject(int id, bool isDynamic)
    {
        this.id = id;
        this.isDynamic = isDynamic;
    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        return $"1,1816,2,{pos.Value.x},3,{pos.Value.y}{GetGroupsString(groups, groupsParent)},155,1,36,1,80,{id},94,{(isDynamic ? 1 : 0)};";
    }
}
