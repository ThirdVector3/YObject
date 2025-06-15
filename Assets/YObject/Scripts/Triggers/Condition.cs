using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Condition : YTrigger
{
    private int id1;
    private int id2;
    private bool is1float;
    private bool is2float;
    private float multiplier1;
    private float multiplier2;
    private ItemCompare.Operation operation;
    private int trueId;
    private int falseId;
    private YTrigger[] trueTriggers;
    private YTrigger[] falseTriggers;

    public Condition(YVariable var1, YVariable var2, ItemCompare.Operation operation)
    {
        this.id1 = var1.GetID();
        this.id2 = var2.GetID();
        this.is1float = var1.IsFloat();
        this.is2float = var2.IsFloat();
        this.multiplier1 = 1;
        this.multiplier2 = 1;
        this.operation = operation;
        trueTriggers = new YTrigger[0];
        falseTriggers = new YTrigger[0];
        trueId = 0;
        falseId = 0;
    }


    public override void Activate()
    {
        new ItemCompare(id1, id2, is1float, is2float, multiplier1, multiplier2, operation, trueTriggers, falseTriggers, trueId, falseId).Activate();
    }

    public Condition Then(Action block)
    {
        if (trueTriggers.Length != 0)
            throw new Exception("Can't do more than one Then in condition");
        YGameManager.Instance.RecordPool();

        block();

        trueTriggers = YGameManager.Instance.StopRecordPool();

        foreach (var trig in trueTriggers)
            trig.isFirstLevel = false;
        trueId = YGameManager.Instance.IDsManager.GetFreeGroup();
        YGameManager.Instance.IDsManager.AddGroup(trueId);

        return this;
    }
    public Condition Else(Action block)
    {
        if (falseTriggers.Length != 0)
            throw new Exception("Can't do more than one Else in condition");
        YGameManager.Instance.RecordPool();

        block();

        falseTriggers = YGameManager.Instance.StopRecordPool();

        foreach (var trig in falseTriggers)
            trig.isFirstLevel = false;
        falseId = YGameManager.Instance.IDsManager.GetFreeGroup();
        YGameManager.Instance.IDsManager.AddGroup(falseId);

        return this;
    }

    public override void AddGroup(int group, bool toChildren = false)
    {
        List<int> gs = groups.ToList();
        gs.Add(group);
        groups = gs.ToArray();
        if (toChildren)
        {
            foreach (var trigger in trueTriggers)
            {
                trigger.AddGroup(group, toChildren);
            }
            foreach (var trigger in falseTriggers)
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
            foreach (var trigger in trueTriggers)
            {
                trigger.AddGroupParent(group, toChildren);
            }
            foreach (var trigger in falseTriggers)
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
            foreach (var trigger in trueTriggers)
            {
                trigger.AddGroups(groups, toChildren);
            }
            foreach (var trigger in falseTriggers)
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
            foreach (var trigger in trueTriggers)
            {
                trigger.AddGroupsParent(groups, toChildren);
            }
            foreach (var trigger in falseTriggers)
            {
                trigger.AddGroupsParent(groups, toChildren);
            }
        }
    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        Vector2 triggersPos = pos.Value;
        string triggers = "";
        foreach (var trigger in trueTriggers)
        {
            trigger.AddGroup(trueId);
            triggers += trigger.GetString(triggersPos);
            triggersPos.x += 2;
        }
        foreach (var trigger in falseTriggers)
        {
            trigger.AddGroup(falseId);
            triggers += trigger.GetString(triggersPos);
            triggersPos.x += 2;
        }

        return $"1,3620,2,{pos.Value.x},3,{pos.Value.y}{GetGroupsString(groups, groupsParent)},155,1,62,1,87,1,36,1,80,{id1},95,{id2},51,{trueId},71,{falseId},476,{(is1float ? 2 : 1)},477,{(is2float ? 2 : 1)},479,{multiplier1},483,{multiplier2},480,3,481,3,482,{(int)operation};" + triggers;
    }
}
