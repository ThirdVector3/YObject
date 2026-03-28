using UnityEngine;

public class DoorsCloset : YMonoBehaviour
{
    [SerializeField] private YTransform leftDoor;
    [SerializeField] private YTransform rightDoor;

    private Coroutine doorsTransition;
    private YVariable inDoorsTransition;
    private YVariable doorsTransitionStartTime;

    public override void Init()
    {
        inDoorsTransition = new YInt(0);
        doorsTransitionStartTime = new YFloat();

        YCoroutines.RecordCoroutine();

        doorsTransitionStartTime.SetValue(new YVariable("Time.time"));
        inDoorsTransition.SetValue(1);
        new YWaitForSeconds(1);
        inDoorsTransition.SetValue(0);

        doorsTransition = YCoroutines.GetCoroutine();
    }
    public override void Tick()
    {
        new Condition(YInputService.Get().P2LeftDown())
            .Then(() =>
            {
                YCoroutines.StartCoroutine(doorsTransition);
            });

        new Condition(inDoorsTransition)
            .Then(() =>
            {
                DoorsTransition();
            });
    }
    public override void Begin()
    {
        YGameService.Get().SetFPS(60);
    }
    private void DoorsTransition()
    {
        var cos = -YMathService.Get().CosDeg((new YVariable("Time.time") - doorsTransitionStartTime) * 360);
        leftDoor.SetLocalRotation(new YVector3(0, 45, 0) * cos + new YVector3(0, 45, 0));
        rightDoor.SetLocalRotation(new YVector3(0, -45, 0) * cos - new YVector3(0, 45, 0));
    }
}
