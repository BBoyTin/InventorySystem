using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Item/Basic Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    string _id;
    public string ID { get { return _id; } }
  
    public string ItemName;
    public Sprite Icon;

    public bool IsPickUpable = true;

    
    public int MaximumStacks = 1;


#if UNITY_EDITOR
    private void OnValidate()
    {
        //koristim da automatski postavi id jednak unikantom idu od Unitya
        string path = AssetDatabase.GetAssetPath(this);
        _id = AssetDatabase.AssetPathToGUID(path);

    }
#endif
    public virtual Item GetCopy()
    {
        return this;
    }

    public virtual void DestroyItem()
    {
        Debug.Log("this needs to be overriten");
    }

    public virtual string GetItemType()
    {
        return "basic item ";
    }

    public virtual string GetDescription()
    {
        if (MaximumStacks ==int.MaxValue)
        {
            return "Max Stacks: INFINITE" ;
        }

        return "Max Stacks: "+ MaximumStacks;
    }
}




public interface IItemContainer
{
    int ItemCount(string itemID);
    Item RemoveItem(string itemID);
    bool RemoveItem(Item item);
    bool AddItem(Item item);
    bool IsFull();

    void Clear();

}