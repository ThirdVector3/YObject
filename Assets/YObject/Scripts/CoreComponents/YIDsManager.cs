﻿using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class YIDsManager
{
    public static YIDsManager Instance { get; private set; }
    private int[] standartTakenGroups = new int[]
    {
        689,
        748,
        993,
        1000,
        1001,
        1002,
        1003,

        1010,
        1011,
        1012,
        1013,
        1014,
        1015,

        6000,
        6001,
        6002,
        6003,
        6004,
        6005,
        6006
    };
    private int lastFreeGroup = 500;

    private (int, float)[] memory = new (int, float)[10000];

    private Dictionary<string, (int, bool)> globalVariables = new Dictionary<string, (int, bool)>();
    private Dictionary<string, Dictionary<string, (int, bool)>> groupsVariables = new Dictionary<string, Dictionary<string, (int, bool)>>();
    private List<int> globalPickedGroups = new List<int>();
    private Dictionary<string, List<int>> groupsPickedGroups = new Dictionary<string, List<int>>();
    private List<int> globalPickedGradientIDs = new List<int>();
    private Dictionary<string, List<int>> groupsPickedGradientIDs = new Dictionary<string, List<int>>();

    private List<int> globalFreeInts;
    private List<int> globalFreeFloats;
    private List<int> globalFreeGroups;

    private Dictionary<string, List<int>> groupsFreeInts;
    private Dictionary<string, List<int>> groupsFreeFloats;
    private Dictionary<string, List<int>> groupsFreeGroups;



    private string currentGroupName = null;
    public YIDsManager(int lastID)
    {
        Instance = this;

        lastFreeGroup = lastID;

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
        SetMemoryValueByName("Camera.focalLen", 8f);
        globalPickedGroups = standartTakenGroups.ToList();

        globalFreeInts = new List<int>();
        globalFreeFloats = new List<int>();
        globalFreeGroups = new List<int>();

        groupsFreeInts = new Dictionary<string, List<int>>();
        groupsFreeFloats = new Dictionary<string, List<int>>();
        groupsFreeGroups = new Dictionary<string, List<int>>();

        for (int i = lastFreeGroup; i < 10000; i++)
        {
            if (standartTakenGroups.Contains(i))
                continue;
            globalFreeInts.Add(i);
            globalFreeFloats.Add(i);
            globalFreeGroups.Add(i);
        }

    }
    public void InitGroups(string[] groups)
    {
        foreach (var group in groups)
        {
            groupsVariables.Add(group, new Dictionary<string, (int, bool)>());
            groupsPickedGroups.Add(group, new List<int>());
            groupsPickedGradientIDs.Add(group, new List<int>());


            groupsFreeInts.Add(group, new List<int>());
            groupsFreeFloats.Add(group, new List<int>());
            groupsFreeGroups.Add(group, new List<int>());

            groupsFreeInts[group].AddRange(globalFreeInts);
            groupsFreeFloats[group].AddRange(globalFreeFloats);
            groupsFreeGroups[group].AddRange(globalFreeGroups);
        }
    }

    public void SetCurrentGroupName(string groupName)
    {
        currentGroupName = groupName;
    }
    public string GetCurrentGroupName()
    {
        return currentGroupName;
    }
    public int AddVariable(string name, int id, bool isFloat)
    {
        return AddVariable(name, id, isFloat, currentGroupName);
    }
    public int AddVariable(string name, int id, bool isFloat, string groupName)
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
        

        if (groupName == null)
        {
            if (isFloat)
                globalFreeFloats.Remove(id);
            else
                globalFreeInts.Remove(id);
        }
        else
        {
            if (isFloat)
                groupsFreeFloats[groupName].Remove(id);
            else
                groupsFreeInts[groupName].Remove(id);
        }
        return id;
    }
    public void RemoveVariable(string name)
    {
        RemoveVariable(name, currentGroupName);
    }
    public void RemoveVariable(string name, string groupName)
    {
        bool isFloat = false;
        int id = -1;

        if (groupName == null)
        {
            if (!globalVariables.ContainsKey(name))
                throw new System.Exception("No variable with that name");
            isFloat = globalVariables[name].Item2;
            id = globalVariables[name].Item1;
            globalVariables.Remove(name);
        }
        else
        {
            if (!groupsVariables.ContainsKey(groupName) || !groupsVariables[groupName].ContainsKey(name))
                throw new System.Exception("No variable with that name");
            isFloat = groupsVariables[groupName][name].Item2;
            id = groupsVariables[groupName][name].Item1;
            groupsVariables[groupName].Remove(name);
        }


        if (groupName == null)
        {
            if (isFloat)
                globalFreeFloats.Remove(id);
            else
                globalFreeInts.Remove(id);
        }
        else
        {
            if (isFloat)
                groupsFreeFloats[groupName].Remove(id);
            else
                groupsFreeInts[groupName].Remove(id);
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
        if (id == 0)
            return (0, 0);
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
        //List<int> allIds = new List<int>();
        //for (int i = lastFreeGroup; i < 10000; i++)
        //{
        //    allIds.Add(i);
        //}
        //foreach (var variable in globalVariables.Values)
        //{
        //    if (variable.Item2 == false && allIds.Contains(variable.Item1))
        //    {
        //        allIds.Remove(variable.Item1);
        //    }
        //}
        //if (groupName != null)
        //{
        //    foreach (var variable in groupsVariables[groupName].Values)
        //    {
        //        if (variable.Item2 == false && allIds.Contains(variable.Item1))
        //        {
        //            allIds.Remove(variable.Item1);
        //        }
        //    }
        //}
        //
        //return allIds.Min();

        if (groupName != null)
            return groupsFreeInts[groupName].Min();
        return globalFreeInts.Min();
    }
    public int GetFreeIdFloat()
    {
        return GetFreeIdFloat(currentGroupName);
    }
    public int GetFreeIdFloat(string groupName)
    {
        //List<int> allIds = new List<int>();
        //for (int i = lastFreeGroup; i < 10000; i++)
        //{
        //    allIds.Add(i);
        //}
        //foreach (var variable in globalVariables.Values)
        //{
        //    if (variable.Item2 == true && allIds.Contains(variable.Item1))
        //    {
        //        allIds.Remove(variable.Item1);
        //    }
        //}
        //if (groupName != null)
        //{
        //    foreach (var variable in groupsVariables[groupName].Values)
        //    {
        //        if (variable.Item2 == true && allIds.Contains(variable.Item1))
        //        {
        //            allIds.Remove(variable.Item1);
        //        }
        //    }
        //}
        //return allIds.Min();

        if (groupName != null)
            return groupsFreeFloats[groupName].Min();
        return globalFreeFloats.Min();
    }
    public int GetFreeGroup()
    {
        return GetFreeGroup(currentGroupName);
    }
    public int GetFreeGroup(string groupName)
    {
        //List<int> allIds = new List<int>();
        //for (int i = lastFreeGroup; i < 10000; i++)
        //{
        //    allIds.Add(i);
        //}
        //foreach (var group in globalPickedGroups)
        //{
        //    if (allIds.Contains(group))
        //        allIds.Remove(group);
        //}
        //if (groupName != null)
        //{
        //    foreach (var group in groupsPickedGroups[groupName])
        //    {
        //        if (allIds.Contains(group))
        //            allIds.Remove(group);
        //    }
        //}
        //
        //return allIds.Min();

        if (groupName != null)
            return groupsFreeGroups[groupName].Min();
        return globalFreeGroups.Min();
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

        if (groupName == null)
            globalFreeGroups.Remove(group);
        else
            groupsFreeGroups[groupName].Remove(group);
    }
    public int GetFreeGradient()
    {
        return GetFreeGradient(currentGroupName);
    }
    public int GetFreeGradient(string groupName)
    {
        List<int> allIds = new List<int>();
        for (int i = -1; i > -10000; i--)
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
        if (allIds.Count == 0)
            return -10000;
        return allIds.Max();
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
        //for (int i = lastFreeGroup; i < 10000; i++)
        //{
        //    allIdsGroup.Add(i);
        //}
        //foreach (var group in globalPickedGroups)
        //{
        //    if (allIdsGroup.Contains(group))
        //        allIdsGroup.Remove(group);
        //}
        //if (groupName != null)
        //{
        //    foreach (var group in groupsPickedGroups[groupName])
        //    {
        //        if (allIdsGroup.Contains(group))
        //            allIdsGroup.Remove(group);
        //    }
        //}


        List<int> allIdsFloat = new List<int>();
        //for (int i = lastFreeGroup; i < 10000; i++)
        //{
        //    allIdsFloat.Add(i);
        //}
        //foreach (var variable in globalVariables.Values)
        //{
        //    if (variable.Item2 == true && allIdsFloat.Contains(variable.Item1))
        //    {
        //        allIdsFloat.Remove(variable.Item1);
        //    }
        //}
        //if (groupName != null)
        //{
        //    foreach (var variable in groupsVariables[groupName].Values)
        //    {
        //        if (variable.Item2 == true && allIdsFloat.Contains(variable.Item1))
        //        {
        //            allIdsFloat.Remove(variable.Item1);
        //        }
        //    }
        //}

        if (groupName == null)
        {
            allIdsGroup = new List<int>(globalFreeGroups);
            allIdsFloat = new List<int>(globalFreeFloats);
        }
        else
        {
            allIdsGroup = new List<int>(groupsFreeGroups[groupName]);
            allIdsFloat = new List<int>(groupsFreeFloats[groupName]);
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
        //for (int i = lastFreeGroup; i < 10000; i++)
        //{
        //    allIdsGroup.Add(i);
        //}
        //foreach (var group in globalPickedGroups)
        //{
        //    if (allIdsGroup.Contains(group))
        //        allIdsGroup.Remove(group);
        //}
        //if (groupName != null)
        //{
        //    foreach (var group in groupsPickedGroups[groupName])
        //    {
        //        if (allIdsGroup.Contains(group))
        //            allIdsGroup.Remove(group);
        //    }
        //}

        List<int> allIdsInt = new List<int>();
        //for (int i = lastFreeGroup; i < 10000; i++)
        //{
        //    allIdsInt.Add(i);
        //}
        //foreach (var variable in globalVariables.Values)
        //{
        //    if (variable.Item2 == false && allIdsInt.Contains(variable.Item1))
        //    {
        //        allIdsInt.Remove(variable.Item1);
        //    }
        //}
        //if (groupName != null)
        //{
        //    foreach (var variable in groupsVariables[groupName].Values)
        //    {
        //        if (variable.Item2 == false && allIdsInt.Contains(variable.Item1))
        //        {
        //            allIdsInt.Remove(variable.Item1);
        //        }
        //    }
        //}

        if (groupName == null)
        {
            allIdsGroup = new List<int>(globalFreeGroups);
            allIdsInt = new List<int>(globalFreeInts);
        }
        else
        {
            allIdsGroup = new List<int>(groupsFreeGroups[groupName]);
            allIdsInt = new List<int>(groupsFreeInts[groupName]);
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
