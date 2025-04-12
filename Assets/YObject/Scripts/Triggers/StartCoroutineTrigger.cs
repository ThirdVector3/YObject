
using System.Collections;
using UnityEngine;

public class StartCoroutineTrigger : YTrigger
{
    private int group;
    private CoroutineTrigger coroutine;
    public StartCoroutineTrigger(CoroutineTrigger coroutine)
    {
        group = coroutine.group;
        this.coroutine = coroutine;
    }

    public override void Activate()
    {
        coroutine.Activate();
    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        string ret = "";

        var spawn = new Spawn(group, true, 0, new System.Collections.Generic.Dictionary<int, int>());
        spawn.AddGroups(this.groups);
        ret += spawn.GetString(pos);

        return ret;
    }
}