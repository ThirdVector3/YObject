using System.Collections.Generic;

public class FlyCamera : YMonoBehaviour
{
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


        var itemEditAddX = new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Add, 0.05f);
        //int freeGroup = YGameManager.Instance.IDsManager.GetFreeGroup();
        //itemEditAddX.AddGroup(freeGroup);// = new int[] { freeGroup };
        //YGameManager.Instance.IDsManager.AddGroup(freeGroup);
        var itemCompareAddX = YInput.GetP1Left(new YTrigger[] { itemEditAddX }, new YTrigger[0]);


        var itemEditSubX = new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Subtract, 0.05f);
        //freeGroup = YGameManager.Instance.IDsManager.GetFreeGroup();
        //itemEditSubX.AddGroup(freeGroup);//  = new int[] { freeGroup };
        //YGameManager.Instance.IDsManager.AddGroup(freeGroup);
        var itemCompareSubX = YInput.GetP1Right(new YTrigger[] { itemEditSubX }, new YTrigger[0]);


        var itemEditAddY = new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Add, 0.05f);
        //freeGroup = YGameManager.Instance.IDsManager.GetFreeGroup();
        //itemEditAddY.AddGroup(freeGroup);//  = new int[] { freeGroup };
        //YGameManager.Instance.IDsManager.AddGroup(freeGroup);
        var itemCompareAddY = YInput.GetP2Left(new YTrigger[] { itemEditAddY }, new YTrigger[0]);


        var itemEditSubY = new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Subtract, 0.05f);
        //freeGroup = YGameManager.Instance.IDsManager.GetFreeGroup();
        //itemEditSubY.AddGroup(freeGroup);//  = new int[] { freeGroup };
        //YGameManager.Instance.IDsManager.AddGroup(freeGroup);
        var itemCompareSubY = YInput.GetP2Right(new YTrigger[] { itemEditSubY }, new YTrigger[0]);





        var itemEditAddZ = new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Add, 0.05f);
        //freeGroup = YGameManager.Instance.IDsManager.GetFreeGroup();
        //itemEditAddZ.AddGroup(freeGroup);//  = new int[] { freeGroup };
        //YGameManager.Instance.IDsManager.AddGroup(freeGroup);
        var itemCompareAddZ = YInput.GetP1Up(new YTrigger[] { itemEditAddZ }, new YTrigger[0]);

        var itemEditSubZ = new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Subtract, 0.05f);
        //freeGroup = YGameManager.Instance.IDsManager.GetFreeGroup();
        //itemEditSubZ.AddGroup(freeGroup);//  = new int[] { freeGroup };
        //YGameManager.Instance.IDsManager.AddGroup(freeGroup);
        var itemCompareSubZ = YInput.GetP2Up(new YTrigger[] { itemEditSubZ }, new YTrigger[0]);



        triggers.Add(itemCompareAddX);
        triggers.Add(itemCompareSubX);
        triggers.Add(itemCompareAddY);
        triggers.Add(itemCompareSubY);
        triggers.Add(itemCompareAddZ);
        triggers.Add(itemCompareSubZ);

        return triggers.ToArray();
    }
}