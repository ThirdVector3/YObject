using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Camera3Dash : YMonoBehaviour
{
    [SerializeField] private Vector3 offset = Vector3.zero;


    private Player3Dash player;

    public YTransform[] a;


    public override void Init()
    {
        player = FindObjectOfType<Player3Dash>();
    }
    public override void Begin()
    {
        YGameService.Get().SetFPS(60);
        //YQuaternion q = YQuaternion.Euler(new YVector3(90, 0, 0f));
        //
        //var e = q.ToSinCos();
        //
        //new DebugLog(() => YIDsManager.Instance.GetMemoryValue(e.Item1.z).Item2);
        //new DebugLog(() => YIDsManager.Instance.GetMemoryValue(e.Item2.z).Item2);
        //
        //new DebugLog(() => Mathf.Cos(0 * Mathf.Deg2Rad));
        //new DebugLog(() => Mathf.Sin(0 * Mathf.Deg2Rad));
    }
    public override void Tick()
    {
        foreach (YTransform t in a)
        {
            //t.Rotate(1.78f, 3.42f, 5.4f);
            //t.Rotate(1.78f, 0, 0);
            //t.SetRotation(10, 20, 30f);

            t.TranslateLocal(new YVector3(YMathService.Get().SinRad(new YVariable("Time.time"))*0.04f, new YFloat(0), new YFloat(0)));
        }

        //new Condition(YInputService.Get().P1UpDown())
        //    .Then(() =>
        //    {
        //        foreach (YTransform t in a)
        //        {
        //            t.SetPosition(new YVector3(0,0,1));
        //        }
        //    });

        return;
        YVector3 playerPosition = new YVector3(0f, 0f, 0f);
        YVector3 cameraPosition = new YVector3(0f, 0f, 0f);
        playerPosition.SetValue(player.GetYTransform().GetPosition());
        //YMainCamera.Instance.GetPosition(cameraPosition.x, cameraPosition.y, cameraPosition.z);
        cameraPosition.Subtract(new YVector3(offset.x, offset.y, offset.z));

        YVector3 newPosition = new YVector3(offset.x, offset.y, offset.z) + LerpPosition(cameraPosition, playerPosition, 0.1f);
        YMainCamera.Instance.yTransform.SetPosition(newPosition.x, newPosition.y, newPosition.z);
    }

    private YVector3 LerpPosition(YVector3 a, YVector3 b, float t)
    {
        return new YVector3(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t);
    }
}
