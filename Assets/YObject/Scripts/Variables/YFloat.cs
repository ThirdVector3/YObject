public class YFloat : YVariable
{
    public YFloat(bool temporary = false) : base(GetNewID(true, temporary), true)
    {
    }
    public YFloat(float value) : base(GetNewID(true), true)
    {
        SetDefaultValue(value);
    }
    public YFloat(float value, bool temporary) : base(GetNewID(true, temporary), true)
    {
        SetDefaultValue(value);
    }
    private void SetDefaultValue(float value)
    {
        var trig = new ItemEdit(id, true, ItemEdit.Operation.Equals, value);
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
