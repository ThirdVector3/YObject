using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
public static class YMath
{
    #region trigonometry

    public static YTrigger[] SinDeg(int idIn, int idOut)
    {
        List<YTrigger> triggers = new List<YTrigger>()
        {
            new ItemEdit(9999, true, ItemEdit.Operation.Equals, 3.141f),
            new ItemEdit(9998, true, ItemEdit.Operation.Equals, 180f),
            new ItemEdit(9994, true, ItemEdit.Operation.Equals, 1, idIn, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(9994, true, ItemEdit.Operation.Multiply, 1, 9999, true, 9998, true, ItemEdit.Operation.Divide),
        };
        triggers.AddRange(SinRad(9994, idOut));
        return triggers.ToArray();
    }
    public static YTrigger[] SinRad(int idIn, int idOut)
    {
        // sin = u(3.1 + 3.6|u|)
        // u = x / pi * (1 - |x / pi|)

        List<YTrigger> triggersNormalize = new List<YTrigger>()
        {
            new ItemEdit(9995, true, ItemEdit.Operation.Equals, 1, idIn, true, 0, true, ItemEdit.Operation.Add),

            new ItemEdit(9995, true, ItemEdit.Operation.Add, 3.141f),

            new ItemEdit(9999, true, ItemEdit.Operation.Equals, 6.282f),
            new ItemEdit(9998, true, ItemEdit.Operation.Equals, 1, 9995, true, 9999, true, ItemEdit.Operation.Divide, ItemEdit.Operation2.Floor, ItemEdit.Operation3.None),
            new ItemEdit(9995, true, ItemEdit.Operation.Subtract, 1, 9998, true, 9999, true, ItemEdit.Operation.Multiply),

            new ItemEdit(9995, true, ItemEdit.Operation.Subtract, 3.141f),
        };
        idIn = 9995;

        List <YTrigger> triggersProcedure = new List<YTrigger>()
        {
            // pi
            new ItemEdit(9999, true, ItemEdit.Operation.Equals, 3.141f),

            // u = x / pi * (1 - |x / pi|)
            new ItemEdit(9998, true, ItemEdit.Operation.Equals, 1, idIn, true, 9999, true, ItemEdit.Operation.Divide),
            new ItemEdit(9997, true, ItemEdit.Operation.Equals, 1),
            new ItemEdit(9999, true, ItemEdit.Operation.Equals, 1, 9998, true, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute),
            new ItemEdit(9997, true, ItemEdit.Operation.Equals, 1, 9997, true, 9999, true, ItemEdit.Operation.Subtract),
            new ItemEdit(9997, true, ItemEdit.Operation.Equals, 1, 9997, true, 9998, true, ItemEdit.Operation.Multiply),
            new ItemEdit(9996, true, ItemEdit.Operation.Equals, 1, 9997, true, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute),

            // u(3.1 + 3.6|u|)
            new ItemEdit(9998, true, ItemEdit.Operation.Equals, 3.6f),
            new ItemEdit(9998, true, ItemEdit.Operation.Equals, 1, 9998, true, 9996, true, ItemEdit.Operation.Multiply),
            new ItemEdit(9996, true, ItemEdit.Operation.Equals, 3.1f),
            new ItemEdit(9996, true, ItemEdit.Operation.Equals, 1, 9996, true, 9998, true, ItemEdit.Operation.Add),
            new ItemEdit(9996, true, ItemEdit.Operation.Equals, 1, 9996, true, 9997, true, ItemEdit.Operation.Multiply),

            // result
            new ItemEdit(idOut, true, ItemEdit.Operation.Equals, 1, 9996, true, 0, true, ItemEdit.Operation.Add),
        };

        List<YTrigger> triggers = new List<YTrigger>();

        triggers.AddRange(triggersNormalize);
        triggers.AddRange(triggersProcedure);

        return triggers.ToArray();
    }
    public static YTrigger[] CosDeg(int idIn, int idOut)
    {
        List<YTrigger> triggers = new List<YTrigger>()
        {
            new ItemEdit(9999, true, ItemEdit.Operation.Equals, 3.141f),
            new ItemEdit(9998, true, ItemEdit.Operation.Equals, 180f),
            new ItemEdit(9994, true, ItemEdit.Operation.Equals, 1, idIn, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(9994, true, ItemEdit.Operation.Multiply, 1, 9999, true, 9998, true, ItemEdit.Operation.Divide),
        };
        triggers.AddRange(CosRad(9994, idOut));
        return triggers.ToArray();
    }
    public static YTrigger[] CosRad(int idIn, int idOut)
    {
        // sin = u(3.1 + 3.6|u|)
        // u = x / pi * (1 - |x / pi|)

        List<YTrigger> triggersNormalize = new List<YTrigger>()
        {
            new ItemEdit(9995, true, ItemEdit.Operation.Equals, 1, idIn, true, 0, true, ItemEdit.Operation.Add),

            new ItemEdit(9995, true, ItemEdit.Operation.Add, 1.571f),

            new ItemEdit(9995, true, ItemEdit.Operation.Add, 3.141f),

            new ItemEdit(9999, true, ItemEdit.Operation.Equals, 6.282f),
            new ItemEdit(9998, true, ItemEdit.Operation.Equals, 1, 9995, true, 9999, true, ItemEdit.Operation.Divide, ItemEdit.Operation2.Floor, ItemEdit.Operation3.None),
            new ItemEdit(9995, true, ItemEdit.Operation.Subtract, 1, 9998, true, 9999, true, ItemEdit.Operation.Multiply),

            new ItemEdit(9995, true, ItemEdit.Operation.Subtract, 3.141f),
        };
        idIn = 9995;

        List<YTrigger> triggersProcedure = new List<YTrigger>()
        {
            // pi
            new ItemEdit(9999, true, ItemEdit.Operation.Equals, 3.141f),

            // u = x / pi * (1 - |x / pi|)
            new ItemEdit(9998, true, ItemEdit.Operation.Equals, 1, idIn, true, 9999, true, ItemEdit.Operation.Divide),
            new ItemEdit(9997, true, ItemEdit.Operation.Equals, 1),
            new ItemEdit(9999, true, ItemEdit.Operation.Equals, 1, 9998, true, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute),
            new ItemEdit(9997, true, ItemEdit.Operation.Equals, 1, 9997, true, 9999, true, ItemEdit.Operation.Subtract),
            new ItemEdit(9997, true, ItemEdit.Operation.Equals, 1, 9997, true, 9998, true, ItemEdit.Operation.Multiply),
            new ItemEdit(9996, true, ItemEdit.Operation.Equals, 1, 9997, true, 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute),

            // u(3.1 + 3.6|u|)
            new ItemEdit(9998, true, ItemEdit.Operation.Equals, 3.6f),
            new ItemEdit(9998, true, ItemEdit.Operation.Equals, 1, 9998, true, 9996, true, ItemEdit.Operation.Multiply),
            new ItemEdit(9996, true, ItemEdit.Operation.Equals, 3.1f),
            new ItemEdit(9996, true, ItemEdit.Operation.Equals, 1, 9996, true, 9998, true, ItemEdit.Operation.Add),
            new ItemEdit(9996, true, ItemEdit.Operation.Equals, 1, 9996, true, 9997, true, ItemEdit.Operation.Multiply),

            // result
            new ItemEdit(idOut, true, ItemEdit.Operation.Equals, 1, 9996, true, 0, true, ItemEdit.Operation.Add),
        };

        List<YTrigger> triggers = new List<YTrigger>();

        triggers.AddRange(triggersNormalize);
        triggers.AddRange(triggersProcedure);

        return triggers.ToArray();
    }

    public static YTrigger[] TanDeg(int idIn, int idOut)
    {
        List<YTrigger> triggers = new List<YTrigger>();
        triggers.AddRange(SinDeg(idIn, idOut));
        triggers.AddRange(CosDeg(idIn, 9993));
        triggers.Add(new ItemEdit(idOut, true, ItemEdit.Operation.Divide, 1, 9993, true, 0, true, ItemEdit.Operation.Add));
        return triggers.ToArray();
    }
    public static YTrigger[] TanRad(int idIn, int idOut)
    {
        List<YTrigger> triggers = new List<YTrigger>();
        triggers.AddRange(SinRad(idIn, idOut));
        triggers.AddRange(CosRad(idIn, 9993));
        triggers.Add(new ItemEdit(idOut, true, ItemEdit.Operation.Divide, 1, 9993, true, 0, true, ItemEdit.Operation.Add));
        return triggers.ToArray();
    }

    public static YTrigger[] CotDeg(int idIn, int idOut)
    {
        List<YTrigger> triggers = new List<YTrigger>();
        triggers.AddRange(CosDeg(idIn, idOut));
        triggers.AddRange(SinDeg(idIn, 9993));
        triggers.Add(new ItemEdit(idOut, true, ItemEdit.Operation.Divide, 1, 9993, true, 0, true, ItemEdit.Operation.Add));
        return triggers.ToArray();
    }
    public static YTrigger[] CotRad(int idIn, int idOut)
    {
        List<YTrigger> triggers = new List<YTrigger>();
        triggers.AddRange(CosRad(idIn, idOut));
        triggers.AddRange(SinRad(idIn, 9993));
        triggers.Add(new ItemEdit(idOut, true, ItemEdit.Operation.Divide, 1, 9993, true, 0, true, ItemEdit.Operation.Add));
        return triggers.ToArray();
    }

    #endregion


    public static YTrigger[] Sqrt(int idIn, int idOut)
    {
        List<YTrigger> triggers = new List<YTrigger>();

        var trigger1 = new ItemEdit(idOut, true, ItemEdit.Operation.Equals, 1.01f);
        //var trigger10 = new ItemEdit(idOut, true, ItemEdit.Operation.Equals, 10.01f);
        //var trigger100 = new ItemEdit(idOut, true, ItemEdit.Operation.Equals, 100.01f);
        //var trigger1000 = new ItemEdit(idOut, true, ItemEdit.Operation.Equals, 1000.01f);

        //triggers.Add(new ItemCompare(idIn, 0, true, true, 1, 100, ItemCompare.Operation.Less));
        triggers.Add(trigger1);

        for (int i = 0; i < 5; i++)
        {
            triggers.Add(new ItemEdit(idOut, true, ItemEdit.Operation.Add, 1, idIn, true, idOut, true, ItemEdit.Operation.Divide));
            triggers.Add(new ItemEdit(idOut, true, ItemEdit.Operation.Multiply, 0.5f));
        }

        return triggers.ToArray();
    }

    public static YTrigger[] Max(int idIn1, int idIn2, int idOut)
    {
        YGameManager.Instance.RecordPool();

        YVariable isFirstBigger = new YVariable(idIn1, true) > new YVariable(idIn2, true);
        isFirstBigger = new YFloat(1) * isFirstBigger * new YVariable(idIn1, true) + ((1 - new YFloat(1) * isFirstBigger) * new YVariable(idIn2, true));

        new ItemEdit(idOut, true, ItemEdit.Operation.Equals, 1, isFirstBigger, true, 0, true, ItemEdit.Operation.Add);

        return YGameManager.Instance.StopRecordPool();
    }

    public static YTrigger[] Min(int idIn1, int idIn2, int idOut)
    {
        YGameManager.Instance.RecordPool();

        YVariable isFirstLess = new YVariable(idIn1, true) < new YVariable(idIn2, true);
        isFirstLess = new YFloat(1) * isFirstLess * new YVariable(idIn1, true) + ((1 - new YFloat(1) * isFirstLess) * new YVariable(idIn2, true));

        new ItemEdit(idOut, true, ItemEdit.Operation.Equals, 1, isFirstLess, true, 0, true, ItemEdit.Operation.Add);

        return YGameManager.Instance.StopRecordPool();
    }
}
