using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeatCube : YMonoBehaviour
{
    private const float SPEED = 13f;

    [SerializeField] public float time;
    [SerializeField] private bool left;
    [Range(-1,1)]
    [SerializeField] public int rotation;

    public override void Begin()
    {
        //List<YTrigger> triggers = new List<YTrigger>();

        

        new ItemEdit(alreadyDone, false, ItemEdit.Operation.Equals, 0);

        float distance = time * SPEED - transform.position.z + 4;
        new ItemEdit(9999, true, ItemEdit.Operation.Equals, 1, YIDsManager.Instance.GetIdByName("Time.time"), true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(9999, true, ItemEdit.Operation.Multiply, -SPEED);
        new ItemEdit(9999, true, ItemEdit.Operation.Add, distance);
        GetComponent<YTransform>().Translate(23,23,9999);

        //if (gd)
        //{
        //    if (rotation == 0)
        //        GetComponent<YTransform>().SetRotation(0, -90f, -90);
        //    else if (rotation == 1)
        //        GetComponent<YTransform>().SetRotation(0, -90f, -180);
        //    else
        //        GetComponent<YTransform>().SetRotation(0, -90f, 0);
        //}
        //else
        //{
        //
        //}

        if (rotation == 0)
            GetComponent<YTransform>().SetRotation(-90f, 0, -90);
        else if (rotation == 1)
            GetComponent<YTransform>().SetRotation(0, 90f, -180);
        else
            GetComponent<YTransform>().SetRotation(0, -90f, 0);

        //return triggers.ToArray();
    }

    public override YGDObject[] Init()
    {
        alreadyDone = YGameManager.Instance.IDsManager.GetFreeIdInt();
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".BeatCube.alreadyDone", alreadyDone, false);

        GetComponent<YTransform>().Init();
        CreateHitCoroutine();
        return new YGDObject[] { hitCoroutine };
    }

    public override void Tick()
    {
        //List<YTrigger> triggers = new List<YTrigger>();


        new ItemEdit(9999, true, ItemEdit.Operation.Equals, -SPEED);
        new ItemEdit(9999, true, ItemEdit.Operation.Multiply, 1, YIDsManager.Instance.GetIdByName("Time.deltaTime"), true, 0, true, ItemEdit.Operation.Add);
        GetComponent<YTransform>().Translate(23, 23, 9999);


        List<YTrigger> triggers2 = new List<YTrigger>();

        //triggers2.Add(new ColorTrigger(1, 0, Color.black));

        YTrigger[] triggers3 = new YTrigger[] { new ItemCompare(alreadyDone, 0, false, false, 1, 0, ItemCompare.Operation.Equals, new YTrigger[] { new ItemEdit(alreadyDone, false, ItemEdit.Operation.Equals, 1), YCoroutines.StartCoroutine(hitCoroutine), new Spawn(434, false, 0, new Dictionary<int, int>()) }, new YTrigger[0]) };
        YTrigger[] triggers4 = new YTrigger[] { new ItemCompare(alreadyDone, 0, false, false, 1, 0, ItemCompare.Operation.Equals, new YTrigger[] { new ItemEdit(alreadyDone, false, ItemEdit.Operation.Equals, 1), new ItemEdit(YIDsManager.Instance.GetIdByName("BeatSaberGameManager.misses"),false, ItemEdit.Operation.Add, 1), new ItemEdit(YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Add, 0), new Spawn(433, false, 0, new Dictionary<int, int>()) }, new YTrigger[0]) };


        if (left)
        {
            triggers2.Add(new ItemEdit(9999, true, ItemEdit.Operation.Equals, 1, YIDsManager.Instance.GetIdByName("Input.P1Left"), true, 0, true, ItemEdit.Operation.Add));
            triggers2.Add(new ItemEdit(9999, true, ItemEdit.Operation.Add, 1, YIDsManager.Instance.GetIdByName("Input.P1Right"), true, 0, true, ItemEdit.Operation.Add));
            triggers2.Add(new ItemEdit(9999, true, ItemEdit.Operation.Add, 1, YIDsManager.Instance.GetIdByName("Input.P1Up"), true, 0, true, ItemEdit.Operation.Add));

            triggers2.Add(new ItemEdit(5000, true, ItemEdit.Operation.Equals, 1, 9999, true, 0, true, ItemEdit.Operation.Add));

            if (rotation == -1)
                triggers2.Add(new ItemCompare(9999, 0, true, true, 1, 1, ItemCompare.Operation.Equals,
                    new YTrigger[] { YInput.GetP1Left(triggers3, triggers4) },
                    new YTrigger[0])); //new YTrigger[] { new ItemCompare(alreadyDone, 0, false, false, 1, 0, ItemCompare.Operation.Equals, new YTrigger[] { new ItemEdit(alreadyDone, false, ItemEdit.Operation.Equals, 1), new ItemEdit(YIDsManager.Instance.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Add, 10) }, new YTrigger[0]) }));
            else if (rotation == 0)
                triggers2.Add(new ItemCompare(9999, 0, true, true, 1, 1, ItemCompare.Operation.Equals,
                    new YTrigger[] { YInput.GetP1Up(triggers3, triggers4) },
                    new YTrigger[0]));
            else
                triggers2.Add(new ItemCompare(9999, 0, true, true, 1, 1, ItemCompare.Operation.Equals,
                    new YTrigger[] { YInput.GetP1Right(triggers3, triggers4) },
                    new YTrigger[0]));
        }
        else
        {
            triggers2.Add(new ItemEdit(9999, true, ItemEdit.Operation.Equals, 1, YIDsManager.Instance.GetIdByName("Input.P2Left"), true, 0, true, ItemEdit.Operation.Add));
            triggers2.Add(new ItemEdit(9999, true, ItemEdit.Operation.Add, 1, YIDsManager.Instance.GetIdByName("Input.P2Right"), true, 0, true, ItemEdit.Operation.Add));
            triggers2.Add(new ItemEdit(9999, true, ItemEdit.Operation.Add, 1, YIDsManager.Instance.GetIdByName("Input.P2Up"), true, 0, true, ItemEdit.Operation.Add));

            if (rotation == -1)
                triggers2.Add(new ItemCompare(9999, 0, true, true, 1, 1, ItemCompare.Operation.Equals,
                    new YTrigger[] { YInput.GetP2Left(triggers3, triggers4) },
                    new YTrigger[0])); //new YTrigger[] { new ItemCompare(alreadyDone, 0, false, false, 1, 0, ItemCompare.Operation.Equals, new YTrigger[] { new ItemEdit(alreadyDone, false, ItemEdit.Operation.Equals, 1), new ItemEdit(YIDsManager.Instance.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Add, 10) }, new YTrigger[0]) }));
            else if (rotation == 0)
                triggers2.Add(new ItemCompare(9999, 0, true, true, 1, 1, ItemCompare.Operation.Equals,
                    new YTrigger[] { YInput.GetP2Up(triggers3, triggers4) },
                    new YTrigger[0]));
            else
                triggers2.Add(new ItemCompare(9999, 0, true, true, 1, 1, ItemCompare.Operation.Equals,
                    new YTrigger[] { YInput.GetP2Right(triggers3, triggers4) },
                    new YTrigger[0]));
        }

        //foreach (var trigger in triggers4)
        //{
        //    trigger.isFirstLevel = false;
        //}
        //foreach (var trigger in triggers3)
        //{
        //    trigger.isFirstLevel = false;
        //}
        //foreach (var trigger in triggers2)
        //{
        //    trigger.isFirstLevel = false;
        //}

        new ItemEdit(9999, true, ItemEdit.Operation.Equals, 5.5f);
        new ItemEdit(9998, true, ItemEdit.Operation.Equals, 2);
        new ItemCompare(YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), 9999, true, true, 1, 1, ItemCompare.Operation.Less,
            new YTrigger[] { new ItemCompare(YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), 9998, true, true, 1, 1, ItemCompare.Operation.More,
            /*new YTrigger[]{ new ItemEdit(5000, true, ItemEdit.Operation.Add, 1) }*/  triggers2 , new YTrigger[]{ new ItemCompare(alreadyDone, 0, false, false, 1, 0, ItemCompare.Operation.Equals, new YTrigger[] { new ItemEdit(alreadyDone,false, ItemEdit.Operation.Equals, 1), new ItemEdit(YIDsManager.Instance.GetIdByName("BeatSaberGameManager.misses"), false, ItemEdit.Operation.Add, 1), new ItemEdit(YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Add, 0), new Spawn(433, false, 0, new Dictionary<int, int>()) }, new YTrigger[0]) } )}, new YTrigger[0]);


        //return triggers.ToArray();
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
        Coroutine coroutine = YCoroutines.GetCoroutine(triggers.ToArray());
        hitCoroutine = coroutine;
    }
}
