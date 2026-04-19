using UnityEngine;

public class IslandJumperHammer : YMonoBehaviour
{
    [SerializeField] private float speed = 1;

    public override void Tick()
    {
        var yTransform = GetComponent<YTransform>();

        yTransform.Rotate(0, 0, YMathService.Get().CosRad(new YVariable("Time.time") * speed) * new YVariable("Time.deltaTime") * speed * 45);
    }
}