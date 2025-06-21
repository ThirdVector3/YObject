using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3Manager : YMonoBehaviour
{
    public override void Tick()
    {
        //new SongTrigger(467339, 0, 1, false, 0, 0, 0, 0);

        new Condition(YGameManager.GetService<YInputService>().P2LeftDown())
            .Then(() =>
            {
                YGameobjectGroupsManager.Instance.SetCurrentGroup("first group");
            });

        new Condition(YGameManager.GetService<YInputService>().P2RightDown())
            .Then(() =>
            {
                YGameobjectGroupsManager.Instance.SetCurrentGroup("second group");
            });
    }
}
