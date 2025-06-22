public abstract class YService<T> : YServiceBase where T : YService<T>, new()
{
    public static T Get() => YGameManager.GetService<T>();
}
public abstract class YServiceBase
{
    protected bool initialised;
    public virtual void Uninit()
    {
        initialised = false;
    }
    public virtual void Init() { }
    public virtual void Begin() { }
    public virtual void Tick() { }
}