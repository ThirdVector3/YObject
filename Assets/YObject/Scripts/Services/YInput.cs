public static class YInput
{
    public static YTrigger GetP1Left(YTrigger[] triggersTrue, YTrigger[] triggersFalse)
    {
        return new ItemCompare(YGameManager.Instance.IDsManager.GetIdByName("Input.P1Left"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, triggersTrue, triggersFalse);
    }
    public static YTrigger GetP1Right(YTrigger[] triggersTrue, YTrigger[] triggersFalse)
    {
        return new ItemCompare(YGameManager.Instance.IDsManager.GetIdByName("Input.P1Right"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, triggersTrue, triggersFalse);
    }
    public static YTrigger GetP1Up(YTrigger[] triggersTrue, YTrigger[] triggersFalse)
    {
        return new ItemCompare(YGameManager.Instance.IDsManager.GetIdByName("Input.P1Up"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, triggersTrue, triggersFalse);
    }
    public static YTrigger GetP2Left(YTrigger[] triggersTrue, YTrigger[] triggersFalse)
    {
        return new ItemCompare(YGameManager.Instance.IDsManager.GetIdByName("Input.P2Left"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, triggersTrue, triggersFalse);
    }
    public static YTrigger GetP2Right(YTrigger[] triggersTrue, YTrigger[] triggersFalse)
    {
        return new ItemCompare(YGameManager.Instance.IDsManager.GetIdByName("Input.P2Right"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, triggersTrue, triggersFalse);
    }
    public static YTrigger GetP2Up(YTrigger[] triggersTrue, YTrigger[] triggersFalse)
    {
        return new ItemCompare(YGameManager.Instance.IDsManager.GetIdByName("Input.P2Up"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, triggersTrue, triggersFalse);
    }
}
