using UnityEngine;

public class YInputService : YService<YInputService>
{
    private YVariable previousP1LeftTMP;
    private YVariable previousP1RightTMP;
    private YVariable previousP1UpTMP;
    private YVariable previousP2LeftTMP;
    private YVariable previousP2RightTMP;
    private YVariable previousP2UpTMP;

    private YVariable previousP1Left;
    private YVariable previousP1Right;
    private YVariable previousP1Up;
    private YVariable previousP2Left;
    private YVariable previousP2Right;
    private YVariable previousP2Up;
    public override void Init()
    {
        previousP1Left = new YInt(0);
        previousP1Right = new YInt(0);
        previousP1Up = new YInt(0);
        previousP2Left = new YInt(0);
        previousP2Right = new YInt(0);
        previousP2Up = new YInt(0);

        previousP1LeftTMP = new YInt(0);
        previousP1RightTMP = new YInt(0);
        previousP1UpTMP = new YInt(0);
        previousP2LeftTMP = new YInt(0);
        previousP2RightTMP = new YInt(0);
        previousP2UpTMP = new YInt(0);
    }
    public override void Tick()
    {
        previousP1Left.SetValue(previousP1LeftTMP);
        previousP1Right.SetValue(previousP1RightTMP);
        previousP1Up.SetValue(previousP1UpTMP);
        previousP2Left.SetValue(previousP2LeftTMP);
        previousP2Right.SetValue(previousP2RightTMP);
        previousP2Up.SetValue(previousP2UpTMP);

        previousP1LeftTMP.SetValue(new YVariable("Input.P1Left"));
        previousP1RightTMP.SetValue(new YVariable("Input.P1Right"));
        previousP1UpTMP.SetValue(new YVariable("Input.P1Up"));
        previousP2LeftTMP.SetValue(new YVariable("Input.P2Left"));
        previousP2RightTMP.SetValue(new YVariable("Input.P2Right"));
        previousP2UpTMP.SetValue(new YVariable("Input.P2Up"));
    }

    public YVariable P1LeftDown()
    {
        return (previousP1Left != new YVariable("Input.P1Left")) * (new YVariable("Input.P1Left") == 1);
    }
    public YVariable P1LeftUp()
    {
        return (previousP1Left != new YVariable("Input.P1Left")) * (new YVariable("Input.P1Left") == 0);
    }
    public YVariable P1Left()
    {
        return new YVariable("Input.P1Left");
    }

    public YVariable P1RightDown()
    {
        return (previousP1Right != new YVariable("Input.P1Right")) * (new YVariable("Input.P1Right") == 1);
    }
    public YVariable P1RightUp()
    {
        return (previousP1Right != new YVariable("Input.P1Right")) * (new YVariable("Input.P1Right") == 0);
    }
    public YVariable P1Right()
    {
        return new YVariable("Input.P1Right");
    }

    public YVariable P1UpDown()
    {
        return (previousP1Up != new YVariable("Input.P1Up")) * (new YVariable("Input.P1Up") == 1);
    }
    public YVariable P1UpUp()
    {
        return (previousP1Up != new YVariable("Input.P1Up")) * (new YVariable("Input.P1Up") == 0);
    }
    public YVariable P1Up()
    {
        return new YVariable("Input.P1Up");
    }

    public YVariable P2LeftDown()
    {
        return (previousP2Left != new YVariable("Input.P2Left")) * (new YVariable("Input.P2Left") == 1);
    }
    public YVariable P2LeftUp()
    {
        return (previousP2Left != new YVariable("Input.P2Left")) * (new YVariable("Input.P2Left") == 0);
    }
    public YVariable P2Left()
    {
        return new YVariable("Input.P2Left");
    }

    public YVariable P2RightDown()
    {
        return (previousP2Right != new YVariable("Input.P2Right")) * (new YVariable("Input.P2Right") == 1);
    }
    public YVariable P2RightUp()
    {
        return (previousP2Right != new YVariable("Input.P2Right")) * (new YVariable("Input.P2Right") == 0);
    }
    public YVariable P2Right()
    {
        return new YVariable("Input.P2Right");
    }

    public YVariable P2UpDown()
    {
        return (previousP2Up != new YVariable("Input.P2Up")) * (new YVariable("Input.P2Up") == 1);
    }
    public YVariable P2UpUp()
    {
        return (previousP2Up != new YVariable("Input.P2Up")) * (new YVariable("Input.P2Up") == 0);
    }
    public YVariable P2Up()
    {
        return new YVariable("Input.P2Up");
    }
}