
using System.Collections.Generic;
using UnityEngine;
public class YTransform : YMonoBehaviour
{
    /* [Header(@"0 - changable position
1 - changable position and rotation
2 - changable position and scale
3 - changable position, rotation and scale")]
    [Range(0,3)] [SerializeField] private int state; */

    [SerializeField] private bool canRotate;
    [SerializeField] private bool canScale;

    public override void Begin()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        if (gameObject.isStatic)
            return;// triggers.ToArray();

        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Equals, transform.position.x);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Equals, transform.position.y);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Equals, transform.position.z);

        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, transform.eulerAngles.x);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, transform.eulerAngles.y);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, transform.eulerAngles.z);

        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.x"), true, ItemEdit.Operation.Equals, transform.localScale.x);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.y"), true, ItemEdit.Operation.Equals, transform.localScale.y);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.z"), true, ItemEdit.Operation.Equals, transform.localScale.z);

        if (!canRotate && !canScale)
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.state"), true, ItemEdit.Operation.Equals, 0);
        else if (canRotate && !canScale)
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.state"), true, ItemEdit.Operation.Equals, 1);
        else if(!canRotate && canScale)
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.state"), true, ItemEdit.Operation.Equals, 2);
        else if(canRotate && canScale)
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.state"), true, ItemEdit.Operation.Equals, 3);

        //return triggers.ToArray();
    }

    public override void Init()
    {
        if (initialised)
            return;

        if (gameObject.isStatic)
            return;

        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.position.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.position.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.position.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.rotation.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.rotation.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.rotation.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.rotation.sin.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.rotation.sin.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.rotation.sin.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.rotation.cos.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.rotation.cos.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.rotation.cos.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.scale.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.scale.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.scale.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.state", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        //print(YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.position.x").Item2);

        initialised = true;

    }

    public override void Tick()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        if (gameObject.isStatic)
            return;// triggers.ToArray();

        triggers.AddRange(YMath.SinDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.sin.x")));
        triggers.AddRange(YMath.SinDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.sin.y")));
        triggers.AddRange(YMath.SinDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.sin.z")));

        triggers.AddRange(YMath.CosDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.x")));
        triggers.AddRange(YMath.CosDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.y")));
        triggers.AddRange(YMath.CosDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.z")));

        //return triggers.ToArray();
    }





    public YTrigger[] SetPosition(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetPosition(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Equals, z)
        };
        return result;
    }
    public YTrigger[] GetPosition(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Translate(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Add, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Add, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Add, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Translate(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Add, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Add, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Add, z)
        };
        return result;
    }
    public YTrigger[] SetRotation(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetRotation(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, z)
        };
        return result;
    }
    public YTrigger[] GetRotation(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Rotate(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), true, ItemEdit.Operation.Add, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), true, ItemEdit.Operation.Add, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), true, ItemEdit.Operation.Add, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Rotate(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), true, ItemEdit.Operation.Add, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), true, ItemEdit.Operation.Add, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), true, ItemEdit.Operation.Add, z)
        };
        return result;
    }
    public YTrigger[] GetSin(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.sin.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.sin.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.sin.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] GetCos(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetScale(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetScale(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.z"), true, ItemEdit.Operation.Equals, z)
        };
        return result;
    }
    public YTrigger[] GetScale(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetState(bool canRotate, bool canScale)
    {
        YTrigger[] result;// = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.state"), true, ItemEdit.Operation.Equals, state),
        //};
        if (!canRotate && !canScale)
            result = new YTrigger[] { new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.state"), true, ItemEdit.Operation.Equals, 0) };
        else if (canRotate && !canScale)
            result = new YTrigger[] { new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.state"), true, ItemEdit.Operation.Equals, 1) };
        else if (!canRotate && canScale)
            result = new YTrigger[] { new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.state"), true, ItemEdit.Operation.Equals, 2) };
        else
            result = new YTrigger[] { new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.state"), true, ItemEdit.Operation.Equals, 3) };
        return result;
    }
    public YTrigger[] GetState(int idOut)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOut, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.state"), true, 0, true, ItemEdit.Operation.Add),
        };
        return result;
    }





    private void Update()
    {
        if (!gameObject.isStatic)
        {
            var group = GetComponent<YGameobjectGroup>();
            transform.position = new Vector3(
                YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.position.x", group != null ? group.GetName() : null).Item2,
                YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.position.y", group != null ? group.GetName() : null).Item2,
                YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.position.z", group != null ? group.GetName() : null).Item2
            );
            if (YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.state", group != null ? group.GetName() : null).Item2 == 1 || YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.state", group != null ? group.GetName() : null).Item2 == 3)
            {
                transform.eulerAngles = new Vector3(
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.rotation.x", group != null ? group.GetName() : null).Item2,
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.rotation.y", group != null ? group.GetName() : null).Item2,
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.rotation.z", group != null ? group.GetName() : null).Item2
                );
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
            }

            if (YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.state", group != null ? group.GetName() : null).Item2 == 2 || YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.state", group != null ? group.GetName() : null).Item2 == 3)
            {

                transform.localScale = new Vector3(
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.scale.x", group != null ? group.GetName() : null).Item2,
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.scale.y", group != null ? group.GetName() : null).Item2,
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.scale.z", group != null ? group.GetName() : null).Item2
                );
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
    }
}
