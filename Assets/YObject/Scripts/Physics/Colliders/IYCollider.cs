public interface IYCollider
{
    public IYColliderBase yColliderBase { get; set; }
    public YCollisionData Collide(IYColliderBase collider);
}
