using UnityEngine;
using UnityEngine.UIElements;

public class IslandJumperPlaneCollider : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(1, 1, 0.01f));
    }

    public YVariable SphereCollision(YVector3 sphereCenter, float sphereRadius)
    {
        YVariable hasCollision = new YInt();

        YVector3 right = new YVector3(transform.right);
        YVector3 up = new YVector3(transform.up);
        YVector3 normal = new YVector3(transform.forward);
        YVector3 center = new YVector3(transform.position);


        YVector3 toCenter = sphereCenter - center;
        YVariable distanceToPlane = YVector3.Dot(toCenter, normal);

        new Condition(YMathService.Get().Abs(distanceToPlane) > sphereRadius)
        .Then(() =>
        {
            hasCollision.SetValue(0);
        });

        var localX = YVector3.Dot(toCenter, right);
        var localY = YVector3.Dot(toCenter, up);
        var localZ = YVector3.Dot(toCenter, normal);

        var closestX = YMathService.Get().Clamp(localX, new YFloat(-transform.lossyScale.x / 2), new YFloat(transform.lossyScale.x / 2));
        var closestY = YMathService.Get().Clamp(localY, new YFloat(-transform.lossyScale.y / 2), new YFloat(transform.lossyScale.y / 2));

        var deltaX = localX - closestX;
        var deltaY = localY - closestY;
        var deltaZ = localZ;

        var distanceSqr = deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ;
        var distance = YMathService.Get().Sqrt(distanceSqr);

        new Condition(distance <= sphereRadius)
        .Then(() =>
        {
            hasCollision.SetValue(1);
        })
        .Else(() =>
        {
            hasCollision.SetValue(0);
        });

        return hasCollision;
    }
}
