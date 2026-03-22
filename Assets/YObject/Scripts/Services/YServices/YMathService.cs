using System;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class YMathService : YService<YMathService>
{
    #region trigonometry
    public YVariable SinDeg(YVariable x)
    {
        YVariable sin = new YFloat();
        YMath.SinDeg(x, sin);
        return sin;
    }
    public YVariable SinRad(YVariable x)
    {
        YVariable sin = new YFloat();
        YMath.SinRad(x, sin);
        return sin;
    }
    public YVariable CosDeg(YVariable x)
    {
        YVariable cos = new YFloat();
        YMath.CosDeg(x, cos);
        return cos;
    }
    public YVariable CosRad(YVariable x)
    {
        YVariable cos = new YFloat();
        YMath.CosRad(x, cos);
        return cos;
    }
    public YVariable TanDeg(YVariable x)
    {
        YVariable tan = new YFloat();
        YMath.TanDeg(x, tan);
        return tan;
    }
    public YVariable TanRad(YVariable x)
    {
        YVariable tan = new YFloat();
        YMath.TanRad(x, tan);
        return tan;
    }
    public YVariable CotDeg(YVariable x)
    {
        YVariable cot = new YFloat();
        YMath.CotDeg(x, cot);
        return cot;
    }
    public YVariable CotRad(YVariable x)
    {
        YVariable cot = new YFloat();
        YMath.CotRad(x, cot);
        return cot;
    }
    public YVariable Asin(YVariable x)
    {
        YVariable t = (1 - Sqrt(1 - x * x)) / x;
        return 2 * Atan(t);
    }
    public YVariable Acos(YVariable x)
    {
        return Asin(-x) + Mathf.PI/2;
    }
    public YVariable Atan(YVariable x)
    {
        YVariable sqrt = Sqrt(25 + 80 / 3 * x * x);
        return 8 * x / (3 + sqrt);
    }
    public YVariable Atan2(YVariable y, YVariable x)
    {
        YVariable ret = Atan(y / x);
        new Condition(x >= 0)
        .Else(() =>
        {
            new Condition(x * y <= 0)
            .Then(() =>
            {
                ret.Add(3.141f);
            })
            .Else(() =>
            {
                ret.Subtract(3.141f);
            });
        });
        return ret;
    }
    #endregion


    public YVariable Sqrt(YVariable x)
    {
        YVariable sqrt = new YFloat();
        YMath.Sqrt(x, sqrt);
        return sqrt;
    }
    public YVariable Clamp(YVariable a, YVariable b, YVariable c)
    {
        YVariable ret = new YFloat();
        YMath.Max(a, b, ret);
        YMath.Min(ret, c, ret);
        return ret;
    }
    public YVariable Max(YVariable a, YVariable b)
    {
        YVariable max = new YFloat();
        YMath.Max(a, b, max);
        return max;
    }
    public YVariable Min(YVariable a, YVariable b)
    {
        YVariable min = new YFloat();
        YMath.Min(a, b, min);
        return min;
    }
    public YVariable Sign(YVariable x)
    {
        return x / Abs(x);
    }
    public YVariable Mod(YVariable a, YVariable b)
    {
        return a - (b * Floor(a / b));
    }
    public YVariable Abs(YVariable x)
    {
        YVariable abs = new YFloat();
        new ItemEdit(abs, abs.IsFloat(), ItemEdit.Operation.Equals, 1, x, x.IsFloat(), 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Absolute);
        return abs;
    }
    public YVariable Neg(YVariable x)
    {
        YVariable neg = new YFloat();
        new ItemEdit(neg, neg.IsFloat(), ItemEdit.Operation.Equals, 1, x, x.IsFloat(), 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.None, ItemEdit.Operation3.Negative);
        return neg;
    }
    public YVariable Floor(YVariable x)
    {
        YVariable floor = new YFloat();
        new ItemEdit(floor, floor.IsFloat(), ItemEdit.Operation.Equals, 1, x, x.IsFloat(), 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.Floor, ItemEdit.Operation3.None);
        return floor;
    }
    public YVariable Ceil(YVariable x)
    {
        YVariable ceil = new YFloat();
        new ItemEdit(ceil, ceil.IsFloat(), ItemEdit.Operation.Equals, 1, x, x.IsFloat(), 0, true, ItemEdit.Operation.Add, ItemEdit.Operation2.Ceil, ItemEdit.Operation3.None);
        return ceil;
    }
}
