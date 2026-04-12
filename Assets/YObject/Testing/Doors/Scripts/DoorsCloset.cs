using UnityEngine;

public class DoorsCloset : YMonoBehaviour
{
    [SerializeField] private YTransform leftDoor;
    [SerializeField] private YTransform rightDoor;

    private Coroutine doorsTransitionIn;
    private Coroutine doorsTransitionOut;
    private YVariable inDoorsTransition;
    private YVariable doorsTransitionStartTime;
    private float doorsTransitionTime = 0.7f;

    public override void Init()
    {
        inDoorsTransition = new YInt(0);
        doorsTransitionStartTime = new YFloat();

        FindAnyObjectByType<DoorsPlayer>().Init();

        YCoroutines.RecordCoroutine();

        doorsTransitionStartTime.SetValue(new YVariable("Time.time"));
        inDoorsTransition.SetValue(1);
        new YWaitForSeconds(doorsTransitionTime);
        inDoorsTransition.SetValue(0);
        DoorsPlayer.Instance.SetIsInCloset();

        doorsTransitionIn = YCoroutines.GetCoroutine();


        YCoroutines.RecordCoroutine();
        
        doorsTransitionStartTime.SetValue(new YVariable("Time.time"));
        inDoorsTransition.SetValue(2);
        new YWaitForSeconds(doorsTransitionTime);
        inDoorsTransition.SetValue(0);
        DoorsPlayer.Instance.SetIsNotInCloset();
        
        doorsTransitionOut = YCoroutines.GetCoroutine();
    }
    public override void Tick()
    {
        new Condition(inDoorsTransition, new YInt(1), ItemCompare.Operation.Equals)
            .Then(() =>
            {
                DoorsTransitionIn();
            });
        new Condition(inDoorsTransition, new YInt(2), ItemCompare.Operation.Equals)
            .Then(() =>
            {
                DoorsTransitionOut();
            });
    }
    public void HideToThis()
    {
        YCoroutines.StartCoroutine(doorsTransitionIn);
    }
    public void LeaveThis()
    {
        YCoroutines.StartCoroutine(doorsTransitionOut);
    }
    private void DoorsTransitionIn()
    {
        var cos = -YMathService.Get().CosDeg((new YVariable("Time.time") - doorsTransitionStartTime) * 360 / doorsTransitionTime);
        var cos2 = -YMathService.Get().CosDeg((new YVariable("Time.time") - doorsTransitionStartTime) * 180 / doorsTransitionTime);
        leftDoor.SetLocalRotation(new YVector3(0, 45, 0) * cos + new YVector3(0, 45 - 90, 0));
        rightDoor.SetLocalRotation(new YVector3(0, -45, 0) * cos - new YVector3(0, 45 - 90, 0));

        var playerTransform = DoorsPlayer.Instance.GetComponent<YTransform>();
        var yTransform = GetComponent<YTransform>();

        DoorsPlayer.Instance.SetRotation(new YVector3(0, 90, 0) * cos2 + new YVector3(0, 90 + transform.eulerAngles.y, 0));

        var endPos = yTransform.GetPosition();
        endPos.y = DoorsPlayer.Instance.GetPositionBeforeCloset().y;
        playerTransform.SetPosition(YVector3.Lerp(DoorsPlayer.Instance.GetPositionBeforeCloset(), endPos, (new YVariable("Time.time") - doorsTransitionStartTime) / doorsTransitionTime));
    }
    private void DoorsTransitionOut()
    {
        var cos = -YMathService.Get().CosDeg((new YVariable("Time.time") - doorsTransitionStartTime) * 360 / doorsTransitionTime);
        var cos2 = -YMathService.Get().CosDeg((new YVariable("Time.time") - doorsTransitionStartTime) * 180 / doorsTransitionTime);
        leftDoor.SetLocalRotation(new YVector3(0, 45, 0) * cos + new YVector3(0, 45 - 90, 0));
        rightDoor.SetLocalRotation(new YVector3(0, -45, 0) * cos - new YVector3(0, 45 - 90, 0));

        var playerTransform = DoorsPlayer.Instance.GetComponent<YTransform>();
        var yTransform = GetComponent<YTransform>();

        var startPos = yTransform.GetPosition();
        startPos.y = DoorsPlayer.Instance.GetPositionBeforeCloset().y;
        playerTransform.SetPosition(YVector3.Lerp(startPos, DoorsPlayer.Instance.GetPositionBeforeCloset(), (new YVariable("Time.time") - doorsTransitionStartTime) / doorsTransitionTime));
    }
}
