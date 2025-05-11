using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomTrigger : YTrigger
{
    private float percentage;
    private int id1;
    private int id2;
    private YTrigger[] triggers1;
    private YTrigger[] triggers2;

    public RandomTrigger(float percentage, YTrigger[] triggers1, YTrigger[] triggers2) : base()
    {
        this.percentage = percentage;

        if (triggers1 != null && triggers1.Length > 0)
        {
            foreach (var trig in triggers1)
                trig.isFirstLevel = false;
            id1 = YGameManager.Instance.IDsManager.GetFreeGroup();
            YGameManager.Instance.IDsManager.AddGroup(id1);
        }
        else
            id1 = 0;
        if (triggers2 != null && triggers2.Length > 0)
        {
            foreach (var trig in triggers2)
                trig.isFirstLevel = false;
            id2 = YGameManager.Instance.IDsManager.GetFreeGroup();
            YGameManager.Instance.IDsManager.AddGroup(id2);
        }
        else
            id2 = 0;

        this.triggers1 = triggers1;
        this.triggers2 = triggers2;
    }
    public RandomTrigger(float percentage, int id1, int id2, YTrigger[] triggers1, YTrigger[] triggers2) : base()
    {
        this.percentage = percentage;
        this.id1 = id1;
        this.id2 = id2;
        this.triggers1 = triggers1;
        this.triggers2 = triggers2;

        if (triggers1 != null)
            foreach (var trig in triggers1)
                trig.isFirstLevel = false;
        if (triggers2 != null)
            foreach (var trig in triggers2)
                trig.isFirstLevel = false;
    }

    public override void Activate()
    {
        if (Random.Range(0,100f) <= percentage)
        {
            foreach (var trigger in triggers1)
            {
                trigger.Activate();
            }
        }
        else
        {
            foreach (var trigger in triggers2)
            {
                trigger.Activate();
            }
        }
    }

    public override void AddGroup(int group, bool toChildren = false)
    {
        List<int> gs = groups.ToList();
        gs.Add(group);
        groups = gs.ToArray();
        if (toChildren)
        {
            foreach (var trigger in triggers1)
            {
                trigger.AddGroup(group, toChildren);
            }
            foreach (var trigger in triggers2)
            {
                trigger.AddGroup(group, toChildren);
            }
        }
    }
    public override void AddGroupParent(int group, bool toChildren = false)
    {
        List<int> gs = groupsParent.ToList();
        gs.Add(group);
        groupsParent = gs.ToArray();
        if (toChildren)
        {
            foreach (var trigger in triggers1)
            {
                trigger.AddGroupParent(group, toChildren);
            }
            foreach (var trigger in triggers2)
            {
                trigger.AddGroupParent(group, toChildren);
            }
        }
    }
    public override void AddGroups(params int[] groups)
    {
        List<int> gs = this.groups.ToList();
        gs.AddRange(groups);
        this.groups = gs.ToArray();
    }
    public override void AddGroups(int[] groups, bool toChildren = false)
    {
        List<int> gs = this.groups.ToList();
        gs.AddRange(groups);
        this.groups = gs.ToArray();
        if (toChildren)
        {
            foreach (var trigger in triggers1)
            {
                trigger.AddGroups(groups, toChildren);
            }
            foreach (var trigger in triggers2)
            {
                trigger.AddGroups(groups, toChildren);
            }
        }
    }
    public override void AddGroupsParent(int[] groups, bool toChildren = false)
    {
        List<int> gs = groupsParent.ToList();
        gs.AddRange(groups);
        groupsParent = gs.ToArray();
        if (toChildren)
        {
            foreach (var trigger in triggers1)
            {
                trigger.AddGroupsParent(groups, toChildren);
            }
            foreach (var trigger in triggers2)
            {
                trigger.AddGroupsParent(groups, toChildren);
            }
        }
    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        Vector2 triggersPos = pos.Value;
        string triggers = "";
        foreach (var trigger in triggers1)
        {
            trigger.AddGroup(id1);
            triggers += trigger.GetString(triggersPos);
            triggersPos.x += 2;
        }
        foreach (var trigger in triggers2)
        {
            trigger.AddGroup(id2);
            triggers += trigger.GetString(triggersPos);
            triggersPos.x += 2;
        }

        return $"1,1912,2,{pos.Value.x},3,{pos.Value.y}{GetGroupsString(groups, groupsParent)},62,1,87,1,36,1,51,{id1},71,{id2},10,{percentage};" + triggers;
    }
}
