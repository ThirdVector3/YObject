using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeatCube : YMonoBehaviour
{
    private const bool gd = false;
    private const float SPEED = 10f;

    [SerializeField] private float time;
    [SerializeField] private bool left;
    [Range(-1,1)]
    [SerializeField] private int rotation;
    public override YTrigger[] Begin()
    {
        List<YTrigger> triggers = new List<YTrigger>();
        float distance = time * SPEED - transform.position.z + 4;
        triggers.Add(new ItemEdit(9999, true, ItemEdit.Operation.Equals, 1, YIDsManager.Instance.GetIdByName("Time.time"), true, 0, true, ItemEdit.Operation.Add));
        triggers.Add(new ItemEdit(9999, true, ItemEdit.Operation.Multiply, -SPEED));
        triggers.Add(new ItemEdit(9999, true, ItemEdit.Operation.Add, distance));
        triggers.AddRange(GetComponent<YTransform>().Translate(23,23,9999));

        if (gd)
        {
            if (rotation == 0)
                triggers.AddRange(GetComponent<YTransform>().SetRotation(0, -90f, -90));
            else if (rotation == 1)
                triggers.AddRange(GetComponent<YTransform>().SetRotation(0, -90f, -180));
            else
                triggers.AddRange(GetComponent<YTransform>().SetRotation(0, -90f, 0));
        }
        else
        {
            if (rotation == 0)
                triggers.AddRange(GetComponent<YTransform>().SetRotation(-90f, 0, -90));
            else if (rotation == 1)
                triggers.AddRange(GetComponent<YTransform>().SetRotation(0, 90f, -180));
            else
                triggers.AddRange(GetComponent<YTransform>().SetRotation(0, -90f, 0));
        }

        return triggers.ToArray();
    }

    public override YGDObject[] Init()
    {
        alreadyDone = YGameManager.Instance.IDsManager.GetFreeIdInt();
        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".BeatCube.alreadyDone", alreadyDone, false);

        GetComponent<YTransform>().Init();
        CreateHitCoroutine();
        return new YGDObject[] { hitCoroutine };
    }

    public override YTrigger[] Tick()
    {
        List<YTrigger> triggers = new List<YTrigger>();
        triggers.Add(new ItemEdit(9999, true, ItemEdit.Operation.Equals, -SPEED));
        triggers.Add(new ItemEdit(9999, true, ItemEdit.Operation.Multiply, 1, YIDsManager.Instance.GetIdByName("Time.deltaTime"), true, 0, true, ItemEdit.Operation.Add));
        triggers.AddRange(GetComponent<YTransform>().Translate(23, 23, 9999));


        List<YTrigger> triggers2 = new List<YTrigger>();

        if (left)
        {
            triggers2.Add(new ItemEdit(9999, true, ItemEdit.Operation.Equals, 1, YIDsManager.Instance.GetIdByName("Input.P1Left"), true, 0, true, ItemEdit.Operation.Add));
            triggers2.Add(new ItemEdit(9999, true, ItemEdit.Operation.Add, 1, YIDsManager.Instance.GetIdByName("Input.P1Right"), true, 0, true, ItemEdit.Operation.Add));
            triggers2.Add(new ItemEdit(9999, true, ItemEdit.Operation.Add, 1, YIDsManager.Instance.GetIdByName("Input.P1Up"), true, 0, true, ItemEdit.Operation.Add));
            if (rotation == -1)
                triggers2.Add(new ItemCompare(9999, 0, true, true, 1, 1, ItemCompare.Operation.Equals,
                    new YTrigger[] { YInput.GetP1Left(new YTrigger[] { new ItemCompare(alreadyDone, 0, false, false, 1, 0, ItemCompare.Operation.Equals, new YTrigger[] { new ItemEdit(alreadyDone, false, ItemEdit.Operation.Equals, 1), YCoroutines.StartCoroutine(hitCoroutine) }, new YTrigger[0]) }, new YTrigger[] { new ItemCompare(alreadyDone, 0, false, false, 1, 0, ItemCompare.Operation.Equals, new YTrigger[] { new ItemEdit(alreadyDone, false, ItemEdit.Operation.Equals, 1), new ItemEdit(YIDsManager.Instance.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Add, 10) }, new YTrigger[0]) }) },
                    new YTrigger[0])); //new YTrigger[] { new ItemCompare(alreadyDone, 0, false, false, 1, 0, ItemCompare.Operation.Equals, new YTrigger[] { new ItemEdit(alreadyDone, false, ItemEdit.Operation.Equals, 1), new ItemEdit(YIDsManager.Instance.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Add, 10) }, new YTrigger[0]) }));
            else if (rotation == 0)
                print("s");
            else
                print("s");
        }
        else
        {
        
        }
        

        triggers.Add(new ItemEdit(9999, true, ItemEdit.Operation.Equals, 5.5f));
        triggers.Add(new ItemEdit(9998, true, ItemEdit.Operation.Equals, 2));
        triggers.Add(new ItemCompare(YIDsManager.Instance.GetIdByName(gameObject.name + ".transform.position.z"), 9999, true, true, 1, 1, ItemCompare.Operation.Less, 
            new YTrigger[] { new ItemCompare(YIDsManager.Instance.GetIdByName(gameObject.name + ".transform.position.z"), 9998, true, true, 1, 1, ItemCompare.Operation.More,
            triggers2.ToArray(), new YTrigger[]{ new ItemCompare(alreadyDone, 0, false, false, 1, 0, ItemCompare.Operation.Equals, new YTrigger[] { new ItemEdit(alreadyDone,false, ItemEdit.Operation.Equals, 1), new ItemEdit(YIDsManager.Instance.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Add, 10) }, new YTrigger[0]) } )}, new YTrigger[0]));

        return triggers.ToArray();
    }

    private int alreadyDone;
    private Coroutine hitCoroutine;
    private void CreateHitCoroutine()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        float adder = 0;

        triggers.Add(new YWaitForSeconds(0.17f));

        for (int i = 0; i < 10; i++)
        {
            triggers.AddRange(GetComponent<YTransform>().Translate(0, -0.05f * adder, 0));

            triggers.Add(new YWaitForSeconds(0.015f));
            adder += 1;
        }
        Coroutine coroutine = YCoroutines.GetCoroutine(new Vector2(500, 300), triggers.ToArray());
        hitCoroutine = coroutine;
    }
}
