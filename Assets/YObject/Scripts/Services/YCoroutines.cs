using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class YCoroutines
{
    private static Vector2 pos = Vector2.zero;
    public static void Init()
    {
        pos = new Vector2(300, 300);
    }
    public static Coroutine GetCoroutine(YTrigger[] triggers)
    {
        //if (pos == Vector2.zero)
        //    pos = new Vector2(1, 0);

        var yCoroutine = new Coroutine(triggers.ToArray());
        yCoroutine.pos = pos;

        pos += new Vector2(0, 10);

        return yCoroutine;
    } 
    public static YTrigger StartCoroutine(Coroutine coroutine)
    {
        return new StartCoroutine(coroutine);
    }
}