using System.Collections.Generic;
using UnityEngine;

public class TestComponent2 : YMonoBehaviour
{
    private YVariable speed;

    public override void Init()
    {
        speed = new YFloat(0.1f);
    }

    public override void Begin()
    {
        //return null;//new YTrigger[] { new SongTrigger(63, 0, 1, true, 0, 0, 0, 0) };

    }

    public override void Tick()
    {

        GetComponent<YTransform>().Translate(23, (new YFloat(0.1f) + speed).GetID(), 23);

        //triggers.Add(new ItemEdit(5001, true, ItemEdit.Operation.Equals, 100));
        //triggers.AddRange(YMath.Sqrt(5001, 5000));

        //triggers.AddRange(GetComponent<YTransform>().SetState(1));
        //triggers.AddRange(GetComponent<YTransform>().Rotate(0, rot, 0));

        //triggers.AddRange(yt.GetSin(9999, 9998, 9997));
        //triggers.AddRange(yt.GetCos(9996, 9995, 9994));
        //triggers.AddRange(new YTrigger[]
        //{
        //    new ItemEdit(9999, true, ItemEdit.Operation.Multiply, 3),
        //    new ItemEdit(9998, true, ItemEdit.Operation.Multiply, 3),
        //    new ItemEdit(9997, true, ItemEdit.Operation.Multiply, 3),
        //    new ItemEdit(9996, true, ItemEdit.Operation.Multiply, 3),
        //    new ItemEdit(9995, true, ItemEdit.Operation.Multiply, 3),
        //    new ItemEdit(9994, true, ItemEdit.Operation.Multiply, 3),
        //});
        //triggers.AddRange(GetComponent<YTransform>().SetPosition(9998, 23, 9995));


        //return triggers.ToArray();
    }
}