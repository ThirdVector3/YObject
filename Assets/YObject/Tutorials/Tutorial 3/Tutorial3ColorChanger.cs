using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3ColorChanger : YMonoBehaviour
{
    public override void Begin()
    {
        new ColorTrigger(6, 2, Color.white);
        new ColorTrigger(7, 2, new Color(0.24f, 0.9f, 0.1f));
        new ColorTrigger(8, 2, Color.black);
    }
}
