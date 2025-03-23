using Unity.VisualScripting;
using UnityEngine;

public abstract class YGDObject
{
    public int[] groups = null;
    public int[] groupsParent = null;
    public abstract string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null);

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
