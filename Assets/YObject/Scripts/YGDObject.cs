using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class YGDObject
{
    protected int[] groups = new int[0];
    protected int[] groupsParent = new int[0];
    public Vector2 pos = Vector2.zero;
    public abstract string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null);

    public virtual void AddGroup(int group, bool toChildren = false)
    {
        List<int> gs = groups.ToList();
        gs.Add(group);
        groups = gs.ToArray();
    }
    public virtual void AddGroupParent(int group, bool toChildren = false)
    {
        List<int> gs = groupsParent.ToList();
        gs.Add(group);
        groupsParent = gs.ToArray();
    }
    public virtual void AddGroups(int[] groups, bool toChildren = false)
    {
        List<int> gs = this.groups.ToList();
        gs.AddRange(groups);
        this.groups = gs.ToArray();
    }
    public virtual void AddGroupsParent(int[] groups, bool toChildren = false)
    {
        List<int> gs = groupsParent.ToList();
        gs.AddRange(groups);
        groupsParent = gs.ToArray();
    }

    protected string GetGroupsString(int[] groups = null, int[] groupsParent = null)
    {
        if (this.groups != null)
            groups = this.groups;

        string groupsString = "";

        if (groups != null)
        { 
            foreach (int group in groups)
            {
                groupsString += group + ".";
            }
        }
        if (groupsString.Length > 0)
            groupsString = groupsString.Substring(0, groupsString.Length - 1);


        if (this.groupsParent != null)
            groupsParent = this.groupsParent;

        string groupsParentString = "";

        if (groupsParent != null)
        {
            foreach (int group in groupsParent)
            {
                groupsParentString += group + ".";
            }
        }
        if (groupsParentString.Length > 0)
            groupsParentString = groupsParentString.Substring(0, groupsParentString.Length - 1);

        string returnString = "";

        if (groupsString.Length > 0)
        {
            returnString += ",57," + groupsString;
        }
        if (groupsParentString.Length > 0)
        {
            returnString += ",274," + groupsParentString;
        }

        return returnString;
    }
}
