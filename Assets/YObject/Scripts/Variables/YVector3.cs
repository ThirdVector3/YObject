using System.Security.Cryptography;

public class YVector3 : YVector2
{
    public YVariable z;
    public YVector3(float x, float y, float z) : base(x,y)
    {
        z = new YFloat(z);
    }
    public YVector3(int xId, int yId, int zId) : base(xId, yId)
    {
        z = new YVariable(zId, true);
    }

    public static YVector3 operator +(YVector3 a, YVector3 b)
    {
        a.x += b.x;
        a.y += b.y;
        a.z += b.z;
        return a;
    }
    public static YVector3 operator -(YVector3 a, YVector3 b)
    {
        a.x -= b.x;
        a.y -= b.y;
        a.z -= b.z;
        return a;
    }
    public static YVector3 operator *(YVector3 a, YVector3 b)
    {
        a.x *= b.x;
        a.y *= b.y;
        a.z *= b.z;
        return a;
    }
    public static YVector3 operator *(YVector3 a, float b)
    {
        a.x *= b;
        a.y *= b;
        a.z *= b;
        return a;
    }
    public static YVector3 operator *(float a, YVector3 b)
    {
        b.x *= a;
        b.y *= a;
        b.z *= a;
        return b;
    }
    public static YVector3 operator /(YVector3 a, YVector3 b)
    {
        a.x /= b.x;
        a.y /= b.y;
        a.z /= b.z;
        return a;
    }
    public static YVector3 operator /(YVector3 a, float b)
    {
        a.x /= b;
        a.y /= b;
        a.z /= b;
        return a;
    }
    public static YVector3 operator /(float a, YVector3 b)
    {
        b.x = a / b.x;
        b.y = a / b.y;
        b.z = a / b.z;
        return b;
    }
}