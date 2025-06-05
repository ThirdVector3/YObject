public abstract class YTrigger : YGDObject
{
    public abstract void Activate();

    public YTrigger()
    {
        YGameManager.Instance.globalPool.Add(this);
    }
}
