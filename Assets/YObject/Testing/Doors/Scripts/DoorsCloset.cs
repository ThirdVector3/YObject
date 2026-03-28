using UnityEngine;

public class DoorsCloset : YMonoBehaviour
{
    [SerializeField] private YTransform leftDoor;
    [SerializeField] private YTransform rightDoor;
    public override void Tick()
    {
        leftDoor.SetLocalRotation(new YVector3(0,45,0) * YMathService.Get().SinRad(new YVariable("Time.time")) + new YVector3(0, 45, 0));
    }
}
