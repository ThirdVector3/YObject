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
    private YTransform _yTransform;
    public YTransform yTransform {
        get
        {
            if (_yTransform == null)
            {
                _yTransform = GetComponent<YTransform>();
                if (_yTransform == null)
                {
                    _yTransform = gameObject.AddComponent<YTransform>();
                }
            }
            return _yTransform;
        }
    }






    private YVariable focalLength;

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
        if (Application.isEditor)
            yTransform.SetState(true, false, true);
        cameraComponent = GetComponent<Camera>();
        cameraComponent.usePhysicalProperties = true;
        cameraComponent.sensorSize = new Vector2(16, 9);
    }

    void Update()
    {
        //transform.position = new Vector3(
        //    YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.position.x").Item2,
        //    YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.position.y").Item2,
        //    YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.position.z").Item2
        //);
        //transform.eulerAngles = -new Vector3(
        //    YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.x").Item2,
        //    YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.y").Item2,
        //    YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.z").Item2
        //);
        cameraComponent.focalLength = YGameManager.Instance.IDsManager.GetMemoryValue(focalLength).Item2;

        //YGameManager.Instance.IDsManager.SetMemoryValueByName("Camera.rotation.sin.x", Mathf.Sin(YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.x").Item2 * Mathf.Deg2Rad));
        //YGameManager.Instance.IDsManager.SetMemoryValueByName("Camera.rotation.sin.y", Mathf.Sin(YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.y").Item2 * Mathf.Deg2Rad));
        //YGameManager.Instance.IDsManager.SetMemoryValueByName("Camera.rotation.sin.z", Mathf.Sin(YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.z").Item2 * Mathf.Deg2Rad));
        //
        //YGameManager.Instance.IDsManager.SetMemoryValueByName("Camera.rotation.cos.x", Mathf.Cos(YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.x").Item2 * Mathf.Deg2Rad));
        //YGameManager.Instance.IDsManager.SetMemoryValueByName("Camera.rotation.cos.y", Mathf.Cos(YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.y").Item2 * Mathf.Deg2Rad));
        //YGameManager.Instance.IDsManager.SetMemoryValueByName("Camera.rotation.cos.z", Mathf.Cos(YGameManager.Instance.IDsManager.GetMemoryValueByName("Camera.rotation.z").Item2 * Mathf.Deg2Rad));
    }

    public override void Init()
    {
        if (initialised)
            return;

        yTransform.Init();


        focalLength = new YFloat(8);


        initialised = true;
    }

    public override void Begin()
    {
        SetDefaultSettings();
        cameraComponent = GetComponent<Camera>();
        //List<YTrigger> triggers = new List<YTrigger>()
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Equals, transform.position.x),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Equals, transform.position.y),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Equals, transform.position.z),
        //
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.x"), true, ItemEdit.Operation.Equals, -transform.eulerAngles.x),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.y"), true, ItemEdit.Operation.Equals, -transform.eulerAngles.y),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.z"), true, ItemEdit.Operation.Equals, -transform.eulerAngles.z),
        //
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.focalLen"), true, ItemEdit.Operation.Equals, cameraComponent.focalLength),
        //};

        focalLength.SetValue(cameraComponent.focalLength);
        //return triggers.ToArray();
    }



    public void SetFocalLen(float focalLen)
    {
        //new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.focalLen"), true, ItemEdit.Operation.Equals, focalLen);
        focalLength.SetValue(focalLen);
    }
    public void SetFocalLen(YVariable focalLen)
    {
        //new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.focalLen"), true, ItemEdit.Operation.Equals, 1, idIn, true, 0, true, ItemEdit.Operation.Add);
        focalLength.SetValue(focalLen);
    }
    public YVariable GetFocalLen()
    {
        return focalLength + 0;
        //var ret = new YVariable("Camera.focalLen") + 0;
        //return ret;
        //return new ItemEdit(idOut, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.focalLen"), true, 0, true, ItemEdit.Operation.Add);
    }
    /*
    public void SetPosition(int idInX, int idInY, int idInZ)
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
        //return result;
    }
    public void SetPosition(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Equals, z)
        };
        //return result;
    }
    public void SetPosition(YVector3 pos)
    {
        SetPosition(pos.x, pos.y, pos.z);
    }
    public YVector3 GetPosition()
    {
        var ret = new YVector3(new YVariable("Camera.position.x") + 0, new YVariable("Camera.position.y") + 0, new YVariable("Camera.position.z") + 0);
        return ret;
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, 0, true, ItemEdit.Operation.Add)
        //};
        //return result;
    }
    public void Translate(int idInX, int idInY, int idInZ)
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
        //return result;
    }
    public void Translate(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.x"), true, ItemEdit.Operation.Add, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.y"), true, ItemEdit.Operation.Add, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.position.z"), true, ItemEdit.Operation.Add, z)
        };
        //return result;
    }
    public void Translate(YVector3 pos)
    {
        Translate(pos.x, pos.y, pos.z);
    }
    public void TranslateLocal(int idInX, int idInY, int idInZ)
    {
        if (idInX == 0)
            idInX = 23;
        if (idInY == 0)
            idInY = 23;
        if (idInZ == 0)
            idInZ = 23;
        //YGameManager.Instance.RecordPool();

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

        X.Add(new YVariable(idInX, true) * cosY);
        //Y += 0;
        Z.Add(new YVariable(idInX, true) * sinY);

        X.Add(new YVariable(idInY, true) * sinY * sinX);
        Y.Add(new YVariable(idInY, true) * cosX);
        Z.Add(new YVariable(idInY, true) * cosY * sinX);

        X.Add(new YVariable(idInZ, true) * sinY * cosX * -1);
        Y.Add(new YVariable(idInZ, true) * sinX);
        Z.Add(new YVariable(idInZ, true) * cosY * cosX);


        curX.Add(X);
        curY.Add(Y);
        curZ.Add(Z);

        //return YGameManager.Instance.StopRecordPool();
    }
    public void TranslateLocal(float x, float y, float z)
    {
        //YGameManager.Instance.RecordPool();

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


        X.Add(new YFloat(x) * cosY);
        //Y += 0;
        Z.Add(new YFloat(x) * sinY);

        X.Add(new YFloat(y) * sinY * sinX);
        Y.Add(new YFloat(y) * cosX);
        Z.Add(new YFloat(y) * cosY * sinX);

        X.Add(new YFloat(z) * sinY * cosX * -1);
        Y.Add(new YFloat(z) * sinX);
        Z.Add(new YFloat(z) * cosY * cosX);


        curX.Add(X);
        curY.Add(Y);
        curZ.Add(Z);

        //return YGameManager.Instance.StopRecordPool();
    }
    public void TranslateLocal(YVector3 pos)
    {
        TranslateLocal(pos.x, pos.y, pos.z);
    }
    public void SetRotation(int idInX, int idInY, int idInZ)
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
        //return result;
    }
    public void SetRotation(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.x"), true, ItemEdit.Operation.Equals, -x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.y"), true, ItemEdit.Operation.Equals, -y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.z"), true, ItemEdit.Operation.Equals, -z)
        };
        //return result;
    }
    public void SetRotation(YVector3 rot)
    {
        SetRotation(rot.x, rot.y, rot.z);
    }
    public YVector3 GetRotation()
    {
        var ret = -new YVector3(new YVariable("Camera.rotation.x") + 0, new YVariable("Camera.rotation.y") + 0, new YVariable("Camera.rotation.z") + 0);
        return ret;

        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.x"), true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.y"), true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.z"), true, 0, true, ItemEdit.Operation.Add)
        //};
        //return result;
    }
    public void Rotate(int idInX, int idInY, int idInZ)
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
        //return result;
    }
    public void Rotate(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.x"), true, ItemEdit.Operation.Add, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.y"), true, ItemEdit.Operation.Add, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.z"), true, ItemEdit.Operation.Add, z)
        };
        //return result;
    }
    public void Rotate(YVector3 rot)
    {
        Rotate(rot.x, rot.y, rot.z);
    }
    public YVector3 GetSin()
    {
        var ret = new YVector3(new YVariable("Camera.rotation.sin.x") + 0, new YVariable("Camera.rotation.sin.y") + 0, new YVariable("Camera.rotation.sin.z") + 0);
        return ret;
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.sin.x"), true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.sin.y"), true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.sin.z"), true, 0, true, ItemEdit.Operation.Add)
        //};
        //return result;
    }
    public YVector3 GetCos()
    {
        var ret = new YVector3(new YVariable("Camera.rotation.cos.x") + 0, new YVariable("Camera.rotation.cos.y") + 0, new YVariable("Camera.rotation.cos.z") + 0);
        return ret;
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.cos.x"), true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.cos.y"), true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName("Camera.rotation.cos.z"), true, 0, true, ItemEdit.Operation.Add)
        //};
        //return result;
    }
    */
}
