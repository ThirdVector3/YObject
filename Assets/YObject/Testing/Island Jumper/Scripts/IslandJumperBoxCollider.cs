using UnityEngine;
using UnityEngine.UIElements;

public class IslandJumperBoxCollider : MonoBehaviour
{
    [field: SerializeField] public bool IsDeadly {  get; private set; }
    [field: SerializeField] public bool IsFinish {  get; private set; }
    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(1, 1, 1));
    }

    private YVector3 GetPosition()
    {
        if (TryGetComponent<YTransform>(out YTransform yTransform))
        {
            return yTransform.GetPosition();
        }
        return new YVector3(transform.position);
    }

    private YQuaternion GetRotation()
    {
        if (TryGetComponent<YTransform>(out YTransform yTransform))
        {
            return yTransform.GetRotation();
        }
        return new YQuaternion(transform.rotation);
    }

    private YVector3 GetScale()
    {
        if (TryGetComponent<YTransform>(out YTransform yTransform))
        {
            return yTransform.GetScale();
        }
        return new YVector3(transform.lossyScale);
    }

    public IslandJumperCollisionData SphereCollision(YVector3 sphereCenter, float sphereRadius)
    {
        IslandJumperCollisionData collisionData = new IslandJumperCollisionData();

        YVariable hasCollision = new YInt();
        YVector3 penetration = new YVector3();
        YVector3 normal = new YVector3();

        YVector3 center = GetPosition();

        YQuaternion rotation = GetRotation();

        YVector3 localCenter = sphereCenter - center;
        YVector3 localSphere = rotation.Conjugate() * localCenter;

        var scale = GetScale();

        YVector3 localClosest = new YVector3(
        YMathService.Get().Clamp(localSphere.x, -scale.x / 2, scale.x / 2),
        YMathService.Get().Clamp(localSphere.y, -scale.y / 2, scale.y / 2),
        YMathService.Get().Clamp(localSphere.z, -scale.z / 2, scale.z / 2)
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

            var distance = YMathService.Get().Sqrt(distSqr);

            var localNormal = localDelta / distance;
            normal = rotation * localNormal;

            penetration.SetValue((sphereRadius - distance) * normal);
        });

        collisionData.HasCollision = hasCollision;
        collisionData.Penetration = penetration;
        collisionData.Deadly = IsDeadly;
        collisionData.Finish = IsFinish;
        collisionData.Normal = normal;

        return collisionData;
    }
}
