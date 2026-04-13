using UnityEngine;
using UnityEngine.UIElements;

public class IslandJumperPlayer : YMonoBehaviour
{
    public static IslandJumperPlayer Instance { get; private set; }

    [SerializeField] private float Speed = 3;
    [SerializeField] private float JumpForce = 1;
    [SerializeField] private YTransform cameraPos;
    [SerializeField] private float lerpSpeed = 10;


    private YVariable speed;
    private YVector3 rotation;
    private YVector3 cameraRotation;
    private YVariable yVelocity;
    private YVariable onGround;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
    public override void Init()
    {
        if (initialised)
            return;

        Instance = this;
        speed = new YFloat(Speed);
        rotation = new YVector3(0, 0, 0);
        cameraRotation = new YVector3(0, 0, 0);
        yVelocity = new YFloat(0);
        onGround = new YInt(0);

        initialised = true;
    }
    public override void Begin()
    {
        YGameService.Get().SetFPS(45);
    }
    public override void Tick()
    {
        YTransform yTransform = GetComponent<YTransform>();

        Move();
        Rotate();
        Jump();
        Fall();

        var cameraPosPosition = cameraPos.GetPosition();
        var cameraPosLocalPosition = cameraPos.GetLocalPosition();
        cameraPosLocalPosition.y.SetValue(YMathService.Get().Lerp(cameraPosLocalPosition.y, yVelocity * 10 + 1, new YVariable("Time.deltaTime") * lerpSpeed));
        cameraPos.SetLocalPosition(cameraPosLocalPosition);

        YMainCamera.Instance.yTransform.SetPosition(YVector3.Lerp(YMainCamera.Instance.yTransform.GetPosition(), cameraPosPosition, new YVariable("Time.deltaTime") * lerpSpeed));
        YMainCamera.Instance.yTransform.SetRotation(YQuaternion.Lerp(YMainCamera.Instance.yTransform.GetRotation(), YQuaternion.Euler(cameraRotation), new YVariable("Time.deltaTime") * lerpSpeed));


        yTransform.SetRotation(rotation);
    }
    private void Jump()
    {
        new Condition(YInputService.Get().P2UpDown() * onGround)
            .Then(() =>
            {
                yVelocity.SetValue(JumpForce * 0.01f);
            });
    }
    private void Fall()
    {
        YTransform yTransform = GetComponent<YTransform>();
        yTransform.Translate(new YVector3(new YFloat(0), yVelocity, new YFloat(0)));
        yVelocity.Subtract(0.2f * new YVariable("Time.deltaTime"));
        onGround.SetValue(0);
        foreach (var collider in FindObjectsByType<IslandJumperPlaneCollider>(sortMode: FindObjectsSortMode.InstanceID))
        {
            var collision = collider.SphereCollision(yTransform.GetPosition(), 0.5f);
            new Condition(collision * (yVelocity < 0))
                .Then(() =>
                {
                    yVelocity.SetValue(0);
                    onGround.SetValue(1);
                });
        }
        foreach (var collider in FindObjectsByType<IslandJumperBoxCollider>(sortMode: FindObjectsSortMode.InstanceID))
        {
            var collision = collider.SphereCollision(yTransform.GetPosition(), 0.5f);
            new Condition(collision)
                .Then(() =>
                {
                    yTransform.SetPosition(0, 0, 0);
                    yVelocity.SetValue(0);
                });
        }

        yTransform.SetScale(new YVector3(new YFloat(1), 1 + yVelocity * 3, new YFloat(1)));
    }
    private void Move()
    {
        YTransform yTransform = GetComponent<YTransform>();

        yTransform.TranslateLocal(new YVector3(0, 0, 1) * YInputService.Get().P1Up() * new YVariable("Time.deltaTime") * speed);
        //yTransform.TranslateLocal(new YVector3(-1, 0, 0) * YInputService.Get().P2Up() * new YVariable("Time.deltaTime") * speed);
    }
    private void Rotate()
    {
        new Condition(YInputService.Get().P1Left())
            .Then(() =>
            {
                rotation.Add(new YVector3(0, -180, 0) * new YVariable("Time.deltaTime"));
                cameraRotation.Add(new YVector3(0, -180, 0) * new YVariable("Time.deltaTime"));
            });
        new Condition(YInputService.Get().P1Right())
            .Then(() =>
            {
                rotation.Add(new YVector3(0, 180, 0) * new YVariable("Time.deltaTime"));
                cameraRotation.Add(new YVector3(0, 180, 0) * new YVariable("Time.deltaTime"));
            });

        //new Condition(YInputService.Get().P2Left())
        //    .Then(() =>
        //    {
        //        cameraRotation.Add(new YVector3(-180, 0, 0) * new YVariable("Time.deltaTime"));
        //    });
        //new Condition(YInputService.Get().P2Right())
        //    .Then(() =>
        //    {
        //        cameraRotation.Add(new YVector3(180, 0, 0) * new YVariable("Time.deltaTime"));
        //    });

        cameraRotation.x.SetValue(YMathService.Get().Lerp(cameraRotation.x, yVelocity * 200 + 20, new YVariable("Time.deltaTime") * lerpSpeed));
    }
    public void SetRotation(YVector3 rot)
    {
        rotation.SetValue(rot);
    }
}
