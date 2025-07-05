using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh3Dash : YMonoBehaviour
{
    private Player3Dash player;
    private YTransform yTransform;

    public override void Init()
    {
        player = FindObjectOfType<Player3Dash>();
        yTransform = GetComponent<YTransform>();
    }
    public override void Tick()
    {
        YVector3 playerPosition = new YVector3(0f, 0f, 0f);
        player.GetYTransform().GetPosition(playerPosition.x, playerPosition.y, playerPosition.z);
        yTransform.SetPosition(playerPosition.x, playerPosition.y, playerPosition.z);

        new Condition(player.IsGrounded)
            .Else(() =>
            {
                yTransform.Rotate(new YFloat(360) * new YVariable("Time.deltaTime"), 0, 0);
            });
    }
}
