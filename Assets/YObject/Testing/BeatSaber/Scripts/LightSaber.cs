using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : YMonoBehaviour
{
    [SerializeField] private bool left = true;


    public override YGDObject[] Init()
    {
        saberUpDoingId = YGameManager.Instance.IDsManager.GetFreeIdInt();
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".LightSaber.saberUpDoing", saberUpDoingId, false);
        saberLeftDoingId = YGameManager.Instance.IDsManager.GetFreeIdInt();
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".LightSaber.saberLeftDoing", saberLeftDoingId, false);
        saberRightDoingId = YGameManager.Instance.IDsManager.GetFreeIdInt();
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".LightSaber.saberRightDoing", saberRightDoingId, false);

        GetComponent<YTransform>().Init();
        SaberUp();
        SaberLeft();
        SaberRight();
        return new YGDObject[] { saberUp, saberLeft, saberRight };
    }

    public override void Tick()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        if (left)
        {
            new ItemCompare(saberUpDoingId, 0, false, false, 1, 0, ItemCompare.Operation.Equals,
                new YTrigger[] { YInput.GetP1Up(new YTrigger[] { YCoroutines.StartCoroutine(saberUp) }, new YTrigger[0]) },
                new YTrigger[0]
                );
            new ItemCompare(saberUpDoingId, 0, false, false, 1, 2, ItemCompare.Operation.Equals,
                new YTrigger[] { YInput.GetP1Up(new YTrigger[0], new YTrigger[] { new ItemEdit(saberUpDoingId, false, ItemEdit.Operation.Equals, 0) }) },
                new YTrigger[0]
                );

            new ItemCompare(saberLeftDoingId, 0, false, false, 1, 0, ItemCompare.Operation.Equals,
                new YTrigger[] { YInput.GetP1Left(new YTrigger[] { YCoroutines.StartCoroutine(saberLeft) }, new YTrigger[0]) },
                new YTrigger[0]
                );
            new ItemCompare(saberLeftDoingId, 0, false, false, 1, 2, ItemCompare.Operation.Equals,
                new YTrigger[] { YInput.GetP1Left(new YTrigger[0], new YTrigger[] { new ItemEdit(saberLeftDoingId, false, ItemEdit.Operation.Equals, 0) }) },
                new YTrigger[0]
                );

            new ItemCompare(saberRightDoingId, 0, false, false, 1, 0, ItemCompare.Operation.Equals,
                new YTrigger[] { YInput.GetP1Right(new YTrigger[] { YCoroutines.StartCoroutine(saberRight) }, new YTrigger[0]) },
                new YTrigger[0]
                );
            new ItemCompare(saberRightDoingId, 0, false, false, 1, 2, ItemCompare.Operation.Equals,
                new YTrigger[] { YInput.GetP1Right(new YTrigger[0], new YTrigger[] { new ItemEdit(saberRightDoingId, false, ItemEdit.Operation.Equals, 0) }) },
                new YTrigger[0]
                );
        }
        else
        {
            new ItemCompare(saberUpDoingId, 0, false, false, 1, 0, ItemCompare.Operation.Equals,
                new YTrigger[] { YInput.GetP2Up(new YTrigger[] { YCoroutines.StartCoroutine(saberUp) }, new YTrigger[0]) },
                new YTrigger[0]
                );
            new ItemCompare(saberUpDoingId, 0, false, false, 1, 2, ItemCompare.Operation.Equals,
                new YTrigger[] { YInput.GetP2Up(new YTrigger[0], new YTrigger[] { new ItemEdit(saberUpDoingId, false, ItemEdit.Operation.Equals, 0) }) },
                new YTrigger[0]
                );

            new ItemCompare(saberLeftDoingId, 0, false, false, 1, 0, ItemCompare.Operation.Equals,
                new YTrigger[] { YInput.GetP2Left(new YTrigger[] { YCoroutines.StartCoroutine(saberLeft) }, new YTrigger[0]) },
                new YTrigger[0]
                );
            new ItemCompare(saberLeftDoingId, 0, false, false, 1, 2, ItemCompare.Operation.Equals,
                new YTrigger[] { YInput.GetP2Left(new YTrigger[0], new YTrigger[] { new ItemEdit(saberLeftDoingId, false, ItemEdit.Operation.Equals, 0) }) },
                new YTrigger[0]
                );

            new ItemCompare(saberRightDoingId, 0, false, false, 1, 0, ItemCompare.Operation.Equals,
                new YTrigger[] { YInput.GetP2Right(new YTrigger[] { YCoroutines.StartCoroutine(saberRight) }, new YTrigger[0]) },
                new YTrigger[0]
                );
            new ItemCompare(saberRightDoingId, 0, false, false, 1, 2, ItemCompare.Operation.Equals,
                new YTrigger[] { YInput.GetP2Right(new YTrigger[0], new YTrigger[] { new ItemEdit(saberRightDoingId, false, ItemEdit.Operation.Equals, 0) }) },
                new YTrigger[0]
                );
        }

        //return triggers.ToArray();
    }

    private int saberUpDoingId;
    private Coroutine saberUp;
    public void SaberUp()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        triggers.Add(new ItemEdit(saberUpDoingId, false, ItemEdit.Operation.Equals, 1));

        float rot = 0;

        for (int i = 0; i < 15; i++)
        {
            triggers.AddRange(GetComponent<YTransform>().Rotate(2 * (rot - 5), 0, 0));

            triggers.AddRange(YMainCamera.Instance.Rotate(-(rot-5) / 10, 0, 0));

            triggers.Add(new YWaitForSeconds(0.015f));
            rot += 1;
        }
        rot = 0;
        for (int i = 0; i < 15; i++)
        {
            triggers.AddRange(GetComponent<YTransform>().Rotate(2 * (5 - rot), 0, 0));

            triggers.AddRange(YMainCamera.Instance.Rotate((rot - 5) / 10, 0, 0));

            triggers.Add(new YWaitForSeconds(0.015f));
            rot += 1;
        }
        if (left)
        {
            triggers.Add(YInput.GetP1Up(new YTrigger[] { new ItemEdit(saberUpDoingId, false, ItemEdit.Operation.Equals, 2) },
                new YTrigger[] { new ItemEdit(saberUpDoingId, false, ItemEdit.Operation.Equals, 0) }));
        }
        else
        {
            triggers.Add(YInput.GetP2Up(new YTrigger[] { new ItemEdit(saberUpDoingId, false, ItemEdit.Operation.Equals, 2) },
    new YTrigger[] { new ItemEdit(saberUpDoingId, false, ItemEdit.Operation.Equals, 0) }));
        }
        Coroutine coroutine = YCoroutines.GetCoroutine(triggers.ToArray()); //new YCoroutine(triggers.ToArray());
        saberUp = coroutine;
    }
    private int saberLeftDoingId;
    private Coroutine saberLeft;
    public void SaberLeft()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        triggers.Add(new ItemEdit(saberLeftDoingId, false, ItemEdit.Operation.Equals, 1));

        float rot = 0;

        for (int i = 0; i < 15; i++)
        {
            triggers.AddRange(GetComponent<YTransform>().Rotate(rot/3, 2 * (rot - 7), 0));
            if (left)
                triggers.AddRange(YMainCamera.Instance.Rotate(rot / 10, rot / 13, rot / 15));
            else
                triggers.AddRange(YMainCamera.Instance.Rotate(-rot / 10, rot / 13, rot / 15));
            triggers.Add(new YWaitForSeconds(0.015f));
            rot += 1;
        }
        rot = 0;
        for (int i = 0; i < 15; i++)
        {
            triggers.AddRange(GetComponent<YTransform>().Rotate(-rot/3, 2 * (7 - rot), 0));
            if (left)
                triggers.AddRange(YMainCamera.Instance.Rotate(-(14-rot) / 10, -(14 - rot) / 13, -(14 - rot) / 15));
            else
                triggers.AddRange(YMainCamera.Instance.Rotate((14 - rot) / 10, -(14 - rot) / 13, -(14 - rot) / 15));
            triggers.Add(new YWaitForSeconds(0.015f));
            rot += 1;
        }
        if (left)
        {
            triggers.Add(YInput.GetP1Left(new YTrigger[] { new ItemEdit(saberLeftDoingId, false, ItemEdit.Operation.Equals, 2) },
                new YTrigger[] { new ItemEdit(saberLeftDoingId, false, ItemEdit.Operation.Equals, 0) }));
        }
        else
        {
            triggers.Add(YInput.GetP2Left(new YTrigger[] { new ItemEdit(saberLeftDoingId, false, ItemEdit.Operation.Equals, 2) },
    new YTrigger[] { new ItemEdit(saberLeftDoingId, false, ItemEdit.Operation.Equals, 0) }));
        }

        Coroutine coroutine = YCoroutines.GetCoroutine(triggers.ToArray()); //new YCoroutine(triggers.ToArray());
        saberLeft = coroutine;
    }
    private int saberRightDoingId;
    private Coroutine saberRight;
    public void SaberRight()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        triggers.Add(new ItemEdit(saberRightDoingId, false, ItemEdit.Operation.Equals, 1));

        float rot = 0;

        for (int i = 0; i < 15; i++)
        {
            triggers.AddRange(GetComponent<YTransform>().Rotate(rot / 3, 2 * (7 - rot), 0));
            if (left)
                triggers.AddRange(YMainCamera.Instance.Rotate(-rot / 10, -rot / 13, -rot / 15));
            else
                triggers.AddRange(YMainCamera.Instance.Rotate(rot / 10, -rot / 13, -rot / 15));
            triggers.Add(new YWaitForSeconds(0.015f));
            rot += 1;
        }
        rot = 0;
        for (int i = 0; i < 15; i++)
        {
            triggers.AddRange(GetComponent<YTransform>().Rotate(-rot / 3, 2 * (rot - 7), 0));
            if (left)
                triggers.AddRange(YMainCamera.Instance.Rotate((14 - rot) / 10, (14 - rot) / 13, (14 - rot) / 15));
            else
                triggers.AddRange(YMainCamera.Instance.Rotate(-(14 - rot) / 10, (14 - rot) / 13, (14 - rot) / 15));
            triggers.Add(new YWaitForSeconds(0.015f));
            rot += 1;
        }
        if (left)
        {
            triggers.Add(YInput.GetP1Right(new YTrigger[] { new ItemEdit(saberRightDoingId, false, ItemEdit.Operation.Equals, 2) },
                new YTrigger[] { new ItemEdit(saberRightDoingId, false, ItemEdit.Operation.Equals, 0) }));
        }
        else
        {
            triggers.Add(YInput.GetP2Right(new YTrigger[] { new ItemEdit(saberRightDoingId, false, ItemEdit.Operation.Equals, 2) },
    new YTrigger[] { new ItemEdit(saberRightDoingId, false, ItemEdit.Operation.Equals, 0) }));
        }

        Coroutine coroutine = YCoroutines.GetCoroutine(triggers.ToArray()); //new YCoroutine(triggers.ToArray());
        saberRight = coroutine;
    }
}
