using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(menuName = "Item/Basic Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    string _id;
    public string ID { get { return _id; } }
  
    public string ItemName;
    public Sprite Icon;

    //zadatak trazi da moze imati max int stacks, inace bi ovako!!!
   // [Range(1, 1000)]
    public int MaximumStacks = 1;

    private void OnValidate()
    {
        //koristim da automatski postavi id jednak unikantom idu od Unitya
        string path = AssetDatabase.GetAssetPath(this);
        _id = AssetDatabase.AssetPathToGUID(path);

    }

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


public struct ItemAmount
{
    public Item Item;
    [Range(1, 10000)]
    public int Amount;

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