using System;
using UnityEngine;

public class DebugLog : YTrigger
{
    private string text = null;
    private Func<object> func = null;
    private YVariable yVariable = null;


    public DebugLog(string text) : base()
    {
        this.text = text;
    }
    public DebugLog(Func<object> func) : base()
    {
        this.func = func;
    }
    public DebugLog(YVariable variable) : base()
    {
        this.yVariable = variable;
    }

    public override void Activate()
    {
        if ((object)yVariable != null)
        {
            if (yVariable.IsFloat())
                Debug.Log(YIDsManager.Instance.GetMemoryValue(yVariable).Item2);
            else
                Debug.Log(YIDsManager.Instance.GetMemoryValue(yVariable).Item1);
        }
        else if (func != null)
        {
            Debug.Log(func());
        }
        else
            Debug.Log(text);
    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        return "";
    }
}