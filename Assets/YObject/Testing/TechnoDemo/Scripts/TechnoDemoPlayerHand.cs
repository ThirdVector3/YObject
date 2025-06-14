using UnityEngine;

public class TechnoDemoPlayerHand : YMonoBehaviour
{
    public YTransform player;
    public Vector3 offset;
    public override void Tick()
    {
        player.GetPosition(9999, 9998, 9997);
        GetComponent<YTransform>().SetPosition(9999, 9998, 9997);
        player.GetRotation(9996, 9995, 9994);
        YInput.GetP2Up(new YTrigger[] {
        new ItemEdit(9996, true, ItemEdit.Operation.Subtract, 45),
        new ItemEdit(9995, true, ItemEdit.Operation.Subtract, 45)
        }, new YTrigger[0]);
        GetComponent<YTransform>().SetRotation(9996, 9995, 9994);
        GetComponent<YTransform>().TranslateLocal(offset.x, offset.y, offset.z);
    }
}
