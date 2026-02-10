public class YCollisionData
{
    public YVector3 Normal;
    public YVariable IsCollided;
    public YVariable PenetrationDepth;
    // 0 - no collision
    // 1 - collision

    public YCollisionData(YVariable isCollided, YVector3 normal, YVariable penetrationDepth)
    {
        this.Normal = normal;
        this.IsCollided = isCollided;
        this.PenetrationDepth = penetrationDepth;
    }
}