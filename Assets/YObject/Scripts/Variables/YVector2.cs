
using UnityEngine;

public class YVector2
{
    public YVariable x;
    public YVariable y;
    public YVector2()
    {
        this.x = new YFloat();
        this.y = new YFloat();
    }
    public YVector2(float x, float y)
    {
        this.x = new YFloat(x);
        this.y = new YFloat(y);
    }
    public YVector2(float x, float y, bool temporary)
    {
        this.x = new YFloat(x, temporary);
        this.y = new YFloat(y, temporary);
    }
    public YVector2(YVariable x, YVariable y)
    {
        this.x = x;
        this.y = y;
    }
    public YVector2(Vector2 vector2)
    {
        this.x = new YFloat(vector2.x);
        this.y = new YFloat(vector2.y);
    }

    public void SetValue(YVector2 value)
    {
        x.SetValue(value.x);
        y.SetValue(value.y);
    }
    public void Add(YVector2 value)
    {
        x.Add(value.x);
        y.Add(value.y);
    }
    public void Subtract(YVector2 value)
    {
        x.Subtract(value.x);
        y.Subtract(value.y);
    }
    public void Multiply(YVector2 value)
    {
        x.Multiply(value.x);
        y.Multiply(value.y);
    }
    public void Divide(YVector2 value)
    {
        x.Divide(value.x);
        y.Divide(value.y);
    }
    public virtual YVariable Length()
    {
        return YMathService.Get().Sqrt(x*x + y*y);
    }
    public virtual void Normalize()
    {
        var length = Length();
        x.Divide(length);
        y.Divide(length);
    }
    public static YVector2 Lerp(YVector2 a, YVector2 b, YVariable t)
    {
        YVector2 result = new YVector2();
        result.x = a.x + (b.x - a.x) * t;
        result.y = a.y + (b.y - a.y) * t;
        return result;
    }
    public static YVariable Dot(YVector2 a, YVector2 b)
    {
        return a.x * b.x + a.y * b.y;
    }

    public static YVector2 operator +(YVector2 a, YVector2 b)
    {
        a.x += b.x;
        a.y += b.y;
        return a;
    }
    public static YVector2 operator -(YVector2 a, YVector2 b)
    {
        a.x -= b.x;
        a.y -= b.y;
        return a;
    }
    public static YVector2 operator -(YVector2 a)
    {
        a.x *= -1;
        a.y *= -1;
        return a;
    }
    public static YVector2 operator *(YVector2 a, YVector2 b)
    {
        a.x *= b.x;
        a.y *= b.y;
        return a;
    }
    public static YVector2 operator *(YVector2 a, float b)
    {
        a.x *= b;
        a.y *= b;
        return a;
    }
    public static YVector2 operator *(YVector2 a, YVariable b)
    {
        a.x *= b;
        a.y *= b;
        return a;
    }
    public static YVector2 operator *(float a, YVector2 b)
    {
        b.x *= a;
        b.y *= a;
        return b;
    }
    public static YVector2 operator *(YVariable a, YVector2 b)
    {
        b.x *= a;
        b.y *= a;
        return b;
    }
    public static YVector2 operator /(YVector2 a, YVector2 b)
    {
        a.x /= b.x;
        a.y /= b.y;
        return a;
    }
    public static YVector2 operator /(YVector2 a, float b)
    {
        a.x /= b;
        a.y /= b;
        return a;
    }
    public static YVector2 operator /(YVector2 a, YVariable b)
    {
        a.x /= b;
        a.y /= b;
        return a;
    }
    public static YVector2 operator /(float a, YVector2 b)
    {
        b.x = a / b.x;
        b.y = a / b.y;
        return b;
    }
    public static YVector2 operator /(YVariable a, YVector2 b)
    {
        b.x = a / b.x;
        b.y = a / b.y;
        return b;
    }
}
