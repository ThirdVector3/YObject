
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class YLightSource : YMonoBehaviour
{
    public enum LightType
    {
        Point,
        Directional,
        Ambient
    }
    [SerializeField] private LightType type;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float intensity = 1f;
    [SerializeField] private float distance = 10f;

    private YVariable intensityTimer;
    private YVariable distanceTimer;

    public void SetIntensity(YVariable intensity)
    {
        this.intensity = intensity.IsFloat() ? YIDsManager.Instance.GetMemoryValue(intensity).Item2 : YIDsManager.Instance.GetMemoryValue(intensity).Item1;
        intensityTimer.SetValue(intensity);
    }
    public YVariable GetIntensity()
    {
        return intensityTimer + 0;
    }

    public override void Tick()
    {
        //GetComponent<YTransform>().Rotate(1f, 2f, 3f);
       
        
        //GetComponent<YTransform>().SetPosition(23, 23, YMathService.Get().SinRad(new YVariable("Time.time")) * 3);
    }
    public override void Init()
    {
        intensityTimer = new YFloat(intensity);
        distanceTimer = new YFloat(distance);

        YGameManager.Instance.RecordPool();

        List<YTrigger> triggers = new List<YTrigger>();

        // 999 998 9999 xyz1
        // 997 996 9998 xyz2
        // 995 994 9997 xyz3

        if (type != LightType.Ambient)
        {
            // A = xyz2 - xyz1

            triggers.Add(new ItemEdit(9996, true, ItemEdit.Operation.Equals, 1, 997, true, 999, true, ItemEdit.Operation.Subtract));
            triggers.Add(new ItemEdit(9995, true, ItemEdit.Operation.Equals, 1, 996, true, 998, true, ItemEdit.Operation.Subtract));
            triggers.Add(new ItemEdit(9994, true, ItemEdit.Operation.Equals, 1, 9998, true, 9999, true, ItemEdit.Operation.Subtract));

            // B = xyz3 - xyz1

            triggers.Add(new ItemEdit(9993, true, ItemEdit.Operation.Equals, 1, 995, true, 999, true, ItemEdit.Operation.Subtract));
            triggers.Add(new ItemEdit(9992, true, ItemEdit.Operation.Equals, 1, 994, true, 998, true, ItemEdit.Operation.Subtract));
            triggers.Add(new ItemEdit(9991, true, ItemEdit.Operation.Equals, 1, 9997, true, 9999, true, ItemEdit.Operation.Subtract));

            // cross = (Ay * Bz - Az * By, Az * Bx - Ax * Bz, Ax * By - Ay * Bx)

            triggers.Add(new ItemEdit(9990, true, ItemEdit.Operation.Equals, 1, 9995, true, 9991, true, ItemEdit.Operation.Multiply));
            triggers.Add(new ItemEdit(9990, true, ItemEdit.Operation.Subtract, 1, 9994, true, 9992, true, ItemEdit.Operation.Multiply));

            triggers.Add(new ItemEdit(9989, true, ItemEdit.Operation.Equals, 1, 9994, true, 9993, true, ItemEdit.Operation.Multiply));
            triggers.Add(new ItemEdit(9989, true, ItemEdit.Operation.Subtract, 1, 9996, true, 9991, true, ItemEdit.Operation.Multiply));

            triggers.Add(new ItemEdit(9988, true, ItemEdit.Operation.Equals, 1, 9996, true, 9992, true, ItemEdit.Operation.Multiply));
            triggers.Add(new ItemEdit(9988, true, ItemEdit.Operation.Subtract, 1, 9995, true, 9993, true, ItemEdit.Operation.Multiply));

            // normal = cross / sqrt(cross.x**2 + cross.y**2 + cross.z**2)

            triggers.Add(new ItemEdit(9986, true, ItemEdit.Operation.Equals, 1, 9990, true, 9990, true, ItemEdit.Operation.Multiply));
            triggers.Add(new ItemEdit(9986, true, ItemEdit.Operation.Add, 1, 9989, true, 9989, true, ItemEdit.Operation.Multiply));
            triggers.Add(new ItemEdit(9986, true, ItemEdit.Operation.Add, 1, 9988, true, 9988, true, ItemEdit.Operation.Multiply));

            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Equals, 1.04f));

            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Add, 1, 9986, true, 9987, true, ItemEdit.Operation.Divide));
            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Multiply, 0.5f));
            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Add, 1, 9986, true, 9987, true, ItemEdit.Operation.Divide));
            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Multiply, 0.5f));
            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Add, 1, 9986, true, 9987, true, ItemEdit.Operation.Divide));
            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Multiply, 0.5f));
            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Add, 1, 9986, true, 9987, true, ItemEdit.Operation.Divide));
            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Multiply, 0.5f));
            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Add, 1, 9986, true, 9987, true, ItemEdit.Operation.Divide));
            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Multiply, 0.5f));
            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Add, 1, 9986, true, 9987, true, ItemEdit.Operation.Divide));
            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Multiply, 0.5f));
            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Add, 1, 9986, true, 9987, true, ItemEdit.Operation.Divide));
            triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Multiply, 0.5f));


            triggers.Add(new ItemEdit(9990, true, ItemEdit.Operation.Divide, 1, 9987, true, 0, true, ItemEdit.Operation.Add));
            triggers.Add(new ItemEdit(9989, true, ItemEdit.Operation.Divide, 1, 9987, true, 0, true, ItemEdit.Operation.Add));
            triggers.Add(new ItemEdit(9988, true, ItemEdit.Operation.Divide, 1, 9987, true, 0, true, ItemEdit.Operation.Add));

            if (type == LightType.Directional)
            {
                // light += (-dot(forward, normal) + 1) / 2
                if (GetComponent<YTransform>() == null)
                {
                    triggers.Add(new ItemEdit(9985, true, ItemEdit.Operation.Equals, -transform.forward.x, 9990, true, 0, true, ItemEdit.Operation.Add));
                    triggers.Add(new ItemEdit(9985, true, ItemEdit.Operation.Add, -transform.forward.y, 9989, true, 0, true, ItemEdit.Operation.Add));
                    triggers.Add(new ItemEdit(9985, true, ItemEdit.Operation.Add, -transform.forward.z, 9988, true, 0, true, ItemEdit.Operation.Add));
                }
                else
                {
                    YTransform yTransform = GetComponent<YTransform>();
                    yTransform.Init();

                    //triggers.AddRange(yTransform.GetSin(9979, 9978, 9977));
                    //triggers.AddRange(yTransform.GetCos(9976, 9975, 9974));
                    //
                    //triggers.Add(new ItemEdit(9973, true, ItemEdit.Operation.Equals, 1, 9978, true, 9976, true, ItemEdit.Operation.Multiply));
                    ////triggers.Add(new ItemEdit(9972, true, ItemEdit.Operation.Equals, -1, 9979, true, 0, true, ItemEdit.Operation.Multiply));
                    //triggers.Add(new ItemEdit(9971, true, ItemEdit.Operation.Equals, 1, 9976, true, 9975, true, ItemEdit.Operation.Multiply));

                    var forward = yTransform.GetRotation() * new YVector3(0,0,-1f);


                    //triggers.Add(new ItemEdit(9985, true, ItemEdit.Operation.Equals, -1, 9973, true, 9990, true, ItemEdit.Operation.Multiply));
                    //triggers.Add(new ItemEdit(9985, true, ItemEdit.Operation.Add, 1, 9979, true, 9989, true, ItemEdit.Operation.Multiply));
                    //triggers.Add(new ItemEdit(9985, true, ItemEdit.Operation.Add, -1, 9971, true, 9988, true, ItemEdit.Operation.Multiply));

                    triggers.Add(new ItemEdit(9985, true, ItemEdit.Operation.Equals, 1, forward.x, true, 9990, true, ItemEdit.Operation.Multiply));
                    triggers.Add(new ItemEdit(9985, true, ItemEdit.Operation.Add, 1, forward.y, true, 9989, true, ItemEdit.Operation.Multiply));
                    triggers.Add(new ItemEdit(9985, true, ItemEdit.Operation.Add, 1, forward.z, true, 9988, true, ItemEdit.Operation.Multiply));
                }

                //triggers.Add(new ItemEdit(9985, true, ItemEdit.Operation.Add, 1));
                //triggers.Add(new ItemEdit(9985, true, ItemEdit.Operation.Divide, 2));
                triggers.Add(new ItemCompare(9985, 0, true, true, 1, 0, ItemCompare.Operation.Less, new YTrigger[] { new ItemEdit(9985, true, ItemEdit.Operation.Equals, 0) }, new YTrigger[0]));

                triggers.Add(new ItemEdit(9980, true, ItemEdit.Operation.Add, 1, 9985, true, intensityTimer, true, ItemEdit.Operation.Multiply));
                triggers.Add(new ItemEdit(9981, true, ItemEdit.Operation.Add, 1, 9985, true, intensityTimer, true, ItemEdit.Operation.Multiply));
                triggers.Add(new ItemEdit(9982, true, ItemEdit.Operation.Add, 1, 9985, true, intensityTimer, true, ItemEdit.Operation.Multiply));
            }
            else if (type == LightType.Point)
            {
                // 999 998 9999 xyz1
                // 997 996 9998 xyz2
                // 995 994 9997 xyz3

                for (int i = 0; i < 3; i++)
                {
                    if (GetComponent<YTransform>() == null)
                    {
                        triggers.Add(new ItemEdit(9000, true, ItemEdit.Operation.Equals, transform.position.x));
                        triggers.Add(new ItemEdit(9973, true, ItemEdit.Operation.Equals, 1, 9000, true, 999 - 2 * i, true, ItemEdit.Operation.Subtract));
                        triggers.Add(new ItemEdit(9000, true, ItemEdit.Operation.Equals, transform.position.y));
                        triggers.Add(new ItemEdit(9972, true, ItemEdit.Operation.Equals, 1, 9000, true, 998 - 2 * i, true, ItemEdit.Operation.Subtract));
                        triggers.Add(new ItemEdit(9000, true, ItemEdit.Operation.Equals, transform.position.z));
                        triggers.Add(new ItemEdit(9971, true, ItemEdit.Operation.Equals, 1, 9000, true, 9999 - i, true, ItemEdit.Operation.Subtract));
                    }
                    else
                    {
                        GetComponent<YTransform>().Init();
                        triggers.Add(new ItemEdit(9973, true, ItemEdit.Operation.Equals, 1, YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.position.x"), true, 999 - 2 * i, true, ItemEdit.Operation.Subtract));
                        triggers.Add(new ItemEdit(9972, true, ItemEdit.Operation.Equals, 1, YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.position.y"), true, 998 - 2 * i, true, ItemEdit.Operation.Subtract));
                        triggers.Add(new ItemEdit(9971, true, ItemEdit.Operation.Equals, 1, YIDsManager.Instance.GetIdByName(gameObject.GetInstanceID() + ".transform.position.z"), true, 9999 - i, true, ItemEdit.Operation.Subtract));
                    }

                    triggers.Add(new ItemEdit(9986, true, ItemEdit.Operation.Equals, 1, 9973, true, 9973, true, ItemEdit.Operation.Multiply));
                    triggers.Add(new ItemEdit(9986, true, ItemEdit.Operation.Add, 1, 9972, true, 9972, true, ItemEdit.Operation.Multiply));
                    triggers.Add(new ItemEdit(9986, true, ItemEdit.Operation.Add, 1, 9971, true, 9971, true, ItemEdit.Operation.Multiply));

                    triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Equals, 1.04f));

                    triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Add, 1, 9986, true, 9987, true, ItemEdit.Operation.Divide));
                    triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Multiply, 0.5f));
                    triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Add, 1, 9986, true, 9987, true, ItemEdit.Operation.Divide));
                    triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Multiply, 0.5f));
                    triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Add, 1, 9986, true, 9987, true, ItemEdit.Operation.Divide));
                    triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Multiply, 0.5f));
                    triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Add, 1, 9986, true, 9987, true, ItemEdit.Operation.Divide));
                    triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Multiply, 0.5f));

                    triggers.Add(new ItemEdit(9973, true, ItemEdit.Operation.Divide, 1, 9987, true, 0, true, ItemEdit.Operation.Add));
                    triggers.Add(new ItemEdit(9972, true, ItemEdit.Operation.Divide, 1, 9987, true, 0, true, ItemEdit.Operation.Add));
                    triggers.Add(new ItemEdit(9971, true, ItemEdit.Operation.Divide, 1, 9987, true, 0, true, ItemEdit.Operation.Add));


                    triggers.Add(new ItemEdit(9985 - i, true, ItemEdit.Operation.Equals, 1, 9973, true, 9990, true, ItemEdit.Operation.Multiply));
                    triggers.Add(new ItemEdit(9985 - i, true, ItemEdit.Operation.Add, 1, 9972, true, 9989, true, ItemEdit.Operation.Multiply));
                    triggers.Add(new ItemEdit(9985 - i, true, ItemEdit.Operation.Add, 1, 9971, true, 9988, true, ItemEdit.Operation.Multiply));

                    //triggers.Add(new ItemEdit(9985 - i, true, ItemEdit.Operation.Add, 1));
                    //triggers.Add(new ItemEdit(9985 - i, true, ItemEdit.Operation.Divide, 2));
                    triggers.Add(new ItemCompare(9985 - i, 0, true, true, 1, 0, ItemCompare.Operation.Less, new YTrigger[] { new ItemEdit(9985 - i, true, ItemEdit.Operation.Equals, 0) }, new YTrigger[0]));

                    triggers.Add(new ItemEdit(9987, true, ItemEdit.Operation.Divide, 1, distanceTimer, true, 0, true, ItemEdit.Operation.Add));
                    triggers.Add(new ItemCompare(9987, 0, true, true, 1, 1, ItemCompare.Operation.More, new YTrigger[] { new ItemEdit(9987, true, ItemEdit.Operation.Equals, 1) }, new YTrigger[0]));

                    triggers.Add(new ItemEdit(9000, true, ItemEdit.Operation.Equals, 1));
                    triggers.Add(new ItemEdit(9985 - i, true, ItemEdit.Operation.Multiply, 1, 9000, true, 9987, true, ItemEdit.Operation.Subtract));
                }
                triggers.Add(new ItemEdit(9980, true, ItemEdit.Operation.Add, 1, 9985, true, intensityTimer, true, ItemEdit.Operation.Multiply));
                triggers.Add(new ItemEdit(9981, true, ItemEdit.Operation.Add, 1, 9984, true, intensityTimer, true, ItemEdit.Operation.Multiply));
                triggers.Add(new ItemEdit(9982, true, ItemEdit.Operation.Add, 1, 9983, true, intensityTimer, true, ItemEdit.Operation.Multiply));
            }



        }
        else
        {
            triggers.Add(new ItemEdit(9980, true, ItemEdit.Operation.Add, 1, intensityTimer, true, 0, true, ItemEdit.Operation.Add));
            triggers.Add(new ItemEdit(9981, true, ItemEdit.Operation.Add, 1, intensityTimer, true, 0, true, ItemEdit.Operation.Add));
            triggers.Add(new ItemEdit(9982, true, ItemEdit.Operation.Add, 1, intensityTimer, true, 0, true, ItemEdit.Operation.Add));
        }

        foreach (var trigger in YGameManager.Instance.StopRecordPool(removeNonFirstLevel: true))
            trigger.AddGroup(846);
    }

    public float[] GetLightLevel(Vector3[] positions)
    {
        var normal = Vector3.Cross(positions[1] - positions[0], positions[2] - positions[0]);
        normal.Normalize();
        if (type == LightType.Directional)
        {
            var light = Mathf.Clamp01(Vector3.Dot(-normal, transform.forward)) * intensity;
            return new float[] { light, light, light };
        }
        else if (type == LightType.Point)
        {
            var light1 = Mathf.Clamp01(Vector3.Dot(normal, (transform.position - positions[0]).normalized)) * (1 - Mathf.Min((transform.position - positions[0]).magnitude / distance, 1));
            var light2 = Mathf.Clamp01(Vector3.Dot(normal, (transform.position - positions[1]).normalized)) * (1 - Mathf.Min((transform.position - positions[1]).magnitude / distance, 1));
            var light3 = Mathf.Clamp01(Vector3.Dot(normal, (transform.position - positions[2]).normalized)) * (1 - Mathf.Min((transform.position - positions[2]).magnitude / distance, 1));
            return new float[] { light1, light2, light3 };
        }
        else if (type == LightType.Ambient)
        {
            var light = intensity;
            return new float[] { light, light, light };
        }
        return null;
    }

    public void BakeAllLights()
    {
        foreach (var meshRenderer in FindObjectsByType<YMeshRenderer>(sortMode: FindObjectsSortMode.None))
        {
            meshRenderer.BakeLighting();
        }
    }

    private void OnDrawGizmos()
    {
        if (type == LightType.Point)
            Gizmos.DrawWireSphere(transform.position, distance);
    }
}
