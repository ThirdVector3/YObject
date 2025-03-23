using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YMainCamera : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(
            YGameManager.Instance.GetMemoryValueByName("Camera.position.x").Item2,
            YGameManager.Instance.GetMemoryValueByName("Camera.position.y").Item2,
            YGameManager.Instance.GetMemoryValueByName("Camera.position.z").Item2
        );
        transform.eulerAngles = new Vector3(
            YGameManager.Instance.GetMemoryValueByName("Camera.rotation.x").Item2,
            YGameManager.Instance.GetMemoryValueByName("Camera.rotation.y").Item2,
            YGameManager.Instance.GetMemoryValueByName("Camera.rotation.z").Item2
        );
    }
}
