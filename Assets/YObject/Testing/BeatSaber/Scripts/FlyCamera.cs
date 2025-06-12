using System.Collections.Generic;
using Unity.VisualScripting;

public class FlyCamera : YMonoBehaviour
{
    public override void Tick()
    {
        //List<YTrigger> triggers = new List<YTrigger>();


        //var itemEditAddX = new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Add, 0.05f);
        //var itemCompareAddX = YInput.GetP1Left(new YTrigger[] { itemEditAddX }, new YTrigger[0]);
        //
        //
        //var itemEditSubX = new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Subtract, 0.05f);
        //var itemCompareSubX = YInput.GetP1Right(new YTrigger[] { itemEditSubX }, new YTrigger[0]);
        //
        //
        //var itemEditAddY = new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Add, 0.05f);
        //var itemCompareAddY = YInput.GetP2Left(new YTrigger[] { itemEditAddY }, new YTrigger[0]);
        //
        //
        //var itemEditSubY = new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Subtract, 0.05f);
        //var itemCompareSubY = YInput.GetP2Right(new YTrigger[] { itemEditSubY }, new YTrigger[0]);
        //
        //
        //
        //
        //
        //var itemEditAddZ = new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Add, 0.05f);
        //var itemCompareAddZ = YInput.GetP1Up(new YTrigger[] { itemEditAddZ }, new YTrigger[0]);
        //
        //var itemEditSubZ = new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Subtract, 0.05f);
        //var itemCompareSubZ = YInput.GetP2Up(new YTrigger[] { itemEditSubZ }, new YTrigger[0]);
        //
        //
        //
        //triggers.Add(itemCompareAddX);
        //triggers.Add(itemCompareSubX);
        //triggers.Add(itemCompareAddY);
        //triggers.Add(itemCompareSubY);
        //triggers.Add(itemCompareAddZ);
        //triggers.Add(itemCompareSubZ);

        //YMainCamera.Instance.TranslateLocal(0, 0, 0.05f);
        //YInput.GetP1Up(YMainCamera.Instance.TranslateLocal(0, 0, 0.1f), new YTrigger[0]);
        //YInput.GetP2Up(YMainCamera.Instance.TranslateLocal(0.1f, 0, 0), new YTrigger[0]);


        //YMainCamera.Instance.GetSin(9999,9998,9997);
        //YMainCamera.Instance.GetCos(9996,9995,9994);
        //new ItemEdit(9998, true, ItemEdit.Operation.Divide, 10);
        //new ItemEdit(9995, true, ItemEdit.Operation.Divide, -10);
        //new ItemEdit(9999, true, ItemEdit.Operation.Divide, 10);
        //new ItemEdit(9998, true, ItemEdit.Operation.Multiply, -1, 9996, true, 0, true, ItemEdit.Operation.Add);
        //new ItemEdit(9995, true, ItemEdit.Operation.Multiply, -1, 9996, true, 0, true, ItemEdit.Operation.Add);
        //new ItemEdit(9993, true, ItemEdit.Operation.Equals, -1, 9995, true, 0, true, ItemEdit.Operation.Add);
        YInput.GetP1Left(YMainCamera.Instance.Rotate(0,3f,0), new YTrigger[0]);
        YInput.GetP1Right(YMainCamera.Instance.Rotate(0,-3f,0), new YTrigger[0]);
        YInput.GetP2Left(YMainCamera.Instance.Rotate(3f, 0, 0), new YTrigger[0]);
        YInput.GetP2Right(YMainCamera.Instance.Rotate(-3f, 0, 0), new YTrigger[0]);
        //YInput.GetP1Up(YMainCamera.Instance.Translate(9998, 9999, 9995), new YTrigger[0]);
        //YInput.GetP2Up(YMainCamera.Instance.Translate(9993, 23, 9998), new YTrigger[0]);

        YInput.GetP1Up(YMainCamera.Instance.TranslateLocal(0, 0, 0.1f), new YTrigger[0]);
        YInput.GetP2Up(YMainCamera.Instance.TranslateLocal(-0.1f, 0, 0), new YTrigger[0]);

        //return triggers.ToArray();
    }
}