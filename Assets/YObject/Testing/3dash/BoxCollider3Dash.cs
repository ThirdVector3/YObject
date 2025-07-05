using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollider3Dash : YMonoBehaviour
{
    private Player3Dash player;
    private static YVector3 touchPoint;
    private static YVariable isColliding;
    public static void ResetCollisionData()
    {
        isColliding.SetValue(0);
    }
    public static YVariable GetCollisionsCount()
    {
        return isColliding;
    }

    public override void Uninit()
    {
        base.Uninit();
        touchPoint = null;
        isColliding = null;
    }

    public override void Init()
    {
        player = FindObjectOfType<Player3Dash>();
        if ((object)touchPoint == null)
        {
            touchPoint = new YVector3(0f, 0, 0);
        }
        if ((object)isColliding == null)
        {
            isColliding = new YInt(0);
        }
    }

    private bool alreadyTicked = false;
    public override void Tick()
    {


        YVector3 playerPosition = new YVector3(0f, 0f, 0f);
        player.GetYTransform().GetPosition(playerPosition.x, playerPosition.y, playerPosition.z);
        YVariable hasCollision = YPhysics.AABBAABBCollision(
            new YVector3(-0.5f, -0.5f, -0.5f) + playerPosition,
            new YVector3(0.5f, 0.5f, 0.5f) + playerPosition,
            new YVector3(transform.position.x - transform.localScale.x / 2, transform.position.y - transform.localScale.y / 2, transform.position.z - transform.localScale.z / 2),
            new YVector3(transform.position.x + transform.localScale.x / 2, transform.position.y + transform.localScale.y / 2, transform.position.z + transform.localScale.z / 2)
            );
        new Condition(hasCollision)
            .Then(() =>
            {
                isColliding += 1;
                //player.GetYTransform().Translate(0, 0.01f, 0);
                //player.IsGrounded = new YInt(1);
            })
            .Else(() =>
            {
                //player.IsGrounded = new YInt(0);
            });
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
