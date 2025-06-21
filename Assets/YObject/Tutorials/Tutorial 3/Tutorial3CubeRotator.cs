using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3CubeRotator : YMonoBehaviour
{
    public override void Tick()
    {
        YVariable rotation = new YFloat(50) * new YVariable("Time.deltaTime");
        GetComponent<YTransform>().Rotate(0, rotation, 0);
    }
}
