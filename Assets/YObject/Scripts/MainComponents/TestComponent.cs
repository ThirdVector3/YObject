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


        var itemEditAddX = new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.x"),true, ItemEdit.Operation.Add, 0.05f);
        int freeGroup = YGameManager.Instance.GetFreeGroup();
        itemEditAddX.groups = new int[] { freeGroup };
        YGameManager.Instance.AddGroup(freeGroup);
        var itemCompareAddX = new ItemCompare(YGameManager.Instance.GetIdByName("Input.P1Left"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, freeGroup, 0, new YTrigger[] { itemEditAddX }, new YTrigger[0]);

        var itemEditSubX = new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Subtract, 0.05f);
        freeGroup = YGameManager.Instance.GetFreeGroup();
        itemEditSubX.groups = new int[] { freeGroup };
        YGameManager.Instance.AddGroup(freeGroup);
        var itemCompareSubX = new ItemCompare(YGameManager.Instance.GetIdByName("Input.P1Right"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, freeGroup, 0, new YTrigger[] { itemEditSubX }, new YTrigger[0]);

        var itemEditAddY = new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Add, 0.05f);
        freeGroup = YGameManager.Instance.GetFreeGroup();
        itemEditAddY.groups = new int[] { freeGroup };
        YGameManager.Instance.AddGroup(freeGroup);
        var itemCompareAddY = new ItemCompare(YGameManager.Instance.GetIdByName("Input.P2Left"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, freeGroup, 0, new YTrigger[] { itemEditAddY }, new YTrigger[0]);

        var itemEditSubY = new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Subtract, 0.05f);
        freeGroup = YGameManager.Instance.GetFreeGroup();
        itemEditSubY.groups = new int[] { freeGroup };
        YGameManager.Instance.AddGroup(freeGroup);
        var itemCompareSubY = new ItemCompare(YGameManager.Instance.GetIdByName("Input.P2Right"), 0, true, true, 1, 1, ItemCompare.Operation.Equals, freeGroup, 0, new YTrigger[] { itemEditSubY }, new YTrigger[0]);

        triggers.Add(itemCompareAddX);
        triggers.Add(itemCompareSubX);
        triggers.Add(itemCompareAddY);
        triggers.Add(itemCompareSubY);

        //triggers.Add(new ItemEdit(5001, true, ItemEdit.Operation.Equals, 90));

        //triggers.AddRange(YMath.CosDeg(5001, 5000));

        triggers.AddRange(GetComponent<YTransform>().SetState(1));
        triggers.AddRange(GetComponent<YTransform>().Rotate(0,1f,0));


        return triggers.ToArray();
    }
}
