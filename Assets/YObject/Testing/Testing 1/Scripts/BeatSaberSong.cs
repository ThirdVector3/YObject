using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSaberSong : YMonoBehaviour
{
    public override void Begin()
    {
        //return
        new SongTrigger(778356, 0, 1, false, 0, 0, 1000, 0);
    }

}
