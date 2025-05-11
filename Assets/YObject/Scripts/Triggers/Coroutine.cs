
using GeometryDashAPI.Levels.GameObjects.Default;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Coroutine : YTrigger
{
    private YTrigger[] triggers;
    public int group;

    public Coroutine(YTrigger[] triggers) : base()
    {
        this.triggers = triggers;
        group = YGameManager.Instance.IDsManager.GetFreeGroup();
        YGameManager.Instance.IDsManager.AddGroup(group);
    }

    public override void Activate()
    {
        YGameManager.Instance.StartCoroutine(IECoroutine());
    }
    private IEnumerator IECoroutine()
    {
        foreach (var trigger in triggers)
        {
            if (trigger == null) continue;
            if (trigger is YWaitForSeconds)
            {
                yield return new WaitForSeconds(((YWaitForSeconds)trigger).seconds);
                continue;
            }
            trigger.Activate();
        }
    }
    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        string ret = "";


        float xPos = this.pos.x;
        float xPosA = xPos;
        //List<YGDObject> objs = new List<YGDObject>();

        var t = new Spawn(0, false, 0, new Dictionary<int, int>());
        t.pos = new Vector2(xPos, this.pos.y);
        t.AddGroup(group);
        for (int i = 0; i < this.groups.Length; i++)
        {
            if (i == 0) continue;
            t.AddGroup(this.groups[i]);
        }
        ret += t.GetString(t.pos);

        foreach (var trigger in triggers)
        {
            if (trigger == null) continue;

            if (trigger is YWaitForSeconds)
            {
                xPosA += 311 * ((YWaitForSeconds)trigger).seconds;
                xPos = xPosA;
                continue;
            }
            trigger.pos = new Vector2(xPos, this.pos.y);
            trigger.AddGroup(group);

            for (int i = 0; i < this.groups.Length; i++)
            {
                if (i == 0) continue;
                trigger.AddGroup(this.groups[i]);
            }
            
            //objs.Add(trigger);

            ret += trigger.GetString(trigger.pos);

            xPos += 1;
        }

        return ret;
    }
}
