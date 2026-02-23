public class YFloat : YVariable
{
    public YFloat() : base(GetNewID(true), true)
    {
    }
    public YFloat(float value) : base(GetNewID(true), true)
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
