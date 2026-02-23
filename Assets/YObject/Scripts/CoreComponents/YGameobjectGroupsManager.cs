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
    private string currentGroupCompile = null;
    public string CurrentGroupCompile
    {
        get
        {
            return currentGroupCompile;
        }
        set
        {
            currentGroupCompile = value;
        }
    }
    static YGameobjectGroupsManager()
    {
        Instance = new YGameobjectGroupsManager();
    }
    public YTrigger SetCurrentGroup(string group)
    {
        return new GroupToggleOn(group);
    }
}
