﻿using System.Collections.Generic;

public class YTmpFloat : YTmpVariable
{
    public YTmpFloat(float value)
    {
        id = GetNewId();
        triggers = new List<YTrigger>()
        {
            new ItemEdit(id, true, ItemEdit.Operation.Equals, value)
        };
    }
    public YTmpFloat(int id)
    {
        this.id = id;
        triggers = new List<YTrigger>();
    }

    public static YTmpFloat operator +(YTmpFloat a, YTmpFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Add, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list;
        return a;
    }
    public static YTmpFloat operator +(YTmpFloat a, float b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Add, b));
        a.triggers = list;
        return a;
    }
    public static YTmpFloat operator +(float a, YTmpFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(b.id, true, ItemEdit.Operation.Add, a));
        b.triggers = list;
        return b;
    }
    public static YTmpFloat operator +(YTmpFloat a, YTmpInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Add, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        a.triggers = list;
        return a;
    }
    public static YTmpInt operator +(YTmpInt a, YTmpFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Add, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list;
        return a;
    }




    public static YTmpFloat operator -(YTmpFloat a, YTmpFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Subtract, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list;
        return a;
    }
    public static YTmpFloat operator -(YTmpFloat a, float b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Subtract, b));
        a.triggers = list;
        return a;
    }
    public static YTmpFloat operator -(float a, YTmpFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        YTmpFloat aF = new YTmpFloat(a);
        list.AddRange(aF.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(aF.id, true, ItemEdit.Operation.Subtract, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        aF.triggers = list;
        return aF;
    }
    public static YTmpFloat operator -(YTmpFloat a, YTmpInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Subtract, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        a.triggers = list;
        return a;
    }
    public static YTmpInt operator -(YTmpInt a, YTmpFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Subtract, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list;
        return a;
    }





    public static YTmpFloat operator *(YTmpFloat a, YTmpFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Multiply, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list;
        return a;
    }
    public static YTmpFloat operator *(YTmpFloat a, float b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Multiply, b));
        a.triggers = list;
        return a;
    }
    public static YTmpFloat operator *(float a, YTmpFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(b.id, true, ItemEdit.Operation.Multiply, a));
        b.triggers = list;
        return b;
    }
    public static YTmpFloat operator *(YTmpFloat a, YTmpInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Multiply, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        a.triggers = list;
        return a;
    }
    public static YTmpInt operator *(YTmpInt a, YTmpFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Multiply, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list;
        return a;
    }





    public static YTmpFloat operator /(YTmpFloat a, YTmpFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Divide, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list;
        return a;
    }
    public static YTmpFloat operator /(YTmpFloat a, float b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Divide, b));
        a.triggers = list;
        return a;
    }
    public static YTmpFloat operator /(float a, YTmpFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        YTmpFloat aF = new YTmpFloat(a);
        list.AddRange(aF.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(aF.id, true, ItemEdit.Operation.Divide, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        aF.triggers = list;
        return aF;
    }
    public static YTmpFloat operator /(YTmpFloat a, YTmpInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Divide, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        a.triggers = list;
        return a;
    }
    public static YTmpInt operator /(YTmpInt a, YTmpFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Divide, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list;
        return a;
    }
}
