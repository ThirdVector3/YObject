
using UnityEngine;

public class YQuaternion
{
    public YVariable x;
    public YVariable y;
    public YVariable z;
    public YVariable w;
    public YQuaternion(float x, float y, float z, float w)
    {
        this.x = new YFloat(x);
        this.y = new YFloat(y);
        this.z = new YFloat(z);
        this.w = new YFloat(w);
    }
    public YQuaternion(float x, float y, float z, float w, bool temporary)
    {
        this.x = new YFloat(x, temporary);
        this.y = new YFloat(y, temporary);
        this.z = new YFloat(z, temporary);
        this.w = new YFloat(w, temporary);
    }
    public YQuaternion(YVariable x, YVariable y, YVariable z, YVariable w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }
    public YQuaternion(Quaternion quaternion)
    {
        this.x = new YFloat(quaternion.x);
        this.y = new YFloat(quaternion.y);
        this.z = new YFloat(quaternion.z);
        this.w = new YFloat(quaternion.w);
    }

    public static YQuaternion Identity()
    {
        return new YQuaternion(new YFloat(0), new YFloat(0), new YFloat(0), new YFloat(1));
    }
    public void SetValue(YQuaternion value)
    {
        x.SetValue(value.x);
        y.SetValue(value.y);
        z.SetValue(value.z);
        w.SetValue(value.w);
    }
    public void SetValue(YVector3 value)
    {
        x.SetValue(value.x);
        y.SetValue(value.y);
        z.SetValue(value.z);
    }
    public void Multiply(YVector3 value)
    {
        SetValue(this * value);
    }
    public void Multiply(YQuaternion value)
    {
        SetValue(this * value);
    }
    public YVariable Length()
    {
        return YMathService.Get().Sqrt(x * x + y * y + z * z + w * w);
    }
    public void Normalize()
    {
        var length = Length();
        x.Divide(length);
        y.Divide(length);
        z.Divide(length);
        w.Divide(length);
    }
    public YQuaternion Normalized()
    {
        Normalize();
        return this;
    }
    public static YQuaternion Euler(YVector3 value)
    {
        var roll = value.z / 2;
        var pitch = value.x / 2;
        var yaw = value.y / 2;

        var cz = YMathService.Get().CosDeg(roll);
        var sz = YMathService.Get().SinDeg(roll);
        var cx = YMathService.Get().CosDeg(pitch);
        var sx = YMathService.Get().SinDeg(pitch);
        var cy = YMathService.Get().CosDeg(yaw);
        var sy = YMathService.Get().SinDeg(yaw);

        YQuaternion q = new YQuaternion(new YFloat(), new YFloat(), new YFloat(), new YFloat());
        q.w = cx * cy * cz + sx * sy * sz;
        q.x = sx * cy * cz - cx * sy * sz;
        q.y = cx * sy * cz + sx * cy * sz;
        q.z = cx * cy * sz - sx * sy * cz;

        return q;
    }
    public YVector3 ToEulerAngles()
    {
        //Normalize();


        YVariable xRet = new YFloat(0), yRet = new YFloat(0), zRet = new YFloat(0);


        xRet.SetValue(YMathService.Get().Asin(2.0f * (w * x - y * z)) * Mathf.Rad2Deg);
        yRet.SetValue(YMathService.Get().Atan2(2.0f * (w * y + z * x),
                        1.0f - 2.0f * (x * x + y * y)) * Mathf.Rad2Deg);
        zRet.SetValue(YMathService.Get().Atan2(2.0f * (w * z + x * y),
                        1.0f - 2.0f * (y * y + z * z)) * Mathf.Rad2Deg);

        return new YVector3(xRet, yRet, zRet);
    }
    public YQuaternion Conjugate()
    {
        return new YQuaternion(-x, -y, -z, w + 0);
    }
    public static YVariable Dot(YQuaternion q1, YQuaternion q2)
    {
        return (q1.x * q2.x) + (q1.y * q2.y) + (q1.z * q2.z) + (q1.w * q2.w);
    }
    public static YQuaternion Slerp(YQuaternion q0, YQuaternion q1, float t)
    {
        q1 = new YQuaternion(q1.x+0, q1.y+0, q1.z+0, q1.w+0);
        var dot = Dot(q0, q1);

        new Condition(dot < 0.0f)
        .Then(() =>
        {
            q1.SetValue(new YQuaternion(-q1.x, -q1.y, -q1.z, -q1.w));
            dot.SetValue(-dot);
        });

        var ret = Identity();
        new Condition(dot > 0.9995f)
        .Then(() =>
        {
            ret.SetValue(q0 + t * (q1 - q0));
            ret.Normalize();
        })
        .Else(() => { 
            var theta = YMathService.Get().Acos(dot);
            ret.SetValue( (YMathService.Get().SinRad((1.0f - t) * theta) * q0 + YMathService.Get().SinRad(t * theta) * q1) / YMathService.Get().SinRad(theta));
        });
        return ret;
    }
    public static YQuaternion Lerp(YQuaternion a, YQuaternion b, YVariable t)
    {
        YQuaternion c = new YQuaternion(0,0,0,0);
        c.x = a.x - t * (a.x - b.x);
        c.y = a.y - t * (a.y - b.y);
        c.z = a.z - t * (a.z - b.z);
        c.w = a.w - t * (a.w - b.w);
        return c;
    }
    public static YQuaternion operator *(YQuaternion a, YQuaternion b)
    {
        return new YQuaternion(
            a.w * b.x + a.x * b.w + a.y * b.z - a.z * b.y,
            a.w * b.y - a.x * b.z + a.y * b.w + a.z * b.x,
            a.w * b.z + a.x * b.y - a.y * b.x + a.z * b.w,
            a.w * b.w - a.x * b.x - a.y * b.y - a.z * b.z
        );
    }
    public static YVector3 operator *(YQuaternion rotation, YVector3 point)
    {
        var num = rotation.x * 2f;
        var num2 = rotation.y * 2f;
        var num3 = rotation.z * 2f;
        var num4 = rotation.x * num;
        var num5 = rotation.y * num2;
        var num6 = rotation.z * num3;
        var num7 = rotation.x * num2;
        var num8 = rotation.x * num3;
        var num9 = rotation.y * num3;
        var num10 = rotation.w * num;
        var num11 = rotation.w * num2;
        var num12 = rotation.w * num3;
        YVector3 result = new YVector3(new YFloat(), new YFloat(), new YFloat());
        result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
        result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
        result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
        return result;
    }

