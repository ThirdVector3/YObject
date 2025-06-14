using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TechnoDemoPlayer : YMonoBehaviour
{
    public override void Tick()
    {
        Rotate();
        Move();


        GetComponent<YTransform>().GetPosition(9999, 9998, 9997);
        GetComponent<YTransform>().GetRotation(9996, 9995, 9994);
        YMainCamera.Instance.SetPosition(9999, 9998, 9997);
        YMainCamera.Instance.SetRotation(9996, 9995, 9994);
    }

    private void Move()
    {
        YVariable move = new YFloat(2.5f) * new YVariable("Time.deltaTime");
        YInput.GetP1Up(GetComponent<YTransform>().TranslateLocal(0, 0, move.GetID()), new YTrigger[0]);
    }
    private void Rotate()
    {
        YVariable rotation = new YFloat(110f) * new YVariable("Time.deltaTime");
        YInput.GetP1Right(GetComponent<YTransform>().Rotate(0, rotation.GetID(), 0), new YTrigger[0]);
        rotation *= -1;
        YInput.GetP1Left(GetComponent<YTransform>().Rotate(0, rotation.GetID(), 0), new YTrigger[0]);
    }
}
