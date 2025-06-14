using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YMainCamera : YMonoBehaviour
{
    private static YMainCamera _instance;

    private Camera cameraComponent;
    public static YMainCamera Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<YMainCamera>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;

        SetDefaultSettings();
    }

    private void OnValidate()
    {
        SetDefaultSettings();
    }

    private void SetDefaultSettings()
    {
        cameraComponent = GetComponent<Camera>();
        cameraComponent.usePhysicalProperties = true;
        cameraComponent.sensorSize = new Vector2(16, 9);
    }

    void Update()
    {
        transform.position = new Vector3(
            YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.position.x").Item2,
            YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.position.y").Item2,
            YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.position.z").Item2
        );
        transform.eulerAngles = -new Vector3(
            YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.x").Item2,
            YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.y").Item2,
            YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.z").Item2
        );
        cameraComponent.focalLength = YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.focalLen").Item2;

        YGameManager.Instance.IDsManager.SetMemoryValueByName("Camera.rotation.sin.x", Mathf.Sin(YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.x").Item2 * Mathf.Deg2Rad));
        YGameManager.Instance.IDsManager.SetMemoryValueByName("Camera.rotation.sin.y", Mathf.Sin(YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.y").Item2 * Mathf.Deg2Rad));
        YGameManager.Instance.IDsManager.SetMemoryValueByName("Camera.rotation.sin.z", Mathf.Sin(YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.z").Item2 * Mathf.Deg2Rad));

        YGameManager.Instance.IDsManager.SetMemoryValueByName("Camera.rotation.cos.x", Mathf.Cos(YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.x").Item2 * Mathf.Deg2Rad));
        YGameManager.Instance.IDsManager.SetMemoryValueByName("Camera.rotation.cos.y", Mathf.Cos(YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.y").Item2 * Mathf.Deg2Rad));
        YGameManager.Instance.IDsManager.SetMemoryValueByName("Camera.rotation.cos.z", Mathf.Cos(YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.z").Item2 * Mathf.Deg2Rad));
    }

    public override void Begin()
    {
        SetDefaultSettings();
        cameraComponent = GetComponent<Camera>();
        List<YTrigger> triggers = new List<YTrigger>()
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Equals, transform.position.x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Equals, transform.position.y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Equals, transform.position.z),

            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.x"), true, ItemEdit.Operation.Equals, -transform.eulerAngles.x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.y"), true, ItemEdit.Operation.Equals, -transform.eulerAngles.y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.z"), true, ItemEdit.Operation.Equals, -transform.eulerAngles.z),

            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.focalLen"), true, ItemEdit.Operation.Equals, cameraComponent.focalLength),
        };

        //return triggers.ToArray();
    }



    public YTrigger SetFocalLen(float focalLen)
    {
        return new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.focalLen"), true, ItemEdit.Operation.Equals, focalLen);
    }
    public YTrigger SetFocalLen(int idIn)
    {
        return new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.focalLen"), true, ItemEdit.Operation.Equals, 1, idIn, true, 0, true, ItemEdit.Operation.Add);
    }
    public YTrigger GetFocalLen(int idOut)
    {
        return new ItemEdit(idOut, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.focalLen"), true, 0, true, ItemEdit.Operation.Add);
    }
    public YTrigger[] SetPosition(int idInX, int idInY, int idInZ)
    {
        if (idInX == 0)
            idInX = 23;
        if (idInY == 0)
            idInY = 23;
        if (idInZ == 0)
            idInZ = 23;
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetPosition(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Equals, z)
        };
        return result;
    }
    public YTrigger[] GetPosition(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Translate(int idInX, int idInY, int idInZ)
    {
        if (idInX == 0)
            idInX = 23;
        if (idInY == 0)
            idInY = 23;
        if (idInZ == 0)
            idInZ = 23;
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Add, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Add, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Add, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Translate(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Add, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Add, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Add, z)
        };
        return result;
    }
    public YTrigger[] TranslateLocal(int idInX, int idInY, int idInZ)
    {
        if (idInX == 0)
            idInX = 23;
        if (idInY == 0)
            idInY = 23;
        if (idInZ == 0)
            idInZ = 23;
        YGameManager.Instance.RecordPool();

        YVariable sinX = new YVariable("Camera.rotation.sin.x");
        YVariable sinY = new YVariable("Camera.rotation.sin.y");
        YVariable sinZ = new YVariable("Camera.rotation.sin.z");
        YVariable cosX = new YVariable("Camera.rotation.cos.x");
        YVariable cosY = new YVariable("Camera.rotation.cos.y");
        YVariable cosZ = new YVariable("Camera.rotation.cos.z");

        YVariable curX = new YVariable("Camera.position.x");
        YVariable curY = new YVariable("Camera.position.y");
        YVariable curZ = new YVariable("Camera.position.z");

        YVariable X = new YFloat(0);
        YVariable Y = new YFloat(0);
        YVariable Z = new YFloat(0);

        X += new YVariable(idInX, true) * cosY;
        //Y += 0;
        Z += new YVariable(idInX, true) * sinY;

        X += new YVariable(idInY, true) * sinY * sinX;
        Y += new YVariable(idInY, true) * cosX;
        Z += new YVariable(idInY, true) * cosY * sinX;

        X += new YVariable(idInZ, true) * sinY * cosX * -1;
        Y += new YVariable(idInZ, true) * sinX;
        Z += new YVariable(idInZ, true) * cosY * cosX;


        curX += X;
        curY += Y;
        curZ += Z;

        return YGameManager.Instance.StopRecordPool();
    }
    public YTrigger[] TranslateLocal(float x, float y, float z)
    {
        YGameManager.Instance.RecordPool();

        YVariable sinX = new YVariable("Camera.rotation.sin.x");
        YVariable sinY = new YVariable("Camera.rotation.sin.y");
        YVariable sinZ = new YVariable("Camera.rotation.sin.z");
        YVariable cosX = new YVariable("Camera.rotation.cos.x");
        YVariable cosY = new YVariable("Camera.rotation.cos.y");
        YVariable cosZ = new YVariable("Camera.rotation.cos.z");

        YVariable curX = new YVariable("Camera.position.x");
        YVariable curY = new YVariable("Camera.position.y");
        YVariable curZ = new YVariable("Camera.position.z");

        YVariable X = new YFloat(0);
        YVariable Y = new YFloat(0);
        YVariable Z = new YFloat(0);


        X += new YFloat(x) * cosY;
        //Y += 0;
        Z += new YFloat(x) * sinY;

        X += new YFloat(y) * sinY * sinX;
        Y += new YFloat(y) * cosX;
        Z += new YFloat(y) * cosY * sinX;

        X += new YFloat(z) * sinY * cosX * -1;
        Y += new YFloat(z) * sinX;
        Z += new YFloat(z) * cosY * cosX;


        curX += X;
        curY += Y;
        curZ += Z;

        return YGameManager.Instance.StopRecordPool();
    }
    public YTrigger[] SetRotation(int idInX, int idInY, int idInZ)
    {
        if (idInX == 0)
            idInX = 23;
        if (idInY == 0)
            idInY = 23;
        if (idInZ == 0)
            idInZ = 23;
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.x"), true, ItemEdit.Operation.Equals, -1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.y"), true, ItemEdit.Operation.Equals, -1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.z"), true, ItemEdit.Operation.Equals, -1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetRotation(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.x"), true, ItemEdit.Operation.Equals, -x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.y"), true, ItemEdit.Operation.Equals, -y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.z"), true, ItemEdit.Operation.Equals, -z)
        };
        return result;
    }
    public YTrigger[] GetRotation(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Rotate(int idInX, int idInY, int idInZ)
    {
        if (idInX == 0)
            idInX = 23;
        if (idInY == 0)
            idInY = 23;
        if (idInZ == 0)
            idInZ = 23;
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.x"), true, ItemEdit.Operation.Add, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.y"), true, ItemEdit.Operation.Add, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.z"), true, ItemEdit.Operation.Add, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Rotate(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.x"), true, ItemEdit.Operation.Add, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.y"), true, ItemEdit.Operation.Add, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.z"), true, ItemEdit.Operation.Add, z)
        };
        return result;
    }
    public YTrigger[] GetSin(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.sin.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.sin.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.sin.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] GetCos(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.cos.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.cos.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.cos.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }

}
