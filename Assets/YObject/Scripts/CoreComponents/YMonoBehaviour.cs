using System.Collections;
using UnityEngine;

public abstract class YMonoBehaviour : MonoBehaviour
{
    protected bool initialised;
    public void Uninit()
    {
        initialised = false;
    }
    public abstract YGDObject[] Init();
    public abstract YTrigger[] Begin();
    public abstract YTrigger[] Tick();
}