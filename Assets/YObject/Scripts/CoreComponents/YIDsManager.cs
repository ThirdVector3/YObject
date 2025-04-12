using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
public class YIDsManager
{
    private int[] standartTakenGroups = new int[]
    {
        689,
        748,
        1000,
        1001,
        1002,
        1003,
        6000,
        6001,
        6002,
        6003,
        6004,
        6005,
        6006
    };

    private (int, float)[] memory = new (int, float)[10000];

    private Dictionary<string, (int, bool)> globalVariables = new Dictionary<string, (int, bool)>();
    private Dictionary<string, Dictionary<string, (int, bool)>> groupsVariables = new Dictionary<string, Dictionary<string, (int, bool)>>();
    private List<int> globalPickedGroups = new List<int>();
    private Dictionary<string, List<int>> groupsPickedGroups = new Dictionary<string, List<int>>();
    private List<int> globalPickedGradientIDs = new List<int>();
    private Dictionary<string, List<int>> groupsPickedGradientIDs = new Dictionary<string, List<int>>();



    private string currentGroupName = null;
    public YIDsManager()
    {
        globalVariables = new Dictionary<string, (int, bool)>()
        {
            {"Camera.position.x", (1, true) },
            {"Camera.position.y", (2, true) },
            {"Camera.position.z", (3, true) },

            {"Camera.rotation.x", (4, true) },
            {"Camera.rotation.y", (5, true) },
            {"Camera.rotation.z", (11, true) },

            {"Camera.rotation.sin.x", (6, true) },
            {"Camera.rotation.sin.y", (7, true) },
            {"Camera.rotation.sin.z", (12, true) },

            {"Camera.rotation.cos.x", (8, true) },
            {"Camera.rotation.cos.y", (9, true) },
            {"Camera.rotation.cos.z", (13, true) },

            {"Camera.focalLen", (16, true) },

            {"PI", (10, true) },

            {"Input.P1Left", (17, true) },
            {"Input.P1Right", (18, true) },
            {"Input.P1Up", (19, true) },

            {"Input.P2Left", (20, true) },
            {"Input.P2Right", (21, true) },
            {"Input.P2Up", (22, true) },

            {"ZERO", (23, true) },

            {"Time.deltaTime", (14, true) },
            {"Time.time", (15, true) },
        };
        globalPickedGroups = standartTakenGroups.ToList();


    }
    public void InitGroups(string[] groups)
    {
        foreach (var group in groups)
        {
            groupsVariables.Add(group, new Dictionary<string, (int, bool)>());
            groupsPickedGroups.Add(group, new List<int>());
            groupsPickedGradientIDs.Add(group, new List<int>());
        }
    }

