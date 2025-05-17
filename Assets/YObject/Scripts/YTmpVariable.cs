using System.Collections.Generic;

public class YTmpVariable
{
    private static int lastId = 9999;
    protected static int GetNewId()
    {
        if (--lastId < 9000)
            lastId = 9999;
        return lastId;
    }

    public int id;
    public List<YTrigger> triggers = new List<YTrigger>();
}