using System.Collections.Generic;
using UnityEngine;

public class TestComponent : YMonoBehaviour
{
    public void Update()
    {
        //print(YGameManager.Instance.GetMemoryValue(5000));
    }

    public override YTrigger[] Begin()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        //triggers.Add(YCoroutines.StartCoroutine(yCoroutine));

        return triggers.ToArray();
    }

    private CoroutineTrigger yCoroutine;
    public override YGDObject[] Init()
    {
        List<YTrigger> triggers = new List<YTrigger>();
        triggers.Add(new YWaitForSeconds(3));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(0f, 0, 0));
        triggers.Add(new YWaitForSeconds(1));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(1f, 1, 1));
        triggers.Add(new YWaitForSeconds(1));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(2f, 2, 2));
        triggers.Add(new YWaitForSeconds(1));
        triggers.AddRange(GetComponent<YTransform>().SetPosition(3f, 3, 3));

        yCoroutine = YCoroutines.GetCoroutine(new Vector2(300, 300), triggers.ToArray()); //new YCoroutine(triggers.ToArray());

        return new YGDObject[]
        {
            yCoroutine
        };
    }

    public override YTrigger[] Tick()
    {
        List<YTrigger> triggers = new List<YTrigger>();



        //triggers.Add(new ItemEdit(5001, true, ItemEdit.Operation.Equals, 100));
        //triggers.AddRange(YMath.Sqrt(5001, 5000));

        triggers.AddRange(GetComponent<YTransform>().SetState(1));
        triggers.AddRange(GetComponent<YTransform>().Rotate(1f,1f,0));



        triggers.Add(YInput.GetP1Left(YGameManager.Instance.GameobjectGroupsManager.SetCurrentGroup("2"), new YTrigger[0]));
        triggers.Add(YInput.GetP1Right(YGameManager.Instance.GameobjectGroupsManager.SetCurrentGroup("1"), new YTrigger[0]));


        return triggers.ToArray();
    }
}