    public void SetCurrentGroupName(string groupName)
    {
        currentGroupName = groupName;
    }
    public void AddVariable(string name, int id, bool isFloat)
    {
        AddVariable(name, id, isFloat, currentGroupName);
    }
    public void AddVariable(string name, int id, bool isFloat, string groupName)
    {
        if (globalVariables.ContainsKey(name))
            throw new System.Exception("More than one variable with same name");


        if (groupName == null)
            globalVariables.Add(name, (id, isFloat));
        else
        {
            if (!groupsVariables.ContainsKey(groupName))
                groupsVariables.Add(groupName, new Dictionary<string, (int, bool)>());

            if (groupsVariables[groupName].ContainsKey(name))
                throw new System.Exception("More than one variable with same name");

            groupsVariables[groupName].Add(name, (id, isFloat));
        }
    }
    public void RemoveVariable(string name)
    {
        RemoveVariable(name, currentGroupName);
    }
    public void RemoveVariable(string name, string groupName)
    {
        if (groupName == null)
        {
            if (!globalVariables.ContainsKey(name))
                throw new System.Exception("No variable with that name");

            globalVariables.Remove(name);
        }
        else
        {
            if (!groupsVariables.ContainsKey(groupName) || !groupsVariables[groupName].ContainsKey(name))
                throw new System.Exception("No variable with that name");

            groupsVariables[groupName].Remove(name);
        }
    }
    public void SetMemoryValue(int id, float value)
    {
        memory[id] = (memory[id].Item1, value);
    }
    public void SetMemoryValue(int id, int value)
    {
        memory[id] = (value, memory[id].Item2);
    }
    public void SetMemoryValueByName(string name, float value)
    {
        SetMemoryValueByName(name, value, currentGroupName);
    }
    public void SetMemoryValueByName(string name, int value)
    {
        SetMemoryValueByName(name, value, currentGroupName);
    }
    public void SetMemoryValueByName(string name, float value, string groupName)
    {
        if (GetIsFloatByName(name, groupName))
        {
            memory[GetIdByName(name, groupName)] = (memory[GetIdByName(name, groupName)].Item1, value);
        }
    }
    public void SetMemoryValueByName(string name, int value, string groupName)
    {
        if (!GetIsFloatByName(name, groupName))
        {
            memory[GetIdByName(name, groupName)] = (value, memory[GetIdByName(name, groupName)].Item1);
        }
    }
    public (int, float) GetMemoryValue(int id)
    {
        return memory[id];
    }
    public (int, float) GetMemoryValueByName(string name)
    {
        return GetMemoryValueByName(name, currentGroupName);
    }
    public (int, float) GetMemoryValueByName(string name, string groupName)
    {
        return GetMemoryValue(GetIdByName(name, groupName));
    }
    public int GetIdByName(string name)
    {
        return GetIdByName(name, currentGroupName);
    }
    public int GetIdByName(string name, string groupName)
    {
        if (groupName == null)
            return globalVariables[name].Item1;

        if (globalVariables.ContainsKey(name))
            return globalVariables[name].Item1;

        return groupsVariables[groupName][name].Item1;
    }
    public bool GetIsFloatByName(string name)
    {
        return GetIsFloatByName(name, currentGroupName);
    }
    public bool GetIsFloatByName(string name, string groupName)
    {
        if (groupName == null)
            return globalVariables[name].Item2;

        if (globalVariables.ContainsKey(name))
            return globalVariables[name].Item2;

        return groupsVariables[groupName][name].Item2;
    }
    public int GetFreeIdInt()
    {
        return GetFreeIdInt(currentGroupName);
    }
    public int GetFreeIdInt(string groupName)
    {
        List<int> allIds = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIds.Add(i);
        }
        foreach (var variable in globalVariables.Values)
        {
            if (variable.Item2 == false && allIds.Contains(variable.Item1))
            {
                allIds.Remove(variable.Item1);
            }
        }
        if (groupName != null)
        {
            foreach (var variable in groupsVariables[groupName].Values)
            {
                if (variable.Item2 == false && allIds.Contains(variable.Item1))
                {
                    allIds.Remove(variable.Item1);
                }
            }
        }

