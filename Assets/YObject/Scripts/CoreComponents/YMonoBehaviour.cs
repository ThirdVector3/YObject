using System.Collections;
using UnityEngine;

public abstract class YMonoBehaviour : MonoBehaviour
{
    protected bool initialised;
    public virtual void Uninit()
    {
        initialised = false;
    }
    public virtual YGDObject[] Init() { return null; }
    public virtual void Begin() {  }
    public virtual void Tick() {  }
}