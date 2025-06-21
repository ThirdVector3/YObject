using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3RandomMover : YMonoBehaviour
{
    public override void Tick()
    {
        new RandomTrigger(50f, GetComponent<YTransform>().Translate(0.1f, 0, 0), GetComponent<YTransform>().Translate(-0.1f, 0, 0)); 
    }
}
