
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;

public class YTransform : YMonoBehaviour
{

    [SerializeField] private bool canRotate;
    [SerializeField] private bool canScale;

    private YQuaternion rotation;
    private YVector3 position;

    private YVariable rotationNormalizationCountdown;

    public override void Begin()
    {
        List<YTrigger> triggers = new List<YTrigger>();

        if (gameObject.isStatic)
            return;// triggers.ToArray();

        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localposition.x"), true, ItemEdit.Operation.Equals, transform.localPosition.x);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localposition.y"), true, ItemEdit.Operation.Equals, transform.localPosition.y);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localposition.z"), true, ItemEdit.Operation.Equals, transform.localPosition.z);

        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.x"), true, ItemEdit.Operation.Equals, transform.localRotation.x);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.y"), true, ItemEdit.Operation.Equals, transform.localRotation.y);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.z"), true, ItemEdit.Operation.Equals, transform.localRotation.z);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.w"), true, ItemEdit.Operation.Equals, transform.localRotation.w);

        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Equals, transform.position.x);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Equals, transform.position.y);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Equals, transform.position.z);

        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, transform.rotation.x);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, transform.rotation.y);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, transform.rotation.z);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.w"), true, ItemEdit.Operation.Equals, transform.rotation.w);

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


        //SetSinCoses();
        //return triggers.ToArray();
    }

    public override void Init()
    {
        if (initialised)
            return;

        if (gameObject.isStatic)
            return;

        rotationNormalizationCountdown = new YInt(30);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localposition.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localposition.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localposition.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localrotation.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localrotation.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localrotation.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localrotation.w", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        //YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localrotation.sin.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        //YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localrotation.sin.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        //YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localrotation.sin.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        //
        //YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localrotation.cos.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        //YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localrotation.cos.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        //YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localrotation.cos.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        //YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localscale.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        //YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localscale.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        //YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.localscale.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.position.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.position.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.position.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.rotation.x", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.rotation.y", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.rotation.z", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);
        YGameManager.Instance.IDsManager.AddVariable(gameObject.GetInstanceID() + ".transform.rotation.w", YGameManager.Instance.IDsManager.GetFreeIdFloat(), true);

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

        rotation = new YQuaternion(
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.x"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.y"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.z"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.w"));

        position = new YVector3(
            new YVariable(gameObject.GetInstanceID() + ".transform.localposition.x"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localposition.y"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localposition.z"));

        initialised = true;

    }

    public override void Tick()
    {
        if (gameObject.isStatic)
            return;

        rotationNormalizationCountdown.Subtract(1);
        new Condition(rotationNormalizationCountdown, new YInt(0), ItemCompare.Operation.LessOrEquals)
            .Then(() =>
            {
                rotationNormalizationCountdown.SetValue(30);
                rotation.Normalize();
            });


        //set world pos
        var worldTransform = LocalToParentTransform();
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Equals, 1, worldTransform.position.x, true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Equals, 1, worldTransform.position.y, true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Equals, 1, worldTransform.position.z, true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, 1, worldTransform.rotation.x, true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, 1, worldTransform.rotation.y, true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, 1, worldTransform.rotation.z, true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.w"), true, ItemEdit.Operation.Equals, 1, worldTransform.rotation.w, true, 0, true, ItemEdit.Operation.Add);

        //var localTransform = ParentToLocalTransform();
        //new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localposition.x"), true, ItemEdit.Operation.Equals, 1, localTransform.position.x, true, 0, true, ItemEdit.Operation.Add);
        //new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localposition.y"), true, ItemEdit.Operation.Equals, 1, localTransform.position.y, true, 0, true, ItemEdit.Operation.Add);
        //new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localposition.z"), true, ItemEdit.Operation.Equals, 1, localTransform.position.z, true, 0, true, ItemEdit.Operation.Add);
        //new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.x"), true, ItemEdit.Operation.Equals, 1, localTransform.rotation.x, true, 0, true, ItemEdit.Operation.Add);
        //new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.y"), true, ItemEdit.Operation.Equals, 1, localTransform.rotation.y, true, 0, true, ItemEdit.Operation.Add);
        //new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.z"), true, ItemEdit.Operation.Equals, 1, localTransform.rotation.z, true, 0, true, ItemEdit.Operation.Add);
        //new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.w"), true, ItemEdit.Operation.Equals, 1, localTransform.rotation.w, true, 0, true, ItemEdit.Operation.Add);



    }

    private YTrigger[] SetSinCoses()
    {
        List<YTrigger> triggers = new List<YTrigger>();
        //triggers.AddRange(YMath.SinDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.x"), YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.sin.x")));
        //triggers.AddRange(YMath.SinDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.y"), YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.sin.y")));
        //triggers.AddRange(YMath.SinDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.z"), YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.sin.z")));
        //
        //triggers.AddRange(YMath.CosDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.x"), YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.cos.x")));
        //triggers.AddRange(YMath.CosDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.y"), YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.cos.y")));
        //triggers.AddRange(YMath.CosDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.z"), YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.cos.z")));

        triggers.AddRange(YMath.SinDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.sin.x")));
        triggers.AddRange(YMath.SinDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.sin.y")));
        triggers.AddRange(YMath.SinDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.sin.z")));

        triggers.AddRange(YMath.CosDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.x")));
        triggers.AddRange(YMath.CosDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.y")));
        triggers.AddRange(YMath.CosDeg(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.cos.z")));
        return triggers.ToArray();
    }

    public class TransformValue
    {
        public TransformValue() { }

        public YVector3 position;
        public YQuaternion rotation;
        public YVector3 scale;
    }
    public TransformValue LocalToParentTransform()
    {
        var ret = new TransformValue();
        if (!transform.parent || !transform.parent.GetComponent<YTransform>())
        {
            ret.position = new YVector3(
            new YVariable(gameObject.GetInstanceID() + ".transform.localposition.x"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localposition.y"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localposition.z"));

            ret.rotation = new YQuaternion(
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.x"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.y"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.z"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.w"));

            ret.scale = new YVector3(
            new YVariable(gameObject.GetInstanceID() + ".transform.scale.x"),
            new YVariable(gameObject.GetInstanceID() + ".transform.scale.y"),
            new YVariable(gameObject.GetInstanceID() + ".transform.scale.z"));

            return ret;
        }

        ret.position = new YVector3(
            new YVariable(gameObject.GetInstanceID() + ".transform.localposition.x"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localposition.y"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localposition.z"));

        ret.scale = new YVector3(
            new YVariable(gameObject.GetInstanceID() + ".transform.scale.x") + 0,
            new YVariable(gameObject.GetInstanceID() + ".transform.scale.y") + 0,
            new YVariable(gameObject.GetInstanceID() + ".transform.scale.z") + 0);

        ret.rotation = new YQuaternion(
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.x"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.y"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.z"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.w"));

        //var parentData = transform.parent.GetComponent<YTransform>().LocalToParentTransform();
        var parentTransform = transform.parent.GetComponent<YTransform>();

        ret.position = ret.position * parentTransform.GetScale();

        ret.rotation = (parentTransform.GetRotation() * ret.rotation);

        ret.position.SetValue(parentTransform.GetRotation() * ret.position);

        ret.position.Add(parentTransform.GetPosition());

        return ret;
    }
    public TransformValue ParentToLocalTransform()
    {

        var ret = new TransformValue();
        ret.position = new YVector3(
        new YVariable(gameObject.GetInstanceID() + ".transform.position.x"),
        new YVariable(gameObject.GetInstanceID() + ".transform.position.y"),
        new YVariable(gameObject.GetInstanceID() + ".transform.position.z"));

        ret.rotation = new YQuaternion(
        new YVariable(gameObject.GetInstanceID() + ".transform.rotation.x"),
        new YVariable(gameObject.GetInstanceID() + ".transform.rotation.y"),
        new YVariable(gameObject.GetInstanceID() + ".transform.rotation.z"),
        new YVariable(gameObject.GetInstanceID() + ".transform.rotation.w"));

        ret.scale = new YVector3(
        new YVariable(gameObject.GetInstanceID() + ".transform.scale.x"),
        new YVariable(gameObject.GetInstanceID() + ".transform.scale.y"),
        new YVariable(gameObject.GetInstanceID() + ".transform.scale.z"));

        if (!transform.parent || !transform.parent.GetComponent<YTransform>())
        {
            return ret;
        }

        var parentData = new TransformValue();

        parentData.position = new YVector3(
        new YVariable(transform.parent.gameObject.GetInstanceID() + ".transform.position.x"),
        new YVariable(transform.parent.gameObject.GetInstanceID() + ".transform.position.y"),
        new YVariable(transform.parent.gameObject.GetInstanceID() + ".transform.position.z"));

        parentData.rotation = new YQuaternion(
        new YVariable(transform.parent.gameObject.GetInstanceID() + ".transform.rotation.x"),
        new YVariable(transform.parent.gameObject.GetInstanceID() + ".transform.rotation.y"),
        new YVariable(transform.parent.gameObject.GetInstanceID() + ".transform.rotation.z"),
        new YVariable(transform.parent.gameObject.GetInstanceID() + ".transform.rotation.w"));

        parentData.scale = new YVector3(
        new YVariable(transform.parent.gameObject.GetInstanceID() + ".transform.scale.x"),
        new YVariable(transform.parent.gameObject.GetInstanceID() + ".transform.scale.y"),
        new YVariable(transform.parent.gameObject.GetInstanceID() + ".transform.scale.z"));



        ret.rotation = parentData.rotation.Conjugate() * ret.rotation;

        //ret.rotation.Normalize();

        ret.position = parentData.rotation.Conjugate() * (ret.position - parentData.position);

        ret.position.Divide(parentData.scale);

        return ret;
    }



    public virtual void SetPosition(YVector3 pos)
    {
        var currentPosition = new YVector3(
            new YVariable(gameObject.GetInstanceID() + ".transform.position.x"),
            new YVariable(gameObject.GetInstanceID() + ".transform.position.y"),
            new YVariable(gameObject.GetInstanceID() + ".transform.position.z")
        );
        currentPosition.SetValue(pos);

        var local = ParentToLocalTransform();
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localposition.x"), true, ItemEdit.Operation.Equals, 1, local.position.x, true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localposition.y"), true, ItemEdit.Operation.Equals, 1, local.position.y, true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localposition.z"), true, ItemEdit.Operation.Equals, 1, local.position.z, true, 0, true, ItemEdit.Operation.Add);
    }
    public virtual void SetPosition(int idInX, int idInY, int idInZ)
    {
        if (idInX == 0)
            idInX = 23;
        if (idInY == 0)
            idInY = 23;
        if (idInZ == 0)
            idInZ = 23;
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        //};
        SetPosition(new YVector3(new YVariable(idInX, true), new YVariable(idInY, true), new YVariable(idInZ, true)));
        //return result;
    }
    public virtual void SetPosition(float x, float y, float z)
    {
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Equals, x),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Equals, y),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Equals, z)
        //};
        SetPosition(new YVector3(x, y, z));
        //return result;
    }
    public virtual YVector3 GetPosition()
    {
        var v = new YVector3(0f, 0f, 0f);
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(v.x, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(v.y, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(v.z, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return v;
    }
    public virtual void SetLocalPosition(YVector3 pos)
    {
        var currentPosition = new YVector3(
            new YVariable(gameObject.GetInstanceID() + ".transform.localposition.x"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localposition.y"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localposition.z")
        );
        currentPosition.SetValue(pos);
    }
    public virtual void SetLocalPosition(int idInX, int idInY, int idInZ)
    {
        if (idInX == 0)
            idInX = 23;
        if (idInY == 0)
            idInY = 23;
        if (idInZ == 0)
            idInZ = 23;
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        //};
        SetLocalPosition(new YVector3(new YVariable(idInX, true), new YVariable(idInY, true), new YVariable(idInZ, true)));
        //return result;
    }
    public virtual void SetLocalPosition(float x, float y, float z)
    {
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Equals, x),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Equals, y),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Equals, z)
        //};
        SetLocalPosition(new YVector3(x, y, z));
        //return result;
    }
    public virtual YVector3 GetLocalPosition()
    {
        var v = new YVector3(0f, 0f, 0f);
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(v.x, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localposition.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(v.y, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localposition.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(v.z, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localposition.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return v;
    }
    public virtual void Translate(YVector3 pos)
    {
        Translate(pos.x, pos.y, pos.z);
    }
    public virtual void Translate(int idInX, int idInY, int idInZ)
    {
        if (idInX == 0)
            idInX = 23;
        if (idInY == 0)
            idInY = 23;
        if (idInZ == 0)
            idInZ = 23;
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Add, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Add, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Add, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        //};
        position.Add(new YVector3(new YVariable(idInX, true), new YVariable(idInY, true), new YVariable(idInZ, true)));
        //return result;
    }
    public virtual void Translate(float x, float y, float z)
    {
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, ItemEdit.Operation.Add, x),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, ItemEdit.Operation.Add, y),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, ItemEdit.Operation.Add, z)
        //};
        position.Add(new YVector3(x, y, z));
        //return result;
    }
    public virtual void TranslateLocal(YVector3 pos)
    {
        TranslateLocal(pos.x, pos.y, pos.z);
    }
    public virtual void TranslateLocal(int idInX, int idInY, int idInZ)
    {
        if (idInX == 0)
            idInX = 23;
        if (idInY == 0)
            idInY = 23;
        if (idInZ == 0)
            idInZ = 23;
        //YGameManager.Instance.RecordPool();

        //YVariable sinX = new YVariable(gameObject.GetInstanceID() + ".transform.rotation.sin.x");
        //YVariable sinY = new YVariable(gameObject.GetInstanceID() + ".transform.rotation.sin.y");
        //YVariable sinZ = new YVariable(gameObject.GetInstanceID() + ".transform.rotation.sin.z");
        //YVariable cosX = new YVariable(gameObject.GetInstanceID() + ".transform.rotation.cos.x");
        //YVariable cosY = new YVariable(gameObject.GetInstanceID() + ".transform.rotation.cos.y");
        //YVariable cosZ = new YVariable(gameObject.GetInstanceID() + ".transform.rotation.cos.z");
        //
        //YVariable curX = new YVariable(gameObject.GetInstanceID() + ".transform.position.x");
        //YVariable curY = new YVariable(gameObject.GetInstanceID() + ".transform.position.y");
        //YVariable curZ = new YVariable(gameObject.GetInstanceID() + ".transform.position.z");
        //
        //YVariable X = new YFloat(0);
        //YVariable Y = new YFloat(0);
        //YVariable Z = new YFloat(0);
        //
        //
        //X.Add(new YFloat(1) * new YVariable(idInX, true) * cosY);
        //
        //Z.Add(new YFloat(-1) * new YVariable(idInX, true) * sinY);
        //
        //X.Add(new YFloat(1) * new YVariable(idInY, true) * sinY * sinX);
        //Y.Add(new YFloat(1) * new YVariable(idInY, true) * cosX);
        //Z.Add(new YFloat(1) * new YVariable(idInY, true) * cosY * sinX);
        //
        //X.Add(new YFloat(1) * new YVariable(idInZ, true) * sinY * cosX);
        //Y.Add(new YFloat(1) * new YVariable(idInZ, true) * sinX);
        //Z.Add(new YFloat(1) * new YVariable(idInZ, true) * cosY * cosX);
        //
        //
        //curX.Add(X);
        //curY.Add(Y);
        //curZ.Add(Z);

        position.Add(rotation * new YVector3(new YVariable(idInX, true), new YVariable(idInY, true), new YVariable(idInZ, true)));

        //return YGameManager.Instance.StopRecordPool(false);
    }
    public virtual void TranslateLocal(float x, float y, float z)
    {
        //YGameManager.Instance.RecordPool();

        //YVariable sinX = new YVariable(gameObject.GetInstanceID() + ".transform.rotation.sin.x");
        //YVariable sinY = new YVariable(gameObject.GetInstanceID() + ".transform.rotation.sin.y");
        //YVariable sinZ = new YVariable(gameObject.GetInstanceID() + ".transform.rotation.sin.z");
        //YVariable cosX = new YVariable(gameObject.GetInstanceID() + ".transform.rotation.cos.x");
        //YVariable cosY = new YVariable(gameObject.GetInstanceID() + ".transform.rotation.cos.y");
        //YVariable cosZ = new YVariable(gameObject.GetInstanceID() + ".transform.rotation.cos.z");
        //
        //YVariable curX = new YVariable(gameObject.GetInstanceID() + ".transform.position.x");
        //YVariable curY = new YVariable(gameObject.GetInstanceID() + ".transform.position.y");
        //YVariable curZ = new YVariable(gameObject.GetInstanceID() + ".transform.position.z");
        //
        //YVariable X = new YFloat(0);
        //YVariable Y = new YFloat(0);
        //YVariable Z = new YFloat(0);
        //
        //
        //X.Add(new YFloat(x) * cosY);
        //
        //Z.Add(new YFloat(-x) * sinY);
        //
        //X.Add(new YFloat(y) * sinY * sinX);
        //Y.Add(new YFloat(y) * cosX);
        //Z.Add(new YFloat(y) * cosY * sinX);
        //
        //X.Add(new YFloat(z) * sinY * cosX);
        //Y.Add(new YFloat(z) * sinX);
        //Z.Add(new YFloat(z) * cosY * cosX);
        //
        //
        //curX.Add(X);
        //curY.Add(Y);
        //curZ.Add(Z);

        position.Add(rotation * new YVector3(0, 0, 1f) * new YVector3(x, y, z));

        //return YGameManager.Instance.StopRecordPool();
    }
    public virtual void SetLocalRotation(YQuaternion rot)
    {
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.x"), true, ItemEdit.Operation.Equals, rot.x),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.y"), true, ItemEdit.Operation.Equals, rot.y),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.z"), true, ItemEdit.Operation.Equals, rot.z),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.w"), true, ItemEdit.Operation.Equals, rot.w)
        //};
        var currentRotation = new YQuaternion(
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.x"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.y"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.z"),
            new YVariable(gameObject.GetInstanceID() + ".transform.localrotation.w")
        );
        currentRotation.SetValue(rot);
    }
    public virtual void SetLocalRotation(YVector3 rot)
    {
        SetLocalRotation(YQuaternion.Euler(rot));
    }
    public virtual void SetLocalRotation(int idInX, int idInY, int idInZ)
    {
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        //};
        //return result;
        SetLocalRotation(YQuaternion.Euler(new YVector3(new YVariable(idInX, true), new YVariable(idInY, true), new YVariable(idInZ, true))));
    }
    public virtual void SetLocalRotation(float x, float y, float z)
    {
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, x),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, y),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, z)
        //};
        //return result;
        SetLocalRotation(new YQuaternion(Quaternion.Euler(x, y, z)));
    }
    public virtual YQuaternion GetLocalRotation()
    {
        var q = new YQuaternion(0f, 0, 0, 0);
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(q.x, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(q.y, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(q.z, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.z"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(q.w, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.w"), true, 0, true, ItemEdit.Operation.Add)
        };
        return q;
    }
    public virtual void SetRotation(YQuaternion rot)
    {
        var currentRotation = new YQuaternion(
            new YVariable(gameObject.GetInstanceID() + ".transform.rotation.x"),
            new YVariable(gameObject.GetInstanceID() + ".transform.rotation.y"),
            new YVariable(gameObject.GetInstanceID() + ".transform.rotation.z"),
            new YVariable(gameObject.GetInstanceID() + ".transform.rotation.w")
        );
        currentRotation.SetValue(rot);

        var local = ParentToLocalTransform();
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.x"), true, ItemEdit.Operation.Equals, 1, local.rotation.x, true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.y"), true, ItemEdit.Operation.Equals, 1, local.rotation.y, true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.z"), true, ItemEdit.Operation.Equals, 1, local.rotation.z, true, 0, true, ItemEdit.Operation.Add);
        new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.w"), true, ItemEdit.Operation.Equals, 1, local.rotation.w, true, 0, true, ItemEdit.Operation.Add);
    }
    public virtual void SetRotation(YVector3 rot)
    {
        SetRotation(YQuaternion.Euler(rot));
    }
    public virtual void SetRotation(int idInX, int idInY, int idInZ)
    {
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        //};
        //return result;
        SetRotation(YQuaternion.Euler(new YVector3(new YVariable(idInX, true), new YVariable(idInY, true), new YVariable(idInZ, true))));
    }
    public virtual void SetRotation(float x, float y, float z)
    {
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), true, ItemEdit.Operation.Equals, x),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), true, ItemEdit.Operation.Equals, y),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), true, ItemEdit.Operation.Equals, z)
        //};
        //return result;
        SetRotation(new YQuaternion(Quaternion.Euler(x, y, z)));
    }
    public virtual YQuaternion GetRotation()
    {
        var q = new YQuaternion(0f, 0, 0, 0);
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(q.x, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(q.y, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(q.z, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.z"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(q.w, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.rotation.w"), true, 0, true, ItemEdit.Operation.Add)
        };
        return q;
    }
    public virtual void Rotate(YVector3 rot)
    {
        Rotate(rot.x, rot.y, rot.z);
    }
    public virtual void Rotate(int idInX, int idInY, int idInZ)
    {
        if (idInX == 0)
            idInX = 23;
        if (idInY == 0)
            idInY = 23;
        if (idInZ == 0)
            idInZ = 23;
        rotation.Multiply(YQuaternion.Euler(new YVector3(new YVariable(idInX, true), new YVariable(idInY, true), new YVariable(idInZ, true))));
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.x"), true, ItemEdit.Operation.Add, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.y"), true, ItemEdit.Operation.Add, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.z"), true, ItemEdit.Operation.Add, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        //};
        //return result;
    }
    public virtual void Rotate(float x, float y, float z)
    {
        rotation.Multiply(new YQuaternion(Quaternion.Euler(x,y,z)));
        //YTrigger[] result = new YTrigger[]
        //{
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.x"), true, ItemEdit.Operation.Add, x),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.y"), true, ItemEdit.Operation.Add, y),
        //    new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.localrotation.z"), true, ItemEdit.Operation.Add, z)
        //};
        //return result;
    }
    public virtual void SetScale(YVector3 scale)
    {
        SetScale(scale.x, scale.y, scale.z);
    }
    public virtual void SetScale(int idInX, int idInY, int idInZ)
    {
        if (idInX == 0)
            idInX = 23;
        if (idInY == 0)
            idInY = 23;
        if (idInZ == 0)
            idInZ = 23;
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.x"), true, ItemEdit.Operation.Equals, 1, idInX, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.y"), true, ItemEdit.Operation.Equals, 1, idInY, true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.z"), true, ItemEdit.Operation.Equals, 1, idInZ, true, 0, true, ItemEdit.Operation.Add)
        };
        //return result;
    }
    public virtual void SetScale(float x, float y, float z)
    {
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.x"), true, ItemEdit.Operation.Equals, x),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.y"), true, ItemEdit.Operation.Equals, y),
            new ItemEdit(YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.z"), true, ItemEdit.Operation.Equals, z)
        };
        //return result;
    }
    public virtual YVector3 GetScale()
    {
        var v = new YVector3(0f,0,0);
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(v.x, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.x"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(v.y, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.y"), true, 0, true, ItemEdit.Operation.Add),
            new ItemEdit(v.z, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.scale.z"), true, 0, true, ItemEdit.Operation.Add)
        };
        return v;
    }
    public virtual void SetState(bool canRotate, bool canScale)
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
        //return result;
    }
    public virtual YVariable GetState()
    {
        var v = new YInt(0);
        YTrigger[] result = new YTrigger[]
        {
            new ItemEdit(v, true, ItemEdit.Operation.Equals, 1, YGameManager.Instance.IDsManager.GetIdByName(gameObject.GetInstanceID() + ".transform.state"), true, 0, true, ItemEdit.Operation.Add),
        };
        return v;
    }





    private void Update()
    {
        //print(YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.localposition.z").Item2);
        if (!gameObject.isStatic)
        {
            try
            {
                var group = GetComponent<YGameobjectGroup>();
                transform.position = new Vector3(
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.position.x", group != null ? group.GetName() : null).Item2,
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.position.y", group != null ? group.GetName() : null).Item2,
                    YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.position.z", group != null ? group.GetName() : null).Item2
                );
                if (YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.state", group != null ? group.GetName() : null).Item2 == 1 || YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.state", group != null ? group.GetName() : null).Item2 == 3)
                {
                    transform.rotation = new Quaternion(
                        YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.rotation.x", group != null ? group.GetName() : null).Item2,
                        YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.rotation.y", group != null ? group.GetName() : null).Item2,
                        YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.rotation.z", group != null ? group.GetName() : null).Item2,
                        YGameManager.Instance.IDsManager.GetMemoryValueByName(gameObject.GetInstanceID() + ".transform.rotation.w", group != null ? group.GetName() : null).Item2
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
            catch { }
        }
    }
}
