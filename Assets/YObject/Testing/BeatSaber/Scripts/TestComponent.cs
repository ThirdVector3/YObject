using System.Collections.Generic;
using UnityEngine;

public class TestComponent : YMonoBehaviour
{
    public void Update()
    {
        //print(YGameManager.Instance.GetMemoryValue(5000));
    }

    public override void Begin()
    {



        List<YTrigger> triggers = new List<YTrigger>();

        //triggers.Add(YCoroutines.StartCoroutine(yCoroutine));

        //triggers.Add(new ColorTrigger(1000, 1, Color.white));

        //triggers.Add(new SongTrigger(63, 0, 1, true, 0, 0, 0, 0));

        //return triggers.ToArray();
    }

    private Coroutine yCoroutine;
    public override YGDObject[] Init()
    {
        List<YTrigger> triggers = new List<YTrigger>();


        GetComponent<YTransform>().Init();

        triggers.Add(new YWaitForSeconds(3));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(0f, 0, 0));
        triggers.Add(new YWaitForSeconds(1));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(1f, 1, 1));
        triggers.Add(new YWaitForSeconds(1));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(2f, 2, 2));
        triggers.Add(new YWaitForSeconds(1));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(3f, 3, 3));

        yCoroutine = YCoroutines.GetCoroutine(triggers.ToArray()); //new YCoroutine(triggers.ToArray());

        return new YGDObject[]
        {
            yCoroutine
        };
    }

    public override void Tick()
    {
        //List<YTrigger> triggers = new List<YTrigger>();


        //YTmpVariable yVar = new YTmpFloat(10f) + new YTmpFloat(11f) + new YTmpFloat(66) * 2 / (1 - new YTmpInt(30) + 2);
        //foreach (var trig in yVar.triggers)
        //{
        //    ItemEdit itemEdit = (ItemEdit)trig;
        //    print($"{itemEdit.editID}, {itemEdit.operation}, {itemEdit.multiplier}, {itemEdit.setID1}");
        //}
        //triggers.AddRange(yVar.triggers);




        //foreach (var trigger in yVar.triggers)
        //{
        //    var a = ((ItemEdit)trigger);
        //    print($"{a.editID}, {a.operation}, {a.multiplier}, {a.setID1}");
        //}


        //triggers.Add(new ItemEdit(5001, true, ItemEdit.Operation.Equals, 100));
        //triggers.AddRange(YMath.Sqrt(5001, 5000));

        GetComponent<YTransform>().Rotate(0.2f, 0.2f, 0);

        //triggers.Add(new RandomTrigger(50, GetComponent<YTransform>().Translate(0.05f, 0, 0), GetComponent<YTransform>().Translate(-0.05f, 0, 0)));



        //triggers.Add(YInput.GetP1Left(YGameManager.Instance.GameobjectGroupsManager.SetCurrentGroup("2"), new YTrigger[0]));
        //triggers.Add(YInput.GetP1Right(YGameManager.Instance.GameobjectGroupsManager.SetCurrentGroup("1"), new YTrigger[0]));


        //return triggers.ToArray();
    }
}
