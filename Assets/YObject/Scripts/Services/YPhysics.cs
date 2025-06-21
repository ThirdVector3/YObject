using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public static class YPhysics
{
    public static YVariable PointAABBCollision(YVector3 point, YVector3 minAABB, YVector3 maxAABB)
    {
        YVariable x = (point.x >= minAABB.x) * (point.x <= maxAABB.x);
        YVariable y = (point.y >= minAABB.y) * (point.y <= maxAABB.y);
        YVariable z = (point.z >= minAABB.z) * (point.z <= maxAABB.z);

        return x * y * z;
    }

    public static YVariable AABBAABBCollision(YVector3 minAABB1, YVector3 maxAABB1, YVector3 minAABB2, YVector3 maxAABB2)
    {
        YVariable x = (minAABB1.x <= maxAABB2.x) * (maxAABB1.x >= minAABB2.x);
        YVariable y = (minAABB1.y <= maxAABB2.y) * (maxAABB1.y >= minAABB2.y);
        YVariable z = (minAABB1.z <= maxAABB2.z) * (maxAABB1.z >= minAABB2.z);

        return x * y * z;
    }

    public static YVariable PointSphereCollision(YVector3 point, YVector3 sphere, YVariable radius)
    {
        YVariable distanceX = (new YFloat(1) * point.x - sphere.x);
        YVariable distanceY = (new YFloat(1) * point.y - sphere.y);
        YVariable distanceZ = (new YFloat(1) * point.z - sphere.z);

        distanceX *= distanceX;
        distanceY *= distanceY;
        distanceZ *= distanceZ;

        YVariable distance = distanceX + distanceY + distanceZ;
        YVariable sphereRadiusSquared = new YFloat(1) * radius * radius;

        return distance < sphereRadiusSquared;
    }

    public static YVariable SphereSphereCollision(YVector3 sphere1, YVariable radius1, YVector3 sphere2, YVariable radius2)
    {
        YVariable distanceX = (new YFloat(1) * sphere1.x - sphere2.x);
        YVariable distanceY = (new YFloat(1) * sphere1.y - sphere2.y);
        YVariable distanceZ = (new YFloat(1) * sphere1.z - sphere2.z);

        distanceX *= distanceX;
        distanceY *= distanceY;
        distanceZ *= distanceZ;

        YVariable distance = distanceX + distanceY + distanceZ;
        YVariable sphereRadiusSquared = new YFloat(1) * radius1 + radius2;
        sphereRadiusSquared *= sphereRadiusSquared;

        return distance < sphereRadiusSquared;
    }
}