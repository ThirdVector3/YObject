public static class YInput
{
    public static YTrigger GetP1Left(int trueId, int falseId, YTrigger[] triggersTrue, YTrigger[] triggersFalse)
    {
        return new ItemCompare(YGameManager.Instance.GetIdByName("Input.P1Left"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, trueId, falseId, triggersTrue, triggersFalse);
    }
    public static YTrigger GetP1Right(int trueId, int falseId, YTrigger[] triggersTrue, YTrigger[] triggersFalse)
    {
        return new ItemCompare(YGameManager.Instance.GetIdByName("Input.P1Right"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, trueId, falseId, triggersTrue, triggersFalse);
    }
    public static YTrigger GetP1Up(int trueId, int falseId, YTrigger[] triggersTrue, YTrigger[] triggersFalse)
    {
        return new ItemCompare(YGameManager.Instance.GetIdByName("Input.P1Up"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, trueId, falseId, triggersTrue, triggersFalse);
    }
    public static YTrigger GetP2Left(int trueId, int falseId, YTrigger[] triggersTrue, YTrigger[] triggersFalse)
    {
        return new ItemCompare(YGameManager.Instance.GetIdByName("Input.P2Left"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, trueId, falseId, triggersTrue, triggersFalse);
    }
    public static YTrigger GetP2Right(int trueId, int falseId, YTrigger[] triggersTrue, YTrigger[] triggersFalse)
    {
        return new ItemCompare(YGameManager.Instance.GetIdByName("Input.P2Right"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, trueId, falseId, triggersTrue, triggersFalse);
    }
    public static YTrigger GetP2Up(int trueId, int falseId, YTrigger[] triggersTrue, YTrigger[] triggersFalse)
    {
        return new ItemCompare(YGameManager.Instance.GetIdByName("Input.P2Up"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, trueId, falseId, triggersTrue, triggersFalse);
    }
}
