using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFlyCamera : YMonoBehaviour
{
    public override void Tick()
    {
        //YInput.GetP1Left(YMainCamera.Instance.Rotate(0, 2f, 0), new YTrigger[0]);
        //YInput.GetP1Right(YMainCamera.Instance.Rotate(0, -2f, 0), new YTrigger[0]);
        //YInput.GetP1Up(YMainCamera.Instance.TranslateLocal(0, 0, 0.1f), new YTrigger[0]);


        new Condition(YInputService.Get().P1Left())
            .Then(() =>
            {
                YMainCamera.Instance.Rotate(0, 2f, 0);
            });
        new Condition(YInputService.Get().P1Right())
            .Then(() =>
            {
                YMainCamera.Instance.Rotate(0, -2f, 0);
            });
        new Condition(YInputService.Get().P1Up())
            .Then(() =>
            {
                YMainCamera.Instance.TranslateLocal(0, 0, 0.1f);
            });
    }
}
