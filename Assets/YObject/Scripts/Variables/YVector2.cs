public class YVector2
{
    public YVariable x;
    public YVariable y;
    public YVector2(float x, float y)
    {
        x = new YFloat(x);
        y = new YFloat(y);
    }
    public YVector2(int xId, int yId)
    {
        x = new YVariable(xId, true);
        y = new YVariable(yId, true);
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
    public static YVector2 operator *(float a, YVector2 b)
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
    public static YVector2 operator /(float a, YVector2 b)
    {
        b.x = a / b.x;
        b.y = a / b.y;
        return b;
    }
}
