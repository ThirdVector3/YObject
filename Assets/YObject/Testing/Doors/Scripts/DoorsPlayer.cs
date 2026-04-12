using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent (typeof(YTransform))]
public class DoorsPlayer : YMonoBehaviour
{
    public static DoorsPlayer Instance { get; private set; }

    private YVariable speed;
    private YVector3 rotation;

    private YVariable isInCloset;
    private YVariable isInClosetTransition;
    private YVector3 positionBeforeCloset;
    public override void Init()
    {
        if (initialised)
            return;

        Instance = this;
        speed = new YFloat(3);
        rotation = new YVector3(0, 0, 0);
        isInCloset = new YInt(0);
        isInClosetTransition = new YInt(0);
        positionBeforeCloset = new YVector3();

        initialised = true;
    }
    public override void Begin()
    {
        YGameService.Get().SetFPS(60);
    }
    public override void Tick()
    {
        YTransform yTransform = GetComponent<YTransform>();

        new Condition(isInCloset + isInClosetTransition)
            .Then(() =>
            {
                new Condition(YInputService.Get().P1UpDown() * (1 - isInClosetTransition) * isInCloset)
                .Then(() =>
                {
                    var closetIsClose = new YInt(0);
                    foreach (var closet in FindObjectsByType<DoorsCloset>(sortMode: FindObjectsSortMode.InstanceID))
                    {
                        var pos = closet.GetComponent<YTransform>().GetPosition();
                        var selfPos = yTransform.GetPosition();
                        pos = pos - selfPos;
                        new Condition((pos.x * pos.x + pos.y * pos.y + pos.z * pos.z < 1.5f) * (1 - closetIsClose))
                        .Then(() =>
                        {
                            closetIsClose.SetValue(1);
                            isInClosetTransition.SetValue(1);
                            closet.LeaveThis();
                        });
                    }
                });
            })
            .Else(() =>
            {
                Move();
                Rotate();

                var closetIsClose = new YInt(0);
                foreach (var closet in FindObjectsByType<DoorsCloset>(sortMode: FindObjectsSortMode.InstanceID))
                {
                    var pos = closet.GetComponent<YTransform>().GetPosition();
                    var selfPos = yTransform.GetPosition();
                    pos = pos - selfPos;
                    new Condition((pos.x * pos.x + pos.y * pos.y + pos.z * pos.z < 1.5f) * (1 - closetIsClose))
                    .Then(() =>
                    {
                        pos.y.SetValue(0);
                        yTransform.Translate(pos * -0.1f);
                        var worldTransform = yTransform.LocalToParentTransform();
                        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Equals, 1, worldTransform.position.x, true, 0, true, ItemEdit.Operation.Add);
                        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Equals, 1, worldTransform.position.y, true, 0, true, ItemEdit.Operation.Add);
                        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Equals, 1, worldTransform.position.z, true, 0, true, ItemEdit.Operation.Add);
                        positionBeforeCloset.SetValue(GetComponent<YTransform>().GetPosition());
                        closetIsClose.SetValue(1);
                        isInClosetTransition.SetValue(1);
                        closet.HideToThis();
                    });
                }
            });


        YMainCamera.Instance.SetPosition(yTransform.GetPosition());
        YMainCamera.Instance.SetRotation(rotation);
        yTransform.SetRotation(rotation);
    }

    private void Move()
    {
        YTransform yTransform = GetComponent<YTransform>();

        yTransform.TranslateLocal(new YVector3(0, 0, 1) * YInputService.Get().P1Up() * new YVariable("Time.deltaTime") * speed);
        yTransform.TranslateLocal(new YVector3(-1, 0, 0) * YInputService.Get().P2Up() * new YVariable("Time.deltaTime") * speed);
    }
    private void Rotate()
    {
        //YTransform yTransform = GetComponent<YTransform>();

        new Condition(YInputService.Get().P1Left())
            .Then(() =>
            {
                rotation.Add(new YVector3(0, -180, 0) * new YVariable("Time.deltaTime"));
            });
        new Condition(YInputService.Get().P1Right())
            .Then(() =>
            {
                rotation.Add(new YVector3(0, 180, 0) * new YVariable("Time.deltaTime"));
            });
    }
    public void SetRotation(YVector3 rot)
    {
        rotation.SetValue(rot);
    }
    public YVector3 GetPositionBeforeCloset()
    {
        return positionBeforeCloset;
    }
    public void SetIsInCloset()
    {
        isInCloset.SetValue(1);
        isInClosetTransition.SetValue(0);
    }
    public void SetIsNotInCloset()
    {
        isInCloset.SetValue(0);
        isInClosetTransition.SetValue(0);
    }
}
