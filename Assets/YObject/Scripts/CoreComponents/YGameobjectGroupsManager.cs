public class YGameobjectGroupsManager
{
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
    public YTrigger[] SetCurrentGroup(string group)
    {
        return new YTrigger[] { new GroupToggleOn(group) };
    }
}
