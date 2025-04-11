using System.Globalization;
using UnityEditor;
using UnityEngine;
public class ItemEdit : YTrigger
{
    private int editID;
    private Operation operation;
    private int setID1;
    private int setID2;
    private Operation operation2;
    private bool isEditFloat;
    private bool isSetFloat;
    private bool isSetFloat2;
    private float multiplier;
    private Operation2 operation3;
    private Operation3 operation4;
    public enum Operation
    {
        Equals,
        Add,
        Subtract,
        Multiply,
        Divide
    }
    public enum Operation2
    {
        None,
        Round,
        Floor,
        Ceil
    }
    public enum Operation3
    {
        None,
        Absolute,
        Negative
    }

    public ItemEdit(int editID, bool isEditFloat, Operation operation, float number)
    {
        this.editID = editID;
        this.operation = operation;
        this.setID1 = 0;
        this.setID2 = 0;
        this.operation2 = (Operation)1;
        this.isEditFloat = isEditFloat;
        this.isSetFloat = false;
        this.isSetFloat2 = false;
        this.multiplier = number;
        this.operation3 = 0;
        this.operation4 = 0;
    }
    public ItemEdit(int editID, bool isEditFloat, Operation operation, float multiplier, int setID1, bool isSetFloat, int setID2, bool isSetFloat2, Operation operation2)
    {
        this.editID = editID;
        this.operation = operation;
        this.setID1 = setID1;
        this.setID2 = setID2;
        this.operation2 = operation2;
        this.isEditFloat = isEditFloat;
        this.isSetFloat = isSetFloat;
        this.isSetFloat2 = isSetFloat2;
        this.multiplier = multiplier;
        this.operation3 = 0;
        this.operation4 = 0;
    }

    public ItemEdit(int editID, bool isEditFloat, Operation operation, float multiplier, int setID1, bool isSetFloat, int setID2, bool isSetFloat2, Operation operation2, Operation2 operation3, Operation3 operation4)
    {
        this.editID = editID;
        this.operation = operation;
        this.setID1 = setID1;
        this.setID2 = setID2;
        this.operation2 = operation2;
        this.isEditFloat = isEditFloat;
        this.isSetFloat = isSetFloat;
        this.isSetFloat2 = isSetFloat2;
        this.multiplier = multiplier;
        this.operation3 = operation3;
        this.operation4 = operation4;
    }

    public override void Activate()
    {
        var memVal1 = YGameManager.Instance.IDsManager.GetMemoryValue(setID1);
        var memVal2 = YGameManager.Instance.IDsManager.GetMemoryValue(setID2);

        float result = 0;

        if (setID1 != 0)
        {
            if (isSetFloat)
                result += memVal1.Item2;
            else
                result += memVal1.Item1;
        }

        if (setID2 != 0)
        {
            if (isSetFloat2)
            {
                switch (operation2)
                {
                    case Operation.Equals:
                        break;
                    case Operation.Add:
                        result += memVal2.Item2;
                        break;
                    case Operation.Subtract:
                        result -= memVal2.Item2;
                        break;
                    case Operation.Multiply:
                        result *= memVal2.Item2;
                        break;
                    case Operation.Divide:
                        result /= memVal2.Item2;
                        break;
                }
            }
            else
            {
                switch (operation2)
                {
                    case Operation.Equals:
                        break;
                    case Operation.Add:
                        result += memVal2.Item1;
                        break;
                    case Operation.Subtract:
                        result -= memVal2.Item1;
                        break;
                    case Operation.Multiply:
                        result *= memVal2.Item1;
                        break;
                    case Operation.Divide:
                        result /= memVal2.Item1;
                        break;
                }
            }
        }

        if (setID1 == 0 && setID2 == 0)
            result = 1;
        result *= multiplier;

        switch (operation3)
        {
            case Operation2.None:
                break;
            case Operation2.Round:
                result = Mathf.Round(result);
                break;
            case Operation2.Floor:
                result = Mathf.Floor(result);
                break;
            case Operation2.Ceil:
                result = Mathf.Ceil(result);
                break;
        }

        switch (operation4)
        {
            case Operation3.None:
                break;
            case Operation3.Absolute:
                result = Mathf.Abs(result);
                break;
            case Operation3.Negative:
                result = -Mathf.Abs(result);
                break;
        }

        if (isEditFloat)
        {
            switch (operation)
            {
                case Operation.Equals:
                    YGameManager.Instance.IDsManager.SetMemoryValue(editID, result);
                    break;
                case Operation.Add:
                    var editVal = YGameManager.Instance.IDsManager.GetMemoryValue(editID);
                    YGameManager.Instance.IDsManager.SetMemoryValue(editID, editVal.Item2 + result);
                    break;
                case Operation.Subtract:
                    editVal = YGameManager.Instance.IDsManager.GetMemoryValue(editID);
                    YGameManager.Instance.IDsManager.SetMemoryValue(editID, editVal.Item2 - result);
                    break;
                case Operation.Multiply:
                    editVal = YGameManager.Instance.IDsManager.GetMemoryValue(editID);
                    YGameManager.Instance.IDsManager.SetMemoryValue(editID, editVal.Item2 * result);
                    break;
                case Operation.Divide:
                    editVal = YGameManager.Instance.IDsManager.GetMemoryValue(editID);
                    YGameManager.Instance.IDsManager.SetMemoryValue(editID, editVal.Item2 / result);
                    break;
            }
        }
        else
        {
            switch (operation)
            {
                case Operation.Equals:
                    YGameManager.Instance.IDsManager.SetMemoryValue(editID, (int)result);
                    break;
                case Operation.Add:
                    var editVal = YGameManager.Instance.IDsManager.GetMemoryValue(editID);
                    YGameManager.Instance.IDsManager.SetMemoryValue(editID, (int)(editVal.Item2 + result));
                    break;
                case Operation.Subtract:
                    editVal = YGameManager.Instance.IDsManager.GetMemoryValue(editID);
                    YGameManager.Instance.IDsManager.SetMemoryValue(editID, (int)(editVal.Item2 - result));
                    break;
                case Operation.Multiply:
                    editVal = YGameManager.Instance.IDsManager.GetMemoryValue(editID);
                    YGameManager.Instance.IDsManager.SetMemoryValue(editID, (int)(editVal.Item2 * result));
                    break;
                case Operation.Divide:
                    editVal = YGameManager.Instance.IDsManager.GetMemoryValue(editID);
                    YGameManager.Instance.IDsManager.SetMemoryValue(editID, (int)(editVal.Item2 / result));
                    break;
            }
        }
    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        return $"1,3619,2,{pos.Value.x},3,{pos.Value.y}{GetGroupsString(groups, groupsParent)},155,1,62,1,87,1,36,1,80,{setID1},95,{setID2},476,{(isSetFloat ? 2 : 1)},477,{(isSetFloat2 ? 2 : 1)},478,{(isEditFloat ? 2 : 1)},51,{editID},479,{multiplier.ToString(CultureInfo.InvariantCulture)},{$"480,{(int)operation},"}481,{(int)operation2},482,3,{$"485,{(int)operation3}"},{$"578,{(int)operation4}"};";
    }
}
