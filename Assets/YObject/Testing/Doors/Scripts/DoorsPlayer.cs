using UnityEngine;

[RequireComponent (typeof(YTransform))]
public class DoorsPlayer : YMonoBehaviour
{
    private YVariable speed;
    private YVector3 rotation;

    public override void Init()
    {
        speed = new YFloat(3);
        rotation = new YVector3(0, 0, 0);
    }
    public override void Tick()
    {
        YTransform yTransform = GetComponent<YTransform>();

        yTransform.TranslateLocal(new YVector3(0,0,1) * YInputService.Get().P1Up() * new YVariable("Time.deltaTime") * speed);
        yTransform.TranslateLocal(new YVector3(-1,0,0) * YInputService.Get().P2Up() * new YVariable("Time.deltaTime") * speed);
        new Condition(YInputService.Get().P1Left())
            .Then(() =>
            {
                rotation.Add(new YVector3(0, -90, 0) * new YVariable("Time.deltaTime"));
            });
        new Condition(YInputService.Get().P1Right())
            .Then(() =>
            {
                rotation.Add(new YVector3(0, 90, 0) * new YVariable("Time.deltaTime"));
            });

        yTransform.SetRotation(rotation);


        YMainCamera.Instance.SetPosition(yTransform.GetPosition());
        YMainCamera.Instance.SetRotation(rotation);
    }
}
