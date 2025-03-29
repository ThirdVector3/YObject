using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YMainCamera : MonoBehaviour
{
    private static YMainCamera _instance;
    public static YMainCamera Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindAnyObjectByType<YMainCamera>();
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

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
        YGameManager.Instance.SetMemoryValueByName("Camera.rotation.sin.x", Mathf.Sin(YGameManager.Instance.GetMemoryValueByName("Camera.rotation.x").Item2 * Mathf.Deg2Rad));
        YGameManager.Instance.SetMemoryValueByName("Camera.rotation.sin.y", Mathf.Sin(YGameManager.Instance.GetMemoryValueByName("Camera.rotation.y").Item2 * Mathf.Deg2Rad));
        YGameManager.Instance.SetMemoryValueByName("Camera.rotation.sin.z", Mathf.Sin(YGameManager.Instance.GetMemoryValueByName("Camera.rotation.z").Item2 * Mathf.Deg2Rad));

        YGameManager.Instance.SetMemoryValueByName("Camera.rotation.cos.x", Mathf.Cos(YGameManager.Instance.GetMemoryValueByName("Camera.rotation.x").Item2 * Mathf.Deg2Rad));
        YGameManager.Instance.SetMemoryValueByName("Camera.rotation.cos.y", Mathf.Cos(YGameManager.Instance.GetMemoryValueByName("Camera.rotation.y").Item2 * Mathf.Deg2Rad));
        YGameManager.Instance.SetMemoryValueByName("Camera.rotation.cos.z", Mathf.Cos(YGameManager.Instance.GetMemoryValueByName("Camera.rotation.z").Item2 * Mathf.Deg2Rad));
    }

    public YTrigger[] SetPosition(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetPosition(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Equals, z)
        };
        return result;
    }
    public YTrigger[] GetPosition(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName("Camera.position.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName("Camera.position.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName("Camera.position.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Translate(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Add, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Add, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Add, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Translate(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Add, x),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Add, y),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Add, z)
        };
        return result;
    }
    public YTrigger[] SetRotation(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.rotation.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.rotation.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.rotation.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetRotation(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.rotation.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.rotation.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.rotation.z"), true, ItemEdit.Operation.Equals, z)
        };
        return result;
    }
    public YTrigger[] GetRotation(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName("Camera.rotation.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName("Camera.rotation.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName("Camera.rotation.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Rotate(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.rotation.x"), true, ItemEdit.Operation.Add, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.rotation.y"), true, ItemEdit.Operation.Add, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.rotation.z"), true, ItemEdit.Operation.Add, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Rotate(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.rotation.x"), true, ItemEdit.Operation.Add, x),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.rotation.y"), true, ItemEdit.Operation.Add, y),
            new ItemEdit(YGameManager.Instance.GetIdByName("Camera.rotation.z"), true, ItemEdit.Operation.Add, z)
        };
        return result;
    }
    public YTrigger[] GetSin(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName("Camera.rotation.sin.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName("Camera.rotation.sin.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName("Camera.rotation.sin.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] GetCos(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName("Camera.rotation.cos.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName("Camera.rotation.cos.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName("Camera.rotation.cos.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
}
