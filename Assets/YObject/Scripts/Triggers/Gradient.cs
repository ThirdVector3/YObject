using UnityEngine;

public class Gradient : YTrigger
{
    private int blID;
    private int brID;
    private int tlID;
    private int trID;
    private int id;
    private bool disable;
    private int color1;
    private int color2;
    private int hue1;
    private float sat1;
    private float val1;
    private int hue2;
    private float sat2;
    private float val2;
    public enum Type
    {
        Normal,
        Additive,
        Multiply,
        Invert
    }
    private Type type;

    public Gradient(int bl, int br, int tl, int tr, int id, bool disable, int color1, int color2, Type type, int hue1 = 0, float sat1 = 0, float val1 = 1, int hue2 = 0, float sat2 = 0, float val2 = 1) : base()
    {
        blID = bl;
        brID = br;
        tlID = tl;
        trID = tr;
        this.disable = disable;
        this.color1 = color1;
        this.color2 = color2;
        this.id = id;
        this.hue1 = hue1;
        this.sat1 = sat1;
        this.val1 = val1;
        this.hue2 = hue2;
        this.sat2 = sat2;
        this.val2 = val2;
        this.type = type;
    }

    public override void Activate()
    {

    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        return $"1,2903,2,{pos.Value.x},3,{pos.Value.y}{GetGroupsString(groups, groupsParent)},155,1,156,2,21,{color1},22,{color2},41,1,43,{hue1}a{sat1}a{val1}a1a0,42,1,44,{hue2}a{sat2}a{val2}a1a1,62,1,87,1,36,1,174,{((int)type)},202,6,203,{blID},204,{brID},205,{tlID},206,{trID},207,1,208,{(disable ? 1 : 0)},209,{id},456,1;";
    }
}
