public class YGameobjectGroupsManager
{
    public static YGameobjectGroupsManager Instance { get; private set; }
    private string currentGroup = null;
    public string CurrentGroup 
    { 
        get
        {
            return currentGroup; 
        }
        set
        {
            currentGroup = value;
        }
    }
    public YGameobjectGroupsManager()
    {
        Instance = this;
    }
    public YTrigger[] SetCurrentGroup(string group)
    {
        return new YTrigger[] { new GroupToggleOn(group) };
    }
}
