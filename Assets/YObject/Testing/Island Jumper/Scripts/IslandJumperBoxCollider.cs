using UnityEngine;

public class IslandJumperBoxCollider : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(1, 1, 1));
    }

    public YVariable SphereCollision(YVector3 sphereCenter, float sphereRadius)
    {
        YVariable hasCollision = new YInt();

        YVector3 center = new YVector3(transform.position);

        YVector3 localCenter = sphereCenter - center;
        YVector3 localSphere = new YQuaternion(Quaternion.Inverse(transform.rotation)) * localCenter;

        YVector3 localClosest = new YVector3(
        YMathService.Get().Clamp(localSphere.x, new YFloat(-transform.lossyScale.x / 2), new YFloat(transform.lossyScale.x / 2)),
        YMathService.Get().Clamp(localSphere.y, new YFloat(-transform.lossyScale.y / 2), new YFloat(transform.lossyScale.y / 2)),
        YMathService.Get().Clamp(localSphere.z, new YFloat(-transform.lossyScale.z / 2), new YFloat(transform.lossyScale.z / 2))
        );

        YVector3 localDelta = localSphere - localClosest;
        var distSqr = localDelta.x * localDelta.x + localDelta.y * localDelta.y + localDelta.z * localDelta.z;

        new Condition(distSqr > sphereRadius * sphereRadius)
        .Then(() =>
        {
            hasCollision.SetValue(0);
        })
        .Else(() =>
        {
            hasCollision.SetValue(1);
        });

        return hasCollision;
    }
}