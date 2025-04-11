using UnityEngine;

public class YGameobjectGroup : MonoBehaviour
{
    [SerializeField] private string groupName;
    
    public string GetName()
    {
        return groupName;
    }
}