    public static YQuaternion operator +(YQuaternion a, YQuaternion b)
    {
        return new YQuaternion(
            a.x + b.x,
            a.y + b.y,
            a.z + b.z,
            a.w + b.w
        );
    }
    public static YQuaternion operator -(YQuaternion a, YQuaternion b)
    {
        return new YQuaternion(
            a.x - b.x,
            a.y - b.y,
            a.z - b.z,
            a.w - b.w
        );
    }
    public static YQuaternion operator *(YQuaternion a, float b)
    {
        return new YQuaternion(
            a.x * b,
            a.y * b,
            a.z * b,
            a.w * b
        );
    }
    public static YQuaternion operator *(YQuaternion a, YVariable b)
    {
        return new YQuaternion(
            a.x * b,
            a.y * b,
            a.z * b,
            a.w * b
        );
    }
    public static YQuaternion operator *(float b, YQuaternion a)
    {
        return new YQuaternion(
            a.x * b,
            a.y * b,
            a.z * b,
            a.w * b
        );
    }
    public static YQuaternion operator *(YVariable b, YQuaternion a)
    {
        return new YQuaternion(
            a.x * b,
            a.y * b,
            a.z * b,
            a.w * b
        );
    }
    public static YQuaternion operator /(YQuaternion a, float b)
    {
        return new YQuaternion(
            a.x / b,
            a.y / b,
            a.z / b,
            a.w / b
        );
    }
    public static YQuaternion operator /(YQuaternion a, YVariable b)
    {
        return new YQuaternion(
            a.x / b,
            a.y / b,
            a.z / b,
            a.w / b
        );
    }
}