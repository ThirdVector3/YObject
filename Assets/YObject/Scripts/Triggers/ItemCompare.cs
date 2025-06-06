﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class ItemCompare : YTrigger
{
    private int id1;
    private int id2;
    private bool is1float;
    private bool is2float;
    private float multiplier1;
    private float multiplier2;
    public enum Operation
    {
        Equals,
        More,
        MoreOrEquals,
        Less,
        LessOrEquals,
        NotEquals
    }
    private Operation operation;
    private int trueId;
    private int falseId;
    private YTrigger[] trueTriggers;
    private YTrigger[] falseTriggers;



    public ItemCompare(int id1, int id2, bool is1float, bool is2float, float multiplier1, float multiplier2, Operation operation, IEnumerable<YTrigger> trueTriggers, IEnumerable<YTrigger> falseTriggers) : base()
    {
        this.id1 = id1;
        this.id2 = id2;
        this.is1float = is1float;
        this.is2float = is2float;
        this.multiplier1 = multiplier1;
        this.multiplier2 = multiplier2;
        this.operation = operation;
        if (trueTriggers != null && trueTriggers.Count() > 0) 
        {
            foreach (var trig in trueTriggers)
                trig.isFirstLevel = false;
            trueId = YGameManager.Instance.IDsManager.GetFreeGroup();
            YGameManager.Instance.IDsManager.AddGroup(trueId);
        }
        else
            trueId = 0;
        if (falseTriggers != null && falseTriggers.Count() > 0)
        {
            foreach (var trig in falseTriggers)
                trig.isFirstLevel = false;
            falseId = YGameManager.Instance.IDsManager.GetFreeGroup();
            YGameManager.Instance.IDsManager.AddGroup(falseId);
        }
        else
            falseId = 0;
        this.trueTriggers = trueTriggers.ToArray();
        this.falseTriggers = falseTriggers.ToArray();

        ChangeChildrenFromFirstLayer();
    }
    public ItemCompare(int id1, int id2, bool is1float, bool is2float, float multiplier1, float multiplier2, Operation operation, IEnumerable<YTrigger> trueTriggers, IEnumerable<YTrigger> falseTriggers, int trueId, int falseId) : base()
    {
        this.id1 = id1;
        this.id2 = id2;
        this.is1float = is1float;
        this.is2float = is2float;
        this.multiplier1 = multiplier1;
        this.multiplier2 = multiplier2;
        this.operation = operation;
        this.trueId = trueId;
        this.falseId = falseId;
        this.trueTriggers = trueTriggers.ToArray();
        this.falseTriggers = falseTriggers.ToArray();

        if (trueTriggers != null)
            foreach (var trig in trueTriggers)
                trig.isFirstLevel = false;
        if (falseTriggers != null)
            foreach (var trig in falseTriggers)
                trig.isFirstLevel = false;

        ChangeChildrenFromFirstLayer();
    }
    public ItemCompare(YVariable var1, YVariable var2, Operation operation, IEnumerable<YTrigger> trueTriggers, IEnumerable<YTrigger> falseTriggers) : base()
    {
        this.id1 = var1.GetID();
        this.id2 = var2.GetID();
        this.is1float = var1.IsFloat();
        this.is2float = var2.IsFloat();
        this.multiplier1 = 1;
        this.multiplier2 = 1;
        this.operation = operation;
        if (trueTriggers != null && trueTriggers.Count() > 0)
        {
            foreach (var trig in trueTriggers)
                trig.isFirstLevel = false;
            trueId = YGameManager.Instance.IDsManager.GetFreeGroup();
            YGameManager.Instance.IDsManager.AddGroup(trueId);
        }
        else
            trueId = 0;
        if (falseTriggers != null && falseTriggers.Count() > 0)
        {
            foreach (var trig in falseTriggers)
                trig.isFirstLevel = false;
            falseId = YGameManager.Instance.IDsManager.GetFreeGroup();
            YGameManager.Instance.IDsManager.AddGroup(falseId);
        }
        else
            falseId = 0;
        this.trueTriggers = trueTriggers.ToArray();
        this.falseTriggers = falseTriggers.ToArray();

        ChangeChildrenFromFirstLayer();
    }


    public void ChangeChildrenFromFirstLayer()
    {
        if (trueTriggers != null)
        {
            foreach (var trig in trueTriggers)
            {
                trig.isFirstLevel = false;
                if (trig is ItemCompare)
                    ((ItemCompare)trig).ChangeChildrenFromFirstLayer();
            }
        }
        if (falseTriggers != null)
        {
            foreach (var trig in falseTriggers)
            {
                trig.isFirstLevel = false;
                if (trig is ItemCompare)
                    ((ItemCompare)trig).ChangeChildrenFromFirstLayer();
            }
        }
    }
    public override void Activate()
    {
        float a = is1float ? YGameManager.Instance.IDsManager.GetMemoryValue(id1).Item2 : YGameManager.Instance.IDsManager.GetMemoryValue(id1).Item1;
        float b = is2float ? YGameManager.Instance.IDsManager.GetMemoryValue(id2).Item2 : YGameManager.Instance.IDsManager.GetMemoryValue(id2).Item1;


        if (id2 == 0)
            b = 1;

        bool isTrue = false;

        switch (operation)
        {
            case Operation.Equals:
                isTrue = (a * multiplier1) == (b * multiplier2);
                break;
            case Operation.More:
                isTrue = (a * multiplier1) > (b * multiplier2);
                break;
            case Operation.MoreOrEquals:
                isTrue = (a * multiplier1) >= (b * multiplier2);
                break;
            case Operation.Less:
                isTrue = (a * multiplier1) < (b * multiplier2);
                break;
            case Operation.LessOrEquals:
                isTrue = (a * multiplier1) <= (b * multiplier2);
                break;
            case Operation.NotEquals:
                isTrue = (a * multiplier1) != (b * multiplier2);
                break;
        }

        if (isTrue)
        {
            foreach (var trigger in trueTriggers)
            {
                trigger.Activate();
            }
        }
        else
        {
            foreach (var trigger in falseTriggers)
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
            foreach(var trigger in trueTriggers)
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
