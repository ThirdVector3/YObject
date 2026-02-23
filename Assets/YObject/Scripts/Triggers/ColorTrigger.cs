using System.Collections;
using UnityEngine;

public class ColorTrigger : YTrigger
{
    private int id;
    private int copyId;
    private float duration;
    private UnityEngine.Color color;


    public ColorTrigger(int id, float duration, UnityEngine.Color color, int copyId = 0) : base()
    {
        this.id = id;
        this.duration = duration;
        this.color = color;
        this.copyId = copyId;
    }
    public override void Activate()
    {
        if (id >= 1000)
            return;
        YGameManager.Instance.StartCoroutine(IEActivate());
    }
    private IEnumerator IEActivate()
    {
        float startTime = Time.time;
        UnityEngine.Color color = YColorManager.GetColors()[id];

        while (Time.time < startTime + duration)
        {
            float t = ((Time.time - startTime) / duration);
            YColorManager.SetColor(id, UnityEngine.Color.Lerp(color, this.color, t));
            yield return new WaitForFixedUpdate();
        }
        YColorManager.SetColor(id, this.color);
    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        return $"1,899,2,{pos.Value.x},3,{pos.Value.y}{GetGroupsString(groups, groupsParent)},62,1,87,1,36,1,7,{Mathf.RoundToInt(color.r * 255)},8,{Mathf.RoundToInt(color.g * 255)},9,{Mathf.RoundToInt(color.b * 255)},10,{duration},35,1,23,{id}" + (copyId == 0 ? "" : $",50,{copyId},60,1,210,1") + ";";
    }
}