using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class YCoroutines
{
    public static CoroutineTrigger GetCoroutine(Vector2 pos, YTrigger[] triggers)
    {
        if (pos == Vector2.zero)
            pos = new Vector2(1, 0);

        var yCoroutine = new CoroutineTrigger(triggers.ToArray());
        yCoroutine.pos = pos;

        return yCoroutine;
    } 
    public static YTrigger StartCoroutine(CoroutineTrigger coroutine)
    {
        return new StartCoroutineTrigger(coroutine);
    }
}