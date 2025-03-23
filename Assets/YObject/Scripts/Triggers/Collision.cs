using UnityEngine;

public class Collision : YTrigger
{
    private int groupActivate;
    private int collisionA;
    private int collisionB;
    private bool isOnExit;


    public Collision(int groupActivate, int collisionA, int collisionB, bool isOnExit)
    {
        this.groupActivate = groupActivate;
        this.collisionA = collisionA;
        this.collisionB = collisionB;
        this.isOnExit = isOnExit;
    }

    public override void Activate()
    {
        
    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        return $"1,1815,2,{pos.Value.x},3,{pos.Value.y}{GetGroupsString(groups, groupsParent)},155,1,62,1,87,1,36,1,51,{groupActivate},10,0.5,56,1,80,{collisionA},95,{collisionB},93,{(isOnExit ? 1 : 0)};";
    }
}
