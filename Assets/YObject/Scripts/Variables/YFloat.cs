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
