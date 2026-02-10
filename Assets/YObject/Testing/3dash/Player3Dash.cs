using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3Dash : YMonoBehaviour
{
    private YTransform yTransform;
    private YVariable isGrounded;
    public YVariable IsGrounded
    {
        get 
        { 
            return isGrounded;
        }
    }
    private YVariable velocityY;
    public YTransform GetYTransform() => yTransform;

    public override void Init()
    {
        velocityY = new YFloat(0);
        isGrounded = new YInt(0);
        yTransform = GetComponent<YTransform>();
    }
    public override void Tick()
    {
        YVector3 translation = new YVector3(0, 0f, 0f);

        translation.x.SetValue(5);


        isGrounded.SetValue(BoxCollider3Dash.GetCollisionsCount());


        new Condition(isGrounded)
            .Then(() =>
            {
                velocityY.SetValue(0f);
            })
            .Else(() =>
            {
                velocityY.Subtract(0.5f); 
            });

        new Condition(new YInt(1) * YInputService.Get().P1Up() * isGrounded)
            .Then(() =>
            {
                velocityY.SetValue(12);
            });

        translation.y.SetValue(velocityY);

        yTransform.TranslateLocal(translation.x * new YVariable("Time.deltaTime"), translation.y * new YVariable("Time.deltaTime"), translation.z * new YVariable("Time.deltaTime"));

        BoxCollider3Dash.ResetCollisionData();
    }
}
