using System.Collections.Generic;

public static class YCommands
{
    public static YTrigger[] If(string ifStatement, YTrigger[] trueTriggers, YTrigger[] falseTriggers)
    {
        var splitted = ifStatement.Split(' ');
        if (splitted.Length != 3)
        {
            throw new System.Exception("Arguments must be like a == b");
        }

        float firstMultiplier = 1;
        float secondMultiplier = 1;

        bool isFloat1 = false;
        bool isFloat2 = false;

        int id1 = 0;
        int id2 = 0;

        ItemCompare.Operation operation = ItemCompare.Operation.Equals;

        if (float.TryParse(splitted[0], out float float1))
            firstMultiplier = float1;
        else if (splitted[0].Contains("float(") || splitted[0].Contains("int("))
        {
            isFloat1 = splitted[0].Contains("float(");
            id1 = int.Parse(splitted[0].Substring(splitted[0].Contains("float(") ? 6 : 4, splitted[0].Length - 1 - (splitted[0].Contains("float(") ? 6 : 4)));
        }
        else
        {
            isFloat1 = YIDsManager.Instance.GetIsFloatByName(splitted[0]);
            id1 = YIDsManager.Instance.GetIdByName(splitted[0]);
        }

        if (float.TryParse(splitted[2], out float float2))
            secondMultiplier = float2;
        else if (splitted[2].Contains("float(") || splitted[2].Contains("int("))
        {
            isFloat2 = splitted[2].Contains("float(");
            id2 = int.Parse(splitted[2].Substring(splitted[2].Contains("float(") ? 6 : 4, splitted[2].Length - 1 - (splitted[2].Contains("float(") ? 6 : 4)));
        }
        else
        {
            isFloat2 = YIDsManager.Instance.GetIsFloatByName(splitted[2]);
            id2 = YIDsManager.Instance.GetIdByName(splitted[2]);
        }

        switch (splitted[1])
        {
            case "==":
                operation = ItemCompare.Operation.Equals;
                break;
            case "!=":
                operation = ItemCompare.Operation.NotEquals;
                break;
            case ">=":
                operation = ItemCompare.Operation.MoreOrEquals;
                break;
            case "<=":
                operation = ItemCompare.Operation.LessOrEquals;
                break;
            case ">":
                operation = ItemCompare.Operation.More;
                break;
            case "<":
                operation = ItemCompare.Operation.Less;
                break;
        }


        return new YTrigger[] { new ItemCompare(id1, id2, isFloat1, isFloat2, firstMultiplier, secondMultiplier, operation, trueTriggers, falseTriggers) };
    }
}