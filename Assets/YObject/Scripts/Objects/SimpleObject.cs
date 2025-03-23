using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleObject : YGDObject
{
    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        return $"1,3802,2,{pos.Value.x},3,{pos.Value.y},135,1{GetGroupsString(groups,groupsParent)},155,1;";
    }
}
