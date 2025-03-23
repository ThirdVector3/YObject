
using System.Collections.Generic;
using UnityEngine;
public class YTransform : YMonoBehaviour
{
    public override YTrigger[] Begin()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        triggers.Add(new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.x"), true, ItemEdit.Operation.Equals, transform.position.x));
        triggers.Add(new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Equals, transform.position.y));
        triggers.Add(new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.z"), true, ItemEdit.Operation.Equals, transform.position.z));

        triggers.Add(new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, transform.eulerAngles.x));
        triggers.Add(new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, transform.eulerAngles.y));
        triggers.Add(new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, transform.eulerAngles.z));

        triggers.Add(new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.x"), true, ItemEdit.Operation.Equals, transform.localScale.x));
        triggers.Add(new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.y"), true, ItemEdit.Operation.Equals, transform.localScale.y));
        triggers.Add(new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.z"), true, ItemEdit.Operation.Equals, transform.localScale.z));

        return triggers.ToArray();
    }

    public override YGDObject[] Init()
    {
        if (initialised)
            return new YGDObject[0];

        if (gameObject.isStatic)
            return new YGDObject[0];

        YGameManager.Instance.AddVariable(gameObject.name + ".transform.position.x", YGameManager.Instance.GetFreeIdFloat(), true);
        YGameManager.Instance.AddVariable(gameObject.name + ".transform.position.y", YGameManager.Instance.GetFreeIdFloat(), true);
        YGameManager.Instance.AddVariable(gameObject.name + ".transform.position.z", YGameManager.Instance.GetFreeIdFloat(), true);

        YGameManager.Instance.AddVariable(gameObject.name + ".transform.rotation.x", YGameManager.Instance.GetFreeIdFloat(), true);
        YGameManager.Instance.AddVariable(gameObject.name + ".transform.rotation.y", YGameManager.Instance.GetFreeIdFloat(), true);
        YGameManager.Instance.AddVariable(gameObject.name + ".transform.rotation.z", YGameManager.Instance.GetFreeIdFloat(), true);

        YGameManager.Instance.AddVariable(gameObject.name + ".transform.rotation.sin.x", YGameManager.Instance.GetFreeIdFloat(), true);
        YGameManager.Instance.AddVariable(gameObject.name + ".transform.rotation.sin.y", YGameManager.Instance.GetFreeIdFloat(), true);
        YGameManager.Instance.AddVariable(gameObject.name + ".transform.rotation.sin.z", YGameManager.Instance.GetFreeIdFloat(), true);

        YGameManager.Instance.AddVariable(gameObject.name + ".transform.rotation.cos.x", YGameManager.Instance.GetFreeIdFloat(), true);
        YGameManager.Instance.AddVariable(gameObject.name + ".transform.rotation.cos.y", YGameManager.Instance.GetFreeIdFloat(), true);
        YGameManager.Instance.AddVariable(gameObject.name + ".transform.rotation.cos.z", YGameManager.Instance.GetFreeIdFloat(), true);

        YGameManager.Instance.AddVariable(gameObject.name + ".transform.scale.x", YGameManager.Instance.GetFreeIdFloat(), true);
        YGameManager.Instance.AddVariable(gameObject.name + ".transform.scale.y", YGameManager.Instance.GetFreeIdFloat(), true);
        YGameManager.Instance.AddVariable(gameObject.name + ".transform.scale.z", YGameManager.Instance.GetFreeIdFloat(), true);

        YGameManager.Instance.AddVariable(gameObject.name + ".transform.state", YGameManager.Instance.GetFreeIdFloat(), true);

        initialised = true;

        return new YGDObject[0];
    }

    public override YTrigger[] Tick()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        triggers.AddRange(YMath.SinDeg(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.x"), YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.sin.x")));
        triggers.AddRange(YMath.SinDeg(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.y"), YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.sin.y")));
        triggers.AddRange(YMath.SinDeg(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.z"), YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.sin.z")));

        triggers.AddRange(YMath.CosDeg(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.x"), YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.cos.x")));
        triggers.AddRange(YMath.CosDeg(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.y"), YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.cos.y")));
        triggers.AddRange(YMath.CosDeg(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.z"), YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.cos.z")));

        return triggers.ToArray();
    }





    public YTrigger[] SetPosition(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetPosition(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.z"), true, ItemEdit.Operation.Equals, z)
        };
        return result;
    }
    public YTrigger[] GetPosition(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Translate(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.x"), true, ItemEdit.Operation.Add, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Add, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.z"), true, ItemEdit.Operation.Add, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Translate(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.x"), true, ItemEdit.Operation.Add, x),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.y"), true, ItemEdit.Operation.Add, y),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.position.z"), true, ItemEdit.Operation.Add, z)
        };
        return result;
    }
    public YTrigger[] SetRotation(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetRotation(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, z)
        };
        return result;
    }
    public YTrigger[] GetRotation(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Rotate(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.x"), true, ItemEdit.Operation.Add, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.y"), true, ItemEdit.Operation.Add, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.z"), true, ItemEdit.Operation.Add, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] Rotate(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.x"), true, ItemEdit.Operation.Add, x),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.y"), true, ItemEdit.Operation.Add, y),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.z"), true, ItemEdit.Operation.Add, z)
        };
        return result;
    }
    public YTrigger[] GetSin(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.sin.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.sin.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.sin.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] GetCos(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.cos.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.cos.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.rotation.cos.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetScale(int idInX, int idInY, int idInZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetScale(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.z"), true, ItemEdit.Operation.Equals, z)
        };
        return result;
    }
    public YTrigger[] GetScale(int idOutX, int idOutY, int idOutZ)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOutX, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutY, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(idOutZ, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.scale.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return result;
    }
    public YTrigger[] SetState(int state)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.GetIdByName(gameObject.name + ".transform.state"), true, ItemEdit.Operation.Equals, state),
        };
        return result;
    }
    public YTrigger[] GetState(int idOut)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(idOut, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.GetIdByName(gameObject.name + ".transform.state"), true, 0, true, ItemEdit.Operation.Add),
        };
        return result;
    }





    private void Update()
    {
        transform.position = new Vector3(
            YGameManager.Instance.GetMemoryValueByName(gameObject.name + ".transform.position.x").Item2,
            YGameManager.Instance.GetMemoryValueByName(gameObject.name + ".transform.position.y").Item2,
            YGameManager.Instance.GetMemoryValueByName(gameObject.name + ".transform.position.z").Item2
        );
        if (YGameManager.Instance.GetMemoryValueByName(gameObject.name + ".transform.state").Item2 == 1 || YGameManager.Instance.GetMemoryValueByName(gameObject.name + ".transform.state").Item2 == 3)
        {
            transform.eulerAngles = new Vector3(
                YGameManager.Instance.GetMemoryValueByName(gameObject.name + ".transform.rotation.x").Item2,
                YGameManager.Instance.GetMemoryValueByName(gameObject.name + ".transform.rotation.y").Item2,
                YGameManager.Instance.GetMemoryValueByName(gameObject.name + ".transform.rotation.z").Item2
            );
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
        if (YGameManager.Instance.GetMemoryValueByName(gameObject.name + ".transform.state").Item2 == 2 || YGameManager.Instance.GetMemoryValueByName(gameObject.name + ".transform.state").Item2 == 3)
        {
            transform.localScale = new Vector3(
                YGameManager.Instance.GetMemoryValueByName(gameObject.name + ".transform.scale.x").Item2,
                YGameManager.Instance.GetMemoryValueByName(gameObject.name + ".transform.scale.y").Item2,
                YGameManager.Instance.GetMemoryValueByName(gameObject.name + ".transform.scale.z").Item2
            );
        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }
}
