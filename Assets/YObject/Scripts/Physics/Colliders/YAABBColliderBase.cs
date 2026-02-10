using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YAABBColliderBase : IYColliderBase
{
    public YVector3 Min;
    public YVector3 Max;

    public YAABBColliderBase(YVector3 min, YVector3 max)
    {
        Min = min;
        Max = max;
    }

    public YCollisionData AABBCollision(YAABBColliderBase collider)
    {
        YVariable collision = new YInt(0);
        new Condition((Max.x < collider.Min.x) + (Min.x > collider.Max.x) +
            (Max.y < collider.Min.y) + (Min.y > collider.Max.y) +
            (Max.z < collider.Min.z) + (Min.z > collider.Max.z))
            .Else(() =>
            {
                collision.SetValue(1);
            });


        YVector3 normal = new YVector3(0,0,0);

        return new YCollisionData(collision, normal, collision);
    }
}
