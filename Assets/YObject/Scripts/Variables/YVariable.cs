using System;
using System.Collections.Generic;
using UnityEngine;
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

    public void SetValue(YVariable value)
    {
        new ItemEdit(id, isFloat, ItemEdit.Operation.Equals, 1, value.id, value.isFloat, 0, true, ItemEdit.Operation.Add);
    }
    public void SetValue(float value)
    {
        new ItemEdit(id, isFloat, ItemEdit.Operation.Equals, value);
    }
    public void Add(YVariable value)
    {
        new ItemEdit(id, isFloat, ItemEdit.Operation.Add, 1, value.id, value.isFloat, 0, true, ItemEdit.Operation.Add);
    }
    public void Add(float value)
    {
        new ItemEdit(id, isFloat, ItemEdit.Operation.Add, value);
    }
    public void Subtract(YVariable value)
    {
        new ItemEdit(id, isFloat, ItemEdit.Operation.Subtract, 1, value.id, value.isFloat, 0, true, ItemEdit.Operation.Add);
    }
    public void Subtract(float value)
    {
        new ItemEdit(id, isFloat, ItemEdit.Operation.Subtract, value);
    }
    public void Multiply(YVariable value)
    {
        new ItemEdit(id, isFloat, ItemEdit.Operation.Multiply, 1, value.id, value.isFloat, 0, true, ItemEdit.Operation.Add);
    }
    public void Multiply(float value)
    {
        new ItemEdit(id, isFloat, ItemEdit.Operation.Multiply, value);
    }
    public void Divide(YVariable value)
    {
        new ItemEdit(id, isFloat, ItemEdit.Operation.Divide, 1, value.id, value.isFloat, 0, true, ItemEdit.Operation.Add);
    }
    public void Divide(float value)
    {
        new ItemEdit(id, isFloat, ItemEdit.Operation.Divide, value);
    }


    private static int lastTmpId = 9900;
    private static int GetNewTmpId()
    {
        if (--lastTmpId < 9000)
            lastTmpId = 9900;
        return lastTmpId;
    }

    protected static int GetNewID(bool isFloat, bool temporary = false)
    {
        if (!temporary && YGameManager.Instance.gameobjectsAndServicesInitialization)
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
        YVariable c = new YVariable(GetNewTmpId(), a.isFloat);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Add, 1, b.id, b.isFloat, 0, true, ItemEdit.Operation.Add);
        return c;
    }
    public static YVariable operator +(YVariable a, float b)
    {
        YVariable c = new YVariable(GetNewTmpId(), true);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Add, b);
        return c;
    }
    public static YVariable operator +(float a, YVariable b)
    {
        YVariable c = new YVariable(GetNewTmpId(), true);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Equals, 1, b.id, b.isFloat, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Add, a);
        return c;
    }

    public static YVariable operator *(YVariable a, YVariable b)
    {
        YVariable c = new YVariable(GetNewTmpId(), a.isFloat);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Multiply, 1, b.id, b.isFloat, 0, true, ItemEdit.Operation.Add);
        return c;
    }
    public static YVariable operator *(YVariable a, float b)
    {
        YVariable c = new YVariable(GetNewTmpId(), true);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Multiply, b);
        return c;
    }
    public static YVariable operator *(float a, YVariable b)
    {
        YVariable c = new YVariable(GetNewTmpId(), true);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Equals, 1, b.id, b.isFloat, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Multiply, a);
        return c;
    }

    public static YVariable operator -(YVariable a)
    {
        return 0 - a;
    }
    public static YVariable operator -(YVariable a, YVariable b)
    {
        YVariable c = new YVariable(GetNewTmpId(), a.isFloat);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Subtract, 1, b.id, b.isFloat, 0, true, ItemEdit.Operation.Add);
        return c;
    }
    public static YVariable operator -(YVariable a, float b)
    {
        YVariable c = new YVariable(GetNewTmpId(), true);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Subtract, b);
        return c;
    }
    public static YVariable operator -(float a, YVariable b)
    {
        YFloat yFloat = new YFloat(a);
        new ItemEdit(yFloat.id, yFloat.isFloat, ItemEdit.Operation.Subtract, 1, b.id, b.isFloat, 0, true, ItemEdit.Operation.Add);
        return yFloat;
    }

    public static YVariable operator /(YVariable a, YVariable b)
    {
        YVariable c = new YVariable(GetNewTmpId(), a.isFloat);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Divide, 1, b.id, b.isFloat, 0, true, ItemEdit.Operation.Add);
        return c;
    }
    public static YVariable operator /(YVariable a, float b)
    {
        YVariable c = new YVariable(GetNewTmpId(), true);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Equals, 1, a.id, a.isFloat, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(c.id, c.isFloat, ItemEdit.Operation.Divide, b);
        return c;
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
