using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera3Dash : YMonoBehaviour
{
    [SerializeField] private Vector3 offset = Vector3.zero;


    private Player3Dash player;

    public override void Init()
    {
        player = FindObjectOfType<Player3Dash>();
    }
    public override void Tick()
    {
        YVector3 playerPosition = new YVector3(0f, 0f, 0f);
        YVector3 cameraPosition = new YVector3(0f, 0f, 0f);
        player.GetYTransform().GetPosition(playerPosition.x, playerPosition.y, playerPosition.z);
        YMainCamera.Instance.GetPosition(cameraPosition.x, cameraPosition.y, cameraPosition.z);
        cameraPosition -= new YVector3(offset.x, offset.y, offset.z);

        YVector3 newPosition = new YVector3(offset.x, offset.y, offset.z) + LerpPosition(cameraPosition, playerPosition, 0.1f);
        YMainCamera.Instance.SetPosition(newPosition.x, newPosition.y, newPosition.z);
    }

    private YVector3 LerpPosition(YVector3 a, YVector3 b, float t)
    {
        return new YVector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
    }
}
