using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class YAABBCollider : YMonoBehaviour, IYCollider
{
    public IYColliderBase yColliderBase { get; set; }

    public YCollisionData Collide(IYColliderBase collider)
    {
        if (collider is YAABBColliderBase)
            return collider.AABBCollision(yColliderBase as YAABBColliderBase);

        return null;
    }

    public override void Init()
    {
        var posMin = transform.position - transform.localScale / 2;
        var posMax = transform.position + transform.localScale / 2;
        yColliderBase = new YAABBColliderBase(new YVector3(posMin.x, posMin.y, posMin.z), new YVector3(posMax.x, posMax.y, posMax.z));
    }

    public override void Tick()
    {
        foreach (IYCollider collider in FindObjectsOfType<YMonoBehaviour>().OfType<IYCollider>())
        {
            if (collider == this)
                continue;
            //new DebugLog(Collide(collider.yColliderBase).Normal.y);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
