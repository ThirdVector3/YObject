using UnityEngine;

public class YGameService : YService<YGameService>
{
    private YVariable lastTime;
    private YVariable fps;
    public override void Init()
    {
        lastTime = new YFloat();
        fps = new YInt();

        YGameManager.Instance.RecordPool();

        new Condition(new YVariable("Time.time") - lastTime, 1 / fps, ItemCompare.Operation.MoreOrEquals)
            .Then(() =>
            {
                new YVariable("Time.deltaTime").SetValue(1 / fps);
                for (int i = 0;i < 6;i++)
                {
                    new Spawn(1010 + i, i * 0); // * 0.0042f
                }
                new Spawn(1000, 0);
                new Spawn(1002, 0.0001f);
                new Spawn(1003, 0.0002f);
                lastTime.SetValue(new YVariable("Time.time"));
            });

        foreach (var trigger in YGameManager.Instance.StopRecordPool(removeNonFirstLevel: true))
        {
            trigger.AddGroup(134);
        }
    }
    public override void Begin()
    {
        lastTime.SetValue(0);
        fps.SetValue(30);
    }

    public void SetFPS(YVariable fps)
    {
        this.fps.SetValue(fps);
    }
    public void SetFPS(int fps)
    {
        this.fps.SetValue(fps);
    }
    public YVariable GetFPS()
    {
        return fps + 0;
    }


    public override void Update()
    {
        Time.fixedDeltaTime = 1f / YIDsManager.Instance.GetMemoryValue(fps).Item1;
    }
}