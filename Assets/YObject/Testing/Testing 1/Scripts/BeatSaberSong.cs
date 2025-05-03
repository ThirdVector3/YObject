using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSaberSong : YMonoBehaviour
{
    public override YTrigger[] Begin()
    {
        return new YTrigger[] { new SongTrigger(63, 0, 1, false, 0, 0, 0, 0) };
    }

    public override YGDObject[] Init()
    {
        return null;
    }

    public override YTrigger[] Tick()
    {
        return null;
    }
}
