using System;
using System.Collections.Generic;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class YVariable
{
    public YVariable(int id, bool isFloat)
    {
        this.id = id;
        this.isFloat = isFloat;
    }

    public YVariable(string name)
    {
        this.id = YIDsManager.Instance.GetIdByName(name);
        this.isFloat = YIDsManager.Instance.GetIsFloatByName(name);
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
    public static implicit operator int(YVariable yVariable) { return yVariable.id; }


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




    public static YVariable operator ==(YVariable a, YVariable b)
    {
        YVariable aMinusB = new YFloat();
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, b.id, b.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add);
        return 1 - aMinusB;
    }
    public static YVariable operator ==(YVariable a, float b)
    {
        YVariable aMinusB = new YFloat();
        YVariable bFloat = new YFloat(b);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, bFloat.id, bFloat.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add);
        return 1 - aMinusB;
    }
    public static YVariable operator ==(float a, YVariable b)
    {
        YVariable aMinusB = new YFloat();
        YVariable aFloat = new YFloat(a);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, aFloat.id, aFloat.isFloat, b.id, b.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add);
        return 1 - aMinusB;
    }



    public static YVariable operator !=(YVariable a, YVariable b)
    {
        YVariable aMinusB = new YFloat();
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, b.id, b.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add);
        return aMinusB;
    }
    public static YVariable operator !=(YVariable a, float b)
    {
        YVariable aMinusB = new YFloat();
        YVariable bFloat = new YFloat(b);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, bFloat.id, bFloat.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add);
        return aMinusB;
    }
    public static YVariable operator !=(float a, YVariable b)
    {
        YVariable aMinusB = new YFloat();
        YVariable aFloat = new YFloat(a);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, aFloat.id, aFloat.isFloat, b.id, b.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add);
        return aMinusB;
    }


    public static YVariable operator >(YVariable a, YVariable b)
    {
        YVariable aMinusB = new YFloat();
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, b.id, b.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute);
        return aMinusB == 1;
    }
    public static YVariable operator >(YVariable a, float b)
    {
        YVariable aMinusB = new YFloat();
        YVariable bFloat = new YFloat(b);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, bFloat.id, bFloat.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute);
        return aMinusB == 1;
    }
    public static YVariable operator >(float a, YVariable b)
    {
        YVariable aMinusB = new YFloat();
        YVariable aFloat = new YFloat(a);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, aFloat.id, aFloat.isFloat, b.id, b.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute);
        return aMinusB == 1;
    }



    public static YVariable operator <(YVariable a, YVariable b)
    {
        YVariable aMinusB = new YFloat();
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, b.id, b.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute);
        return aMinusB == -1;
    }
    public static YVariable operator <(YVariable a, float b)
    {
        YVariable aMinusB = new YFloat();
        YVariable bFloat = new YFloat(b);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, bFloat.id, bFloat.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute);
        return aMinusB == -1;
    }
    public static YVariable operator <(float a, YVariable b)
    {
        YVariable aMinusB = new YFloat();
        YVariable aFloat = new YFloat(a);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, aFloat.id, aFloat.isFloat, b.id, b.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute);
        return aMinusB == -1;
    }


    public static YVariable operator >=(YVariable a, YVariable b)
    {
        YVariable aMinusB = new YFloat();
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, b.id, b.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute);
        return aMinusB != -1;
    }
    public static YVariable operator >=(YVariable a, float b)
    {
        YVariable aMinusB = new YFloat();
        YVariable bFloat = new YFloat(b);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, bFloat.id, bFloat.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute);
        return aMinusB != -1;
    }
    public static YVariable operator >=(float a, YVariable b)
    {
        YVariable aMinusB = new YFloat();
        YVariable aFloat = new YFloat(a);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, aFloat.id, aFloat.isFloat, b.id, b.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute);
        return aMinusB != -1;
    }



    public static YVariable operator <=(YVariable a, YVariable b)
    {
        YVariable aMinusB = new YFloat();
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, b.id, b.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute);
        return aMinusB != 1;
    }
    public static YVariable operator <=(YVariable a, float b)
    {
        YVariable aMinusB = new YFloat();
        YVariable bFloat = new YFloat(b);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, bFloat.id, bFloat.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute);
        return aMinusB != 1;
    }
    public static YVariable operator <=(float a, YVariable b)
    {
        YVariable aMinusB = new YFloat();
        YVariable aFloat = new YFloat(a);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Equals, 1, aFloat.id, aFloat.isFloat, b.id, b.isFloat, ItemEdit.Operation.Subtract);
        new ItemEdit(aMinusB.id, aMinusB.isFloat, ItemEdit.Operation.Divide, 1, aMinusB.id, aMinusB.isFloat, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute);
        return aMinusB != 1;
    }

    #endregion
}
