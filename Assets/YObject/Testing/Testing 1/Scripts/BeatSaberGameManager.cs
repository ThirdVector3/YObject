using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSaberGameManager : YMonoBehaviour
{
    private int misses;
    private int gameover;
    private int state;
    public override YGDObject[] Init()
    {
        misses = YGameManager.Instance.IDsManager.GetFreeIdInt();
        YGameManager.Instance.IDsManager.AddVariable("BeatSaberGameManager.misses", misses, false);
        gameover = YGameManager.Instance.IDsManager.GetFreeIdInt();
        YGameManager.Instance.IDsManager.AddVariable("BeatSaberGameManager.gameover", gameover, false);
        state = YGameManager.Instance.IDsManager.AddVariable("BeatSaberGameManager.state", YGameManager.Instance.IDsManager.GetFreeIdInt(), false);
        CreateEndCoroutine();

        return new YGDObject[] { endCoroutine };
    }

    public override void Tick()
    {
        //return
        CheckGameover();
        //YInput.GetP1Up(YGameobjectGroupsManager.Instance.SetCurrentGroup(null), YGameobjectGroupsManager.Instance.SetCurrentGroup("2"));
        //YMainCamera.Instance.SetFocalLen(YIDsManager.Instance.GetIdByName("Time.time"));

        //YGameobjectGroupsManager.Instance.SetCurrentGroup("2");

        //YInput.GetP1Up(new YTrigger[] { YGameobjectGroupsManager.Instance.SetCurrentGroup("2") }, new YTrigger[] { YGameobjectGroupsManager.Instance.SetCurrentGroup("3") });

        //new ItemCompare(state, 0, false, false, 1, 0, ItemCompare.Operation.Equals, 
        //new YTrigger[]
        //{
        //    new ItemCompare(YIDsManager.Instance.GetIdByName("Time.time"), 0, true, false, 1, 19, ItemCompare.Operation.More,
        //    new YTrigger[]
        //    {
        //        new ItemEdit(state, false, ItemEdit.Operation.Equals, 1),
        //        YGameobjectGroupsManager.Instance.SetCurrentGroup("3")
        //    },
        //    new YTrigger[0]
        //    )
        //},
        //new YTrigger[]
        //{
        //    new ItemCompare(state, 0, false, false, 1, 1, ItemCompare.Operation.Equals,
        //    new YTrigger[]
        //    {
        //        new ItemCompare(YIDsManager.Instance.GetIdByName("Time.time"), 0, true, false, 1, 38, ItemCompare.Operation.More,
        //        new YTrigger[]
        //        {
        //            new ItemEdit(state, false, ItemEdit.Operation.Equals, 2),
        //            YGameobjectGroupsManager.Instance.SetCurrentGroup("4")
        //        },
        //        new YTrigger[0]
        //        )
        //    },
        //    new YTrigger[0]
        //    )
        //}
        //);
    }

    public override void Begin()
    {
        YCoroutines.StartCoroutine(endCoroutine);
        YGameobjectGroupsManager.Instance.SetCurrentGroup("2");
    }

    public YTrigger[] CheckGameover()
    {
        YTrigger[] triggers = new YTrigger[] { new ItemCompare(misses, 0, false, false, 1, 2, ItemCompare.Operation.More, new YTrigger[] { new ItemCompare(gameover, 0, false, false, 1, 0, ItemCompare.Operation.Equals, new YTrigger[] { new Toggle(432, true), new ItemEdit(gameover, false, ItemEdit.Operation.Equals, 1) }, new YTrigger[0]) }, new YTrigger[0]) };

        return triggers;
    }

    private Coroutine endCoroutine;
    private void CreateEndCoroutine()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        //triggers.Add(YGameobjectGroupsManager.Instance.SetCurrentGroup("2"));

        triggers.Add(new YWaitForSeconds(19));

        //triggers.Add(new ItemEdit(9999, false, ItemEdit.Operation.Equals, 1, misses, false, 0, false, ItemEdit.Operation.Add));
        //triggers.Add(YGameobjectGroupsManager.Instance.SetCurrentGroup("3"));
        //triggers.Add(new ItemEdit(misses, false, ItemEdit.Operation.Equals, 1, 9999, false, 0, false, ItemEdit.Operation.Add));

        triggers.Add(new YWaitForSeconds(38 - 19));

        //triggers.Add(new ItemEdit(9999, false, ItemEdit.Operation.Equals, 1, misses, false, 0, false, ItemEdit.Operation.Add));
        //triggers.Add(YGameobjectGroupsManager.Instance.SetCurrentGroup("4"));
        //triggers.Add(new ItemEdit(misses, false, ItemEdit.Operation.Equals, 1, 9999, false, 0, false, ItemEdit.Operation.Add));

        triggers.Add(new YWaitForSeconds(55.5f - 19 - (38 - 19)));
        triggers.Add(new Spawn(435, false, 0, new Dictionary<int, int>()));

        Coroutine coroutine = YCoroutines.GetCoroutine(triggers.ToArray());
        endCoroutine = coroutine;
    }
}
