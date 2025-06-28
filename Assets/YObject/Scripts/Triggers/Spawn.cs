
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
public class Spawn : YTrigger
{
    private int spawnID;
    private bool spawnOrdered;
    private float delay;
    private Dictionary<int, int> remap;

    public Spawn(int spawnID) : base()
    {
        this.spawnID = spawnID;
        this.spawnOrdered = false;
        this.delay = 0;
        this.remap = null;
    }
    public Spawn(int spawnID, float delay) : base()
    {
        this.spawnID = spawnID;
        this.spawnOrdered = false;
        this.delay = delay;
        this.remap = null;
    }
    public Spawn(int spawnID, bool spawnOrdered, float delay) : base()
    {
        this.spawnID = spawnID;
        this.spawnOrdered = spawnOrdered;
        this.delay = delay;
        this.remap = null;
    }
    public Spawn(int spawnID, bool spawnOrdered, float delay, Dictionary<int, int> remap) : base()
    {
        this.spawnID = spawnID;
        this.spawnOrdered = spawnOrdered;
        this.delay = delay;
        this.remap = remap;
    }

    public override void Activate()
    {
        
    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        string remapString = "";

        if (remap != null)
        {

            remapString = remap.Count == 0 ? "" : ",442,";

            foreach (var r in remap)
            {
                remapString += r.Key + "." + r.Value + ".";
            }

            if (remap.Count != 0)
            {
                remapString = remapString.Substring(0, remapString.Length - 1);
            }
        }

        return $"1,1268,2,{pos.Value.x},3,{pos.Value.y}{GetGroupsString(groups, groupsParent)},155,1,62,1,87,1,36,1,51,{spawnID},63,{delay.ToString(CultureInfo.InvariantCulture)},441,{(spawnOrdered ? 1 : 0)}{remapString};";
    }
}