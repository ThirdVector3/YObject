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

        //new YVector3(9999,9998,9997).SetValue(GetComponent<YTransform>().GetPosition());
        //new YQuaternion(9996, 9995, 9994, 9993).SetValue(GetComponent<YTransform>().GetRotation());
        var v = YQuaternion.Euler(new YVector3(0,0f,0f)).ToEulerAngles();
        YMainCamera.Instance.yTransform.SetPosition(GetComponent<YTransform>().GetPosition());
        YMainCamera.Instance.yTransform.SetRotation(v.x, v.y, v.z);
    }

    private void Move()
    {
        YVariable move = new YFloat(2.5f) * new YVariable("Time.deltaTime");
        //YInput.GetP1Up(GetComponent<YTransform>().TranslateLocal(0, 0, move.GetID()), new YTrigger[0]);
        new Condition(YInputService.Get().P1Up())
            .Then(() =>
            {
                GetComponent<YTransform>().TranslateLocal(0, 0, move);
            });
    }
    private void Rotate()
    {
        YVariable rotation = new YFloat(110f) * new YVariable("Time.deltaTime");
        print("Input disabled!!!");
        //YInput.GetP1Right(GetComponent<YTransform>().Rotate(0, rotation, 0), new YTrigger[0]);
        rotation.Multiply(-1);
        //YInput.GetP1Left(GetComponent<YTransform>().Rotate(0, rotation, 0), new YTrigger[0]);
    }
}
