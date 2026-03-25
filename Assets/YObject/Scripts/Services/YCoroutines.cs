using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class YCoroutines
{
    private static Vector2 pos = Vector2.zero;
    private static int startOfRecording = 0;
    public static void Init()
    {
        pos = new Vector2(300, 300);
        startOfRecording = 0;
    }
    public static void RecordCoroutine()
    {
        //startOfRecording = YGameManager.Instance.globalPool.Count;

        YGameManager.Instance.RecordPool();
    }
    public static Coroutine GetCoroutine()
    {
        //List<YTrigger> triggers = YGameManager.Instance.globalPool.GetRange(startOfRecording, YGameManager.Instance.globalPool.Count - startOfRecording);
        var triggers = YGameManager.Instance.StopRecordPool(removeNonFirstLevel: true);

        var yCoroutine = new Coroutine(triggers);
        yCoroutine.pos = pos;

        pos += new Vector2(0, 10);

        return yCoroutine;
    }
    public static Coroutine GetCoroutine(YTrigger[] triggers)
    {
        //if (pos == Vector2.zero)
        //    pos = new Vector2(1, 0);
        List<YTrigger> firstLevelTriggers = new List<YTrigger>();

        foreach (var trigger in triggers)
        {
            if (trigger.isFirstLevel)
            {
                firstLevelTriggers.Add(trigger);
            }
        }

        var yCoroutine = new Coroutine(firstLevelTriggers.ToArray());
        yCoroutine.pos = pos;

        pos += new Vector2(0, 10);

        return yCoroutine;
    } 
    public static YTrigger StartCoroutine(Coroutine coroutine)
    {
        return new StartCoroutine(coroutine);
    }
}