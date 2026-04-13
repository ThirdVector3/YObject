using System.Collections.Generic;

public class Y3DService : YService<Y3DService>
{
    public int localToCameraPosTranslateFunctionID { get; private set; }
    public override void Init()
    {
        LocalToCameraPosTranslate();
    }
    private void LocalToCameraPosTranslate()
    {
        localToCameraPosTranslateFunctionID = YIDsManager.Instance.GetFreeGroup();
        YIDsManager.Instance.AddGroup(localToCameraPosTranslateFunctionID);

        YMainCamera.Instance.Init();

        YGameManager.Instance.RecordPool();

        new YVariable(9996, true).SetValue(new YVariable(9999, true));
        new YVariable(9995, true).SetValue(new YVariable(9998, true));
        new YVariable(9994, true).SetValue(new YVariable(9997, true));

        new Condition((new YVariable(9980, true) == 2) + (new YVariable(9980, true) == 3))
            .Then(() =>
            {
                new YVariable(9996, true).Multiply(new YVariable(9983, true));
                new YVariable(9995, true).Multiply(new YVariable(9982, true));
                new YVariable(9994, true).Multiply(new YVariable(9981, true));
            });

        new Condition((new YVariable(9980, true) == 1) + (new YVariable(9980, true) == 3))
            .Then(() =>
            {
                var v = new YVector3(new YVariable(9996, true), new YVariable(9995, true), new YVariable(9994, true));
                var q = new YQuaternion(new YVariable(9989, true), new YVariable(9988, true), new YVariable(9987, true), new YVariable(9986, true));
        
                v.SetValue(q * v);
            });

        new YVariable(9996, true).Add(new YVariable(9992, true));
        new YVariable(9995, true).Add(new YVariable(9991, true));
        new YVariable(9994, true).Add(new YVariable(9990, true));

        new YVariable(9976, true).SetValue(new YVariable(9996, true));
        new YVariable(9975, true).SetValue(new YVariable(9995, true));
        new YVariable(9974, true).SetValue(new YVariable(9994, true));

        var camPos = YMainCamera.Instance.yTransform.GetPosition();

        new YVariable(9996, true).Subtract(new YVariable(camPos.x, true));
        new YVariable(9995, true).Subtract(new YVariable(camPos.y, true));
        new YVariable(9994, true).Subtract(new YVariable(camPos.z, true));





        //var siny = new YVariable("Camera.rotation.sin.y");
        //var cosy = new YVariable("Camera.rotation.cos.y");
        //
        //var yawX = new YVariable(9994, true) * siny + new YVariable(9996, true) * cosy;
        //var yawZ = new YVariable(9994, true) * cosy - new YVariable(9996, true) * siny;
        //
        //
        //var cosX = new YVariable("Camera.rotation.cos.x");
        //var sinX = new YVariable("Camera.rotation.sin.x");
        //
        //var pitchY = new YVariable(9995, true) * cosX - yawZ * sinX;
        //var pitchZ = new YVariable(9995, true) * sinX + yawZ * cosX;
        //
        //
        //var sinz = new YVariable("Camera.rotation.sin.z");
        //var cosz = new YVariable("Camera.rotation.cos.z");
        //
        //var rollX = yawX * cosz - pitchY * sinz;
        //var rollY = yawX * sinz + pitchY * cosz;

        var rotatedPoint = YMainCamera.Instance.yTransform.GetRotation().Conjugate() * new YVector3(new YVariable(9996, true), new YVariable(9995, true), new YVariable(9994, true));



        new YVariable(9996, true).SetValue(rotatedPoint.x);
        new YVariable(9995, true).SetValue(rotatedPoint.y);
        new YVariable(9994, true).SetValue(rotatedPoint.z);
        
        
        new YVariable(9977, true).SetValue(YMathService.Get().Sqrt(new YVariable(9996, true) * new YVariable(9996, true)
            + new YVariable(9995, true) * new YVariable(9995, true)
            + new YVariable(9994, true) * new YVariable(9994, true)));
        
        
        new Condition(new YVariable(9994, true), new YFloat(0.01f, true), ItemCompare.Operation.More)
            .Then(() =>
            {
                new YVariable(9996, true).SetValue(YMainCamera.Instance.GetFocalLen() * new YVariable(9996, true) / new YVariable(9994, true));
                new YVariable(9995, true).SetValue(YMainCamera.Instance.GetFocalLen() * new YVariable(9995, true) / new YVariable(9994, true));
                new YVariable(9979, true).SetValue(1);
            })
            .Else(() =>
            {
                new YVariable(9979, true).SetValue(-1);
                new YVariable(9996, true).Multiply(1000);
                new YVariable(9995, true).Multiply(1000);
            });
        
        new YVariable(9994, true).SetValue(new YVariable(9979, true) * new YVariable(9977, true));

        new Spawn(689, false, 0, new Dictionary<int, int> { { 9000, 9996 }, { 9001, 9995 }, { 9997, 9993 } });

        foreach (var trigger in YGameManager.Instance.StopRecordPool(removeNonFirstLevel: true))
        {
            trigger.AddGroup(localToCameraPosTranslateFunctionID);
        }
    }
}
