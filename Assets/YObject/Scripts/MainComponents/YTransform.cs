
using System.Collections.Generic;
using UnityEngine;
public class YTransform : YMonoBehaviour
{
    public override YTrigger[] Begin()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        if (gameObject.isStatic)
            return triggers.ToArray();

        triggers.Add(new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.x"), true, ItemEdit.Operation.Equals, transform.position.x));
        triggers.Add(new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Equals, transform.position.y));
        triggers.Add(new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.z"), true, ItemEdit.Operation.Equals, transform.position.z));

        triggers.Add(new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, transform.eulerAngles.x));
        triggers.Add(new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, transform.eulerAngles.y));
        triggers.Add(new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, transform.eulerAngles.z));

        triggers.Add(new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.scale.x"), true, ItemEdit.Operation.Equals, transform.localScale.x));
        triggers.Add(new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.scale.y"), true, ItemEdit.Operation.Equals, transform.localScale.y));
        triggers.Add(new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.scale.z"), true, ItemEdit.Operation.Equals, transform.localScale.z));

        return triggers.ToArray();
    }

    public override YGDObject[] Init()
    {
        if (initialised)
            return new YGDObject[0];

        if (gameObject.isStatic)
            return new YGDObject[0];

        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.position.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.position.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.position.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.rotation.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.rotation.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.rotation.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.rotation.sin.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.rotation.sin.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.rotation.sin.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.rotation.cos.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.rotation.cos.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.rotation.cos.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.scale.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.scale.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.scale.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.name + ".transform.state", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        //print(YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.position.x").Item2);

        initialised = true;

        return new YGDObject[0];
    }

    public override YTrigger[] Tick()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        if (gameObject.isStatic)
            return triggers.ToArray();

        triggers.AddRange(YMath.SinDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.x"), YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.sin.x")));
        triggers.AddRange(YMath.SinDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.y"), YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.sin.y")));
        triggers.AddRange(YMath.SinDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.z"), YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.sin.z")));

        triggers.AddRange(YMath.CosDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.x"), YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.cos.x")));
        triggers.AddRange(YMath.CosDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.y"), YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.cos.y")));
        triggers.AddRange(YMath.CosDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.z"), YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.cos.z")));

        return triggers.ToArray();
    }





    public YTrigger[] SetPosition(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetPosition(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.z"), true, ItemEdit.Operation.Equals, z)
        };
        return result;
    }
    public YTrigger[] GetPosition(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Translate(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.x"), true, ItemEdit.Operation.Add, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Add, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.z"), true, ItemEdit.Operation.Add, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Translate(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.x"), true, ItemEdit.Operation.Add, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Add, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.position.z"), true, ItemEdit.Operation.Add, z)
        };
        return result;
    }
    public YTrigger[] SetRotation(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetRotation(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, z)
        };
        return result;
    }
    public YTrigger[] GetRotation(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Rotate(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.x"), true, ItemEdit.Operation.Add, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.y"), true, ItemEdit.Operation.Add, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.z"), true, ItemEdit.Operation.Add, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Rotate(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.x"), true, ItemEdit.Operation.Add, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.y"), true, ItemEdit.Operation.Add, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.z"), true, ItemEdit.Operation.Add, z)
        };
        return result;
    }
    public YTrigger[] GetSin(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.sin.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.sin.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.sin.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] GetCos(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.cos.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.cos.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.rotation.cos.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetScale(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.scale.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.scale.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.scale.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetScale(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.scale.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.scale.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.scale.z"), true, ItemEdit.Operation.Equals, z)
        };
        return result;
    }
    public YTrigger[] GetScale(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.scale.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.scale.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.scale.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetState(int state)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.state"), true, ItemEdit.Operation.Equals, state),
        };
        return result;
    }
    public YTrigger[] GetState(int idOut)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOut, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.name + ".transform.state"), true, 0, true, ItemEdit.Operation.Add),
        };
        return result;
    }





    private void Update()
    {
        if (!gameObject.isStatic)
        {
            var group = GetComponent<YGameobjectGroup>();
            transform.position = new Vector3(
                YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.position.x", group != null ? group.GetName() : null).Item2,
                YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.position.y", group != null ? group.GetName() : null).Item2,
                YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.position.z", group != null ? group.GetName() : null).Item2
            );
            if (YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.state", group != null ? group.GetName() : null).Item2 == 1 || YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.state", group != null ? group.GetName() : null).Item2 == 3)
            {
                transform.eulerAngles = new Vector3(
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.rotation.x", group != null ? group.GetName() : null).Item2,
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.rotation.y", group != null ? group.GetName() : null).Item2,
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.rotation.z", group != null ? group.GetName() : null).Item2
                );
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
            }
            if (YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.state", group != null ? group.GetName() : null).Item2 == 2 || YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.state", group != null ? group.GetName() : null).Item2 == 3)
            {
                transform.localScale = new Vector3(
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.scale.x", group != null ? group.GetName() : null).Item2,
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.scale.y", group != null ? group.GetName() : null).Item2,
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.name + ".transform.scale.z", group != null ? group.GetName() : null).Item2
                );
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
    }
}
