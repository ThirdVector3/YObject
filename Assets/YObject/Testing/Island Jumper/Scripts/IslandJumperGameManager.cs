public class IslandJumperGameManager : YMonoBehaviour
{
    public static IslandJumperGameManager Instance { get; private set; }

    public YVariable InMenu { get; private set; }
    private YVariable optionSelected;
    private YVariable fpsSelected;

    public override void Init()
    {
        Instance = this;
        InMenu = new YInt(1);
        optionSelected = new YInt(0);
        fpsSelected = new YInt(0);

        YGameManager.Instance.RecordPool();
        optionSelected.SetValue(1);
        foreach (var trigger in YGameManager.Instance.StopRecordPool(removeNonFirstLevel: true))
        {
            trigger.AddGroup(852);
        }

        YGameManager.Instance.RecordPool();
        optionSelected.SetValue(0);
        foreach (var trigger in YGameManager.Instance.StopRecordPool(removeNonFirstLevel: true))
        {
            trigger.AddGroup(851);
        }
    }

    public override void Begin()
    {
        YGameService.Get().SetFPS(10);
    }

    public override void Tick()
    {
        new ItemEdit(7000, false, ItemEdit.Operation.Equals, 1, YGameService.Get().GetFPS(), true, 0, false, ItemEdit.Operation.Add);
        new Condition(InMenu)
        .Then(() =>
        {
            new Condition((YInputService.Get().P1UpDown() + YInputService.Get().P2UpDown()) * (optionSelected == 0))
            .Then(() =>
            {
                new Toggle(853, false);
                new Spawn(854);
                InMenu.SetValue(0);
            });
            new Condition((YInputService.Get().P1UpDown() + YInputService.Get().P2UpDown()) * (optionSelected == 1))
            .Then(() =>
            {
                fpsSelected.Add(1);
                new Condition(fpsSelected > 5)
                .Then(() =>
                {
                    fpsSelected.SetValue(0);
                });

                new Condition(fpsSelected == 0)
                .Then(() =>
                {
                    YGameService.Get().SetFPS(10);
                });
                new Condition(fpsSelected == 1)
                .Then(() =>
                {
                    YGameService.Get().SetFPS(20);
                });
                new Condition(fpsSelected == 2)
                .Then(() =>
                {
                    YGameService.Get().SetFPS(30);
                });
                new Condition(fpsSelected == 3)
                .Then(() =>
                {
                    YGameService.Get().SetFPS(40);
                });
                new Condition(fpsSelected == 4)
                .Then(() =>
                {
                    YGameService.Get().SetFPS(50);
                });
                new Condition(fpsSelected == 5)
                .Then(() =>
                {
                    YGameService.Get().SetFPS(60);
                });
            });
        });
    }
}
