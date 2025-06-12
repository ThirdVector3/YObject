public class YInt : YVariable
{
    public YInt() : base(GetNewID(false), false)
    {
    }
    public YInt(int value) : base(GetNewID(false), false)
    {
        var trig = new ItemEdit(id, false, ItemEdit.Operation.Equals, value);
        if (YGameManager.Instance.gameobjectsInitialization)
        {
            if (YIDsManager.Instance.GetCurrentGroupName() == null)
            {
                trig.AddGroup(1001);
                YGameManager.Instance.globalBeginTriggers.Add(trig);
            }
            else
            {
                trig.AddGroup(YGameManager.Instance.groupsBeginGroup[YIDsManager.Instance.GetCurrentGroupName()]);
                YGameManager.Instance.groupsBeginTriggers[YIDsManager.Instance.GetCurrentGroupName()].Add(trig);
            }
        }
    }
}
