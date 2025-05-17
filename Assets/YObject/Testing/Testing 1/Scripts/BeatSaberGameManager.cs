using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSaberGameManager : YMonoBehaviour
{
    private int misses;
    private int gameover;

    public override YGDObject[] Init()
    {
        misses = YGameManager.Instance.IDsManager.GetFreeIdInt();
        YGameManager.Instance.IDsManager.AddVariable("BeatSaberGameManager.misses", misses, false);
        gameover = YGameManager.Instance.IDsManager.GetFreeIdInt();
        YGameManager.Instance.IDsManager.AddVariable("BeatSaberGameManager.gameover", gameover, false);
        return null;
    }

    public override void Tick()
    {
        //return
        CheckGameover();
        //YInput.GetP1Up(YGameobjectGroupsManager.Instance.SetCurrentGroup(null), YGameobjectGroupsManager.Instance.SetCurrentGroup("2"));
        //YMainCamera.Instance.SetFocalLen(YIDsManager.Instance.GetIdByName("Time.time"));
    }

    public override void Begin()
    {
        YGameobjectGroupsManager.Instance.SetCurrentGroup("2");
    }

    public YTrigger[] CheckGameover()
    {
        YTrigger[] triggers = new YTrigger[] { new ItemCompare(misses, 0, false, false, 1, 2, ItemCompare.Operation.More, new YTrigger[] { new ItemCompare(gameover, 0, false, false, 1, 0, ItemCompare.Operation.Equals, new YTrigger[] { new Toggle(432, true), new ItemEdit(gameover, false, ItemEdit.Operation.Equals, 1) }, new YTrigger[0]) }, new YTrigger[0]) };

        return triggers;
    }
}
