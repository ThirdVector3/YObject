using System.Collections.Generic;

public class YInt : YVariable
{
    public YInt(int value)
    {
        id = GetNewId();
        triggers = new YTrigger[]
        {
            new ItemEdit(id, true, ItemEdit.Operation.Equals, value)
        };
    }
    public YInt(int value, int id)
    {
        this.id = id;
        triggers = new YTrigger[0];
    }

    public static YInt operator +(YInt a, YInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Add, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }
    public static YInt operator +(YInt a, float b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Add, b));
        a.triggers = list.ToArray();
        return a;
    }
    public static YInt operator +(float a, YInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(b.id, false, ItemEdit.Operation.Add, a));
        b.triggers = list.ToArray();
        return b;
    }





    public static YInt operator -(YInt a, YInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Subtract, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }
    public static YInt operator -(YInt a, float b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Subtract, b));
        a.triggers = list.ToArray();
        return a;
    }
    public static YFloat operator -(float a, YInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        YFloat aF = new YFloat(a);
        list.AddRange(aF.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(aF.id, true, ItemEdit.Operation.Subtract, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        aF.triggers = list.ToArray();
        return aF;
    }





    public static YInt operator *(YInt a, YInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Multiply, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }
    public static YInt operator *(YInt a, float b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Multiply, b));
        a.triggers = list.ToArray();
        return a;
    }
    public static YInt operator *(float a, YInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(b.id, false, ItemEdit.Operation.Multiply, a));
        b.triggers = list.ToArray();
        return b;
    }





    public static YInt operator /(YInt a, YInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Divide, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }
    public static YInt operator /(YInt a, float b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Divide, b));
        a.triggers = list.ToArray();
        return a;
    }
    public static YFloat operator /(float a, YInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        YFloat aF = new YFloat(a);
        list.AddRange(aF.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(aF.id, true, ItemEdit.Operation.Divide, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        aF.triggers = list.ToArray();
        return aF;
    }
}
