public class IslandJumperRotator : YMonoBehaviour
{
    public override void Tick()
    {
        GetComponent<YTransform>().Rotate(1f, 2f, 3f);
    }
}