using System.Collections.Generic;

public class TestComponent : YMonoBehaviour
{
    public void Update()
    {
        //print(YGameManager.Instance.GetMemoryValue(5000));
    }

    public override YTrigger[] Begin()
    {
        return new YTrigger[0];
    }

    public override YGDObject[] Init()
    {
        return new YGDObject[0];
    }

    public override YTrigger[] Tick()
    {
        List<YTrigger> triggers = new List<YTrigger>();



        //triggers.Add(new ItemEdit(5001, true, ItemEdit.Operation.Equals, 100));
        //triggers.AddRange(YMath.Sqrt(5001, 5000));

        triggers.AddRange(GetComponent<YTransform>().SetState(1));
        triggers.AddRange(GetComponent<YTransform>().Rotate(1f,1f,0));


        return triggers.ToArray();
    }
}
