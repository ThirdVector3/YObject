public abstract class YTrigger : YGDObject
{
    public bool isFirstLevel = true;
    public abstract void Activate();

    public YTrigger()
    {
        YGameManager.Instance.globalPool.Add(this);
    }
}
