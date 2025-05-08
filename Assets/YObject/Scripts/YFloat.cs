using System.Collections.Generic;

public class YFloat : YVariable
{
    public YFloat(float value)
    {
        id = GetNewId();
        triggers = new YTrigger[]
        {
            new ItemEdit(id, true, ItemEdit.Operation.Equals, value)
        };
    }
    public YFloat(int id)
    {
        this.id = id;
        triggers = new YTrigger[0];
    }

    public static YFloat operator +(YFloat a, YFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Add, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }
    public static YFloat operator +(YFloat a, float b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Add, b));
        a.triggers = list.ToArray();
        return a;
    }
    public static YFloat operator +(float a, YFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(b.id, true, ItemEdit.Operation.Add, a));
        b.triggers = list.ToArray();
        return b;
    }
    public static YFloat operator +(YFloat a, YInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Add, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }
    public static YInt operator +(YInt a, YFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Add, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }




    public static YFloat operator -(YFloat a, YFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Subtract, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }
    public static YFloat operator -(YFloat a, float b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Subtract, b));
        a.triggers = list.ToArray();
        return a;
    }
    public static YFloat operator -(float a, YFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        YFloat aF = new YFloat(a);
        list.AddRange(aF.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(aF.id, true, ItemEdit.Operation.Subtract, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        aF.triggers = list.ToArray();
        return aF;
    }
    public static YFloat operator -(YFloat a, YInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Subtract, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }
    public static YInt operator -(YInt a, YFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Subtract, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }





    public static YFloat operator *(YFloat a, YFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Multiply, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }
    public static YFloat operator *(YFloat a, float b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Multiply, b));
        a.triggers = list.ToArray();
        return a;
    }
    public static YFloat operator *(float a, YFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(b.id, true, ItemEdit.Operation.Multiply, a));
        b.triggers = list.ToArray();
        return b;
    }
    public static YFloat operator *(YFloat a, YInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Multiply, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }
    public static YInt operator *(YInt a, YFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Multiply, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }





    public static YFloat operator /(YFloat a, YFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Divide, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }
    public static YFloat operator /(YFloat a, float b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Divide, b));
        a.triggers = list.ToArray();
        return a;
    }
    public static YFloat operator /(float a, YFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        YFloat aF = new YFloat(a);
        list.AddRange(aF.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(aF.id, true, ItemEdit.Operation.Divide, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        aF.triggers = list.ToArray();
        return aF;
    }
    public static YFloat operator /(YFloat a, YInt b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, true, ItemEdit.Operation.Divide, 1, b.id, false, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }
    public static YInt operator /(YInt a, YFloat b)
    {
        List<YTrigger> list = new List<YTrigger>();
        list.AddRange(a.triggers);
        list.AddRange(b.triggers);
        list.Add(new ItemEdit(a.id, false, ItemEdit.Operation.Divide, 1, b.id, true, 0, true, ItemEdit.Operation.Add));
        a.triggers = list.ToArray();
        return a;
    }
}
