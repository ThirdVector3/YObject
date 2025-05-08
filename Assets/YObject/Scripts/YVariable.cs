public class YVariable
{
    private static int lastId = 9999;
    protected static int GetNewId()
    {
        if (--lastId < 9000)
            lastId = 9999;
        return lastId;
    }

    public int id;
    public YTrigger[] triggers;
}