public class YInt : YVariable
{
    public YInt(bool temporary = false) : base(GetNewID(false, temporary), false)
    {
    }
    public YInt(int value) : base(GetNewID(false), false)
    {
        SetDefaultValue(value);
    }
    public YInt(int value, bool temporary) : base(GetNewID(false, temporary), false)
    {
        SetDefaultValue(value);
    }
    private void SetDefaultValue(int value)
    {
        var trig = new ItemEdit(id, false, ItemEdit.Operation.Equals, value);
        if (YGameManager.Instance.gameobjectsAndServicesInitialization)
        {
            if (YGameobjectGroupsManager.Instance.CurrentGroupCompile == null)
            {
                trig.AddGroup(1001);
                YGameManager.Instance.globalBeginTriggers.Add(trig);
            }
            else
            {
                trig.AddGroup(YGameManager.Instance.groupsBeginGroup[YGameobjectGroupsManager.Instance.CurrentGroupCompile]);
                YGameManager.Instance.groupsBeginTriggers[YGameobjectGroupsManager.Instance.CurrentGroupCompile].Add(trig);
            }
        }
    }
}
