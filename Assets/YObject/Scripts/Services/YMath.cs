using System.Collections;
using System.Collections.Generic;
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

    #endregion
}
