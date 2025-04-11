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


    public ItemCompare(int id1, int id2, bool is1float, bool is2float, float multiplier1, float multiplier2, Operation operation, YTrigger[] trueTriggers, YTrigger[] falseTriggers)
    {
        this.id1 = id1;
        this.id2 = id2;
        this.is1float = is1float;
        this.is2float = is2float;
        this.multiplier1 = multiplier1;
        this.multiplier2 = multiplier2;
        this.operation = operation;
        if (trueTriggers != null && trueTriggers.Length > 0) 
        {
            trueId = YGameManager.Instance.IDsManager.GetFreeGroup();
            YGameManager.Instance.IDsManager.AddGroup(trueId);
        }
        else
            trueId = 0;
        if (falseTriggers != null && falseTriggers.Length > 0)
        {
            falseId = YGameManager.Instance.IDsManager.GetFreeGroup();
            YGameManager.Instance.IDsManager.AddGroup(falseId);
        }
        else
            falseId = 0;
        this.trueTriggers = trueTriggers;
        this.falseTriggers = falseTriggers;
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
