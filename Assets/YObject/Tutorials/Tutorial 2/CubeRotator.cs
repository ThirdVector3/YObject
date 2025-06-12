using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotator : YMonoBehaviour
{
    Coroutine moveCoroutine;
    public override void Init()
    {
        GetComponent<YTransform>().Init();

        YCoroutines.RecordCoroutine();

        new YWaitForSeconds(1);
        GetComponent<YTransform>().SetPosition(1f, 0, 0);
        new YWaitForSeconds(1);
        GetComponent<YTransform>().SetPosition(2f, 1, 0);

        moveCoroutine = YCoroutines.GetCoroutine();
    }
    public override void Begin()
    {
        YCoroutines.StartCoroutine(moveCoroutine);
    }
    public override void Tick()
    {
        YVariable rotation = new YFloat(50) * new YVariable("Time.deltaTime");
        GetComponent<YTransform>().Rotate(0, rotation.GetID(), 0);
    }
}
