using UnityEngine;

public class YRandomService : YService<YRandomService>
{
    public YVariable Random(float percent)
    {
        var ret = new YInt();

        new RandomTrigger(percent, new YTrigger[] { new ItemEdit(ret, false, ItemEdit.Operation.Equals, 1) }, new YTrigger[] { new ItemEdit(ret, false, ItemEdit.Operation.Equals, 0) });
        
        return ret;
    }
    public YVariable RangeInt(int start, int end)
    {
        return RangeInt(new YInt(start), new YInt(end));
    }
    public YVariable RangeInt(YVariable start, YVariable end)
    {
        var ret = new YInt(0);

        for (int i = 0; i < 20; i++)
        {
            ret.Add(Random(50f) * Mathf.Pow(2, i));
        }

        ret.SetValue(YMathService.Get().Mod(ret, end - start) + start);

        return ret;
    }
}
