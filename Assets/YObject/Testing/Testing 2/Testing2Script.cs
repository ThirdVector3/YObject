using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing2Script : YMonoBehaviour
{
    private YVariable cameraRotationSpeed;
    public override void Begin()
    {
        YCoroutines.StartCoroutine(yCoroutine);

        new ColorTrigger(3, 1, Color.black);

        new SongTrigger(467339, 0, 1, false, 0, 0, 0, 0);
    }
    private Coroutine yCoroutine;
    public override void Init()
    {
        // creating variable
        cameraRotationSpeed = new YFloat(2.5f);

        GetComponent<YTransform>().Init();

        // creating coroutine
        YCoroutines.RecordCoroutine();

        new YWaitForSeconds(3);
        GetComponent<YTransform>().SetPosition(0f, 0, 0);
        new YWaitForSeconds(1);
        GetComponent<YTransform>().SetPosition(1f, 1, 1);
        new YWaitForSeconds(1);
        GetComponent<YTransform>().SetPosition(2f, 2, 2);
        new YWaitForSeconds(1);
        GetComponent<YTransform>().SetPosition(3f, 3, 3);

        yCoroutine = YCoroutines.GetCoroutine();
    }

    public override void Tick()
    {
        // free fly camera

        new Condition(YInputService.Get().P1Left())
            .Then(() =>
            {
                YMainCamera.Instance.Rotate(0, cameraRotationSpeed, 0);
            });
        new Condition(YInputService.Get().P1Right())
            .Then(() =>
            {
                YMainCamera.Instance.Rotate(0, new YFloat(-1) * cameraRotationSpeed, 0);
            });
        new Condition(YInputService.Get().P1Up())
            .Then(() =>
            {
                YMainCamera.Instance.TranslateLocal(0, 0, 0.1f);
            });
    }
}