        return allIds.Min();
    }
    public int GetFreeIdFloat()
    {
        return GetFreeIdFloat(currentGroupName);
    }
    public int GetFreeIdFloat(string groupName)
    {
        List<int> allIds = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIds.Add(i);
        }
        foreach (var variable in globalVariables.Values)
        {
            if (variable.Item2 == true && allIds.Contains(variable.Item1))
            {
                allIds.Remove(variable.Item1);
            }
        }
        if (groupName != null)
        {
            foreach (var variable in groupsVariables[groupName].Values)
            {
                if (variable.Item2 == true && allIds.Contains(variable.Item1))
                {
                    allIds.Remove(variable.Item1);
                }
            }
        }
        return allIds.Min();
    }
    public int GetFreeGroup()
    {
        return GetFreeGroup(currentGroupName);
    }
    public int GetFreeGroup(string groupName)
    {
        List<int> allIds = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIds.Add(i);
        }
        foreach (var group in globalPickedGroups)
        {
            if (allIds.Contains(group))
                allIds.Remove(group);
        }
        if (groupName != null)
        {
            foreach (var group in groupsPickedGroups[groupName])
            {
                if (allIds.Contains(group))
                    allIds.Remove(group);
            }
        }

        return allIds.Min();
    }
    public void AddGroup(int group)
    {
        AddGroup(group, currentGroupName);
    }
    public void AddGroup(int group, string groupName)
    {
        if (groupName == null)
        {
            if (!globalPickedGroups.Contains(group))
                globalPickedGroups.Add(group);
        }
        else
        {
            if (!globalPickedGroups.Contains(group) && !groupsPickedGroups[groupName].Contains(group))
                groupsPickedGroups[groupName].Add(group);
        }
    }
    public int GetFreeGradient()
    {
        return GetFreeGradient(currentGroupName);
    }
    public int GetFreeGradient(string groupName)
    {
        List<int> allIds = new List<int>();
        for (int i = 1; i < 1000; i++)
        {
            allIds.Add(i);
        }
        foreach (var group in globalPickedGradientIDs)
        {
            if (allIds.Contains(group))
                allIds.Remove(group);
        }
        if (groupName != null)
        {
            foreach (var group in groupsPickedGradientIDs[groupName])
            {
                if (allIds.Contains(group))
                    allIds.Remove(group);
            }
        }

        return allIds.Min();
    }
    public void AddGradient(int group)
    {
        AddGradient(group, currentGroupName);
    }
    public void AddGradient(int group, string groupName)
    {
        if (groupName == null)
        {
            if (!globalPickedGradientIDs.Contains(group))
                globalPickedGradientIDs.Add(group);
        }
        else
        {
            if (!globalPickedGradientIDs.Contains(group) && !groupsPickedGradientIDs[groupName].Contains(group))
                groupsPickedGradientIDs[groupName].Add(group);
        }
    }
    public int GetFreeIdFloatAndGroup()
    {
        return GetFreeIdFloatAndGroup(currentGroupName);
    }
    public int GetFreeIdFloatAndGroup(string groupName)
    {
        List<int> allIdsGroup = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIdsGroup.Add(i);
        }
        foreach (var group in globalPickedGroups)
        {
            if (allIdsGroup.Contains(group))
                allIdsGroup.Remove(group);
        }
        if (groupName != null)
        {
            foreach (var group in groupsPickedGroups[groupName])
            {
                if (allIdsGroup.Contains(group))
                    allIdsGroup.Remove(group);
            }
        }


        List<int> allIdsFloat = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIdsFloat.Add(i);
        }
        foreach (var variable in globalVariables.Values)
        {
            if (variable.Item2 == true && allIdsFloat.Contains(variable.Item1))
            {
                allIdsFloat.Remove(variable.Item1);
            }
        }
        if (groupName != null)
        {
            foreach (var variable in groupsVariables[groupName].Values)
            {
                if (variable.Item2 == true && allIdsFloat.Contains(variable.Item1))
                {
                    allIdsFloat.Remove(variable.Item1);
                }
            }
        }

        while (allIdsGroup.Count > 0)
        {
            if (allIdsFloat.Contains(allIdsGroup.Min()))
                return allIdsGroup.Min();

            allIdsGroup.Remove(allIdsGroup.Min());
        }
        return 9999;
    }
    public int GetFreeIdIntAndGroup()
    {
        return GetFreeIdIntAndGroup(currentGroupName);
    }
    public int GetFreeIdIntAndGroup(string groupName)
    {
        List<int> allIdsGroup = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIdsGroup.Add(i);
        }
        foreach (var group in globalPickedGroups)
        {
            if (allIdsGroup.Contains(group))
                allIdsGroup.Remove(group);
        }
        if (groupName != null)
        {
            foreach (var group in groupsPickedGroups[groupName])
            {
                if (allIdsGroup.Contains(group))
                    allIdsGroup.Remove(group);
            }
        }

        List<int> allIdsInt = new List<int>();
        for (int i = 500; i < 10000; i++)
        {
            allIdsInt.Add(i);
        }
        foreach (var variable in globalVariables.Values)
        {
            if (variable.Item2 == false && allIdsInt.Contains(variable.Item1))
            {
                allIdsInt.Remove(variable.Item1);
            }
        }
        if (groupName != null)
        {
            foreach (var variable in groupsVariables[groupName].Values)
            {
                if (variable.Item2 == false && allIdsInt.Contains(variable.Item1))
                {
                    allIdsInt.Remove(variable.Item1);
                }
            }
        }


        while (allIdsGroup.Count > 0)
        {
            if (allIdsInt.Contains(allIdsGroup.Min()))
                return allIdsGroup.Min();

            allIdsGroup.Remove(allIdsGroup.Min());
        }
        return 9999;
    }
}
