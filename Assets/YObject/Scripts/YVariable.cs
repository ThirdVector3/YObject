using System;
using System.Collections.Generic;

public class YVariable
{
    public YVariable(int id, bool isFloat)
    {
        this.id = id;
        this.isFloat = isFloat;
    }


    private static int lastTmpId = 9999;
    private static int GetNewTmpId()
    {
        if (--lastTmpId < 9000)
            lastTmpId = 9999;
        return lastTmpId;
    }

    protected static int GetNewID(bool isFloat)
    {
        if (YGameManager.Instance.gameobjectsInitialization)
            return YIDsManager.Instance.AddVariable(
                Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks.ToString(),
                isFloat ? YIDsManager.Instance.GetFreeIdFloat() : YIDsManager.Instance.GetFreeIdInt(),
                isFloat
            );
        return GetNewTmpId();
    }

    protected int id;
    protected bool isFloat;
    public int GetID() => id;
    public bool IsFloat() => isFloat;

    #region operators

    public static YVariable operator +(YVariable a, YVariable b)
    {
        new ItemEdit(a.id, a.isFloat, ItemEdit.Operation.Add, 1, b.id, b.isFloat, 0, true, ItemEdit.Operation.Add);
        return a;
    }
    public static YVariable operator +(YVariable a, float b)
    {
        new ItemEdit(a.id, a.isFloat, ItemEdit.Operation.Add, b);
        return a;
    }
    public static YVariable operator +(float a, YVariable b)
    {
        new ItemEdit(b.id, b.isFloat, ItemEdit.Operation.Add, a);
        return b;
    }

    public static YVariable operator *(YVariable a, YVariable b)
    {
        new ItemEdit(a.id, a.isFloat, ItemEdit.Operation.Multiply, 1, b.id, b.isFloat, 0, true, ItemEdit.Operation.Add);
        return a;
    }
    public static YVariable operator *(YVariable a, float b)
    {
        new ItemEdit(a.id, a.isFloat, ItemEdit.Operation.Multiply, b);
        return a;
    }
    public static YVariable operator *(float a, YVariable b)
    {
        new ItemEdit(b.id, b.isFloat, ItemEdit.Operation.Multiply, a);
        return b;
    }

    public static YVariable operator -(YVariable a, YVariable b)
    {
        new ItemEdit(a.id, a.isFloat, ItemEdit.Operation.Subtract, 1, b.id, b.isFloat, 0, true, ItemEdit.Operation.Add);
        return a;
    }
    public static YVariable operator -(YVariable a, float b)
    {
        new ItemEdit(a.id, a.isFloat, ItemEdit.Operation.Subtract, b);
        return a;
    }
    public static YVariable operator -(float a, YVariable b)
    {
        YFloat yFloat = new YFloat(a);
        new ItemEdit(yFloat.id, yFloat.isFloat, ItemEdit.Operation.Subtract, 1, b.id, b.isFloat, 0, true, ItemEdit.Operation.Add);
        return yFloat;
    }

    public static YVariable operator /(YVariable a, YVariable b)
    {
        new ItemEdit(a.id, a.isFloat, ItemEdit.Operation.Divide, 1, b.id, b.isFloat, 0, true, ItemEdit.Operation.Add);
        return a;
    }
    public static YVariable operator /(YVariable a, float b)
    {
        new ItemEdit(a.id, a.isFloat, ItemEdit.Operation.Divide, b);
        return a;
    }
    public static YVariable operator /(float a, YVariable b)
    {
        YFloat yFloat = new YFloat(a);
        new ItemEdit(yFloat.id, yFloat.isFloat, ItemEdit.Operation.Divide, 1, b.id, b.isFloat, 0, true, ItemEdit.Operation.Add);
        return yFloat;
    }

    #endregion
}

public class YInt : YVariable
{
    public YInt() : base(GetNewID(false), false)
    {
    }
    public YInt(int value) : base(GetNewID(false), false)
    {
        var trig = new ItemEdit(id, false, ItemEdit.Operation.Equals, value);
        if (YGameManager.Instance.gameobjectsInitialization)
        {
            if (YIDsManager.Instance.GetCurrentGroupName() == null)
            {
                trig.AddGroup(1001);
                YGameManager.Instance.globalBeginTriggers.Add(trig);
            }
            else
            {
                trig.AddGroup(YGameManager.Instance.groupsBeginGroup[YIDsManager.Instance.GetCurrentGroupName()]);
                YGameManager.Instance.groupsBeginTriggers[YIDsManager.Instance.GetCurrentGroupName()].Add(trig);
            }
        }
    }
}
public class YFloat : YVariable
{
    public YFloat() : base(GetNewID(true), true)
    {
    }
    public YFloat(float value) : base(GetNewID(true), true)
    {
        var trig = new ItemEdit(id, true, ItemEdit.Operation.Equals, value);
        if (YGameManager.Instance.gameobjectsInitialization)
        {
            if (YIDsManager.Instance.GetCurrentGroupName() == null)
            {
                trig.AddGroup(1001);
                YGameManager.Instance.globalBeginTriggers.Add(trig);
            }
            else
            {
                trig.AddGroup(YGameManager.Instance.groupsBeginGroup[YIDsManager.Instance.GetCurrentGroupName()]);
                YGameManager.Instance.groupsBeginTriggers[YIDsManager.Instance.GetCurrentGroupName()].Add(trig);
            }
        }
    }
}