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
    #endregion


    public YVariable Sqrt(YVariable x)
    {
        YVariable sqrt = new YFloat();
        YMath.Sqrt(x, sqrt);
        return sqrt;
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
}
