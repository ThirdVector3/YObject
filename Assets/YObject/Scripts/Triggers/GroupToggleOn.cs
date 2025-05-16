
using System.Collections.Generic;
using UnityEngine;

public class GroupToggleOn : YTrigger
{
    private string groupName;

    public GroupToggleOn(string groupName) : base()
    {
        this.groupName = groupName;
    }

    public override void Activate()
    {
        foreach (var gr in YGameManager.Instance.groupsGameobject)
        {
            foreach (var go in gr.Value)
            {
                go.SetActive(false);
            }
        }
        if (groupName != null && YGameManager.Instance.groupsGameobject.ContainsKey(groupName))
        {
            foreach (var go in YGameManager.Instance.groupsGameobject[groupName])
            {
                go.SetActive(true);
            }
        }
        YGameManager.Instance.GameobjectGroupsManager.CurrentGroup = groupName;
        if (groupName != null && YGameManager.Instance.groupsBeginTriggers.ContainsKey(groupName))
        {
            foreach (YTrigger trigger in YGameManager.Instance.groupsBeginTriggers[groupName])
            {
                trigger.Activate();
            }
        }
    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        string ret = "";
        foreach (var gr in YGameManager.Instance.groupsGroup)
        {
            var toggle = new Toggle(gr.Value, false);
            toggle.AddGroups(this.groups);
            var stop = new Stop(gr.Value);
            stop.AddGroups(this.groups);
            ret += stop.GetString(pos);
            pos += new Vector2(2, 0);
            ret += toggle.GetString(pos);
            pos += new Vector2(2, 0);
        }
        if (groupName != null && YGameManager.Instance.groupsGroup.ContainsKey(groupName))
        {
            var toggle2 = new Toggle(YGameManager.Instance.groupsGroup[groupName], true);
            toggle2.AddGroups(this.groups);
            var spawn = new Spawn(YGameManager.Instance.groupsBeginGroup[groupName], false, 0, new Dictionary<int, int>());
            spawn.AddGroups(this.groups);
            ret += toggle2.GetString(pos);
            pos += new Vector2(2, 0);
            ret += spawn.GetString(pos);
        }
        return ret;
    }
}
