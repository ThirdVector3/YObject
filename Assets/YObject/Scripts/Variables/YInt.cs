public class YInt : YVariable
{
    public YInt() : base(GetNewID(false), false)
    {
    }
    public YInt(int value) : base(GetNewID(false), false)
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
