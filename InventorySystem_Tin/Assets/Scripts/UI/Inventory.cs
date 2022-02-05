using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    List<Item> _items;
    [SerializeField]
    Transform _itemsParent;
    [SerializeField]
    ItemSlot[] _itemSlots;

    public event Action<Item> OnItemRightClickEvent;

    private void Start()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].OnRightClickEvent += OnItemRightClickEvent;
        }
    }

    private void OnValidate()
    {
        if(_itemsParent != null)
        {
            _itemSlots = _itemsParent.GetComponentsInChildren<ItemSlot>();
        }

        RefreshUI();
    }


    public bool AddItem(Item item)
    {
        if (IsFull())
        {
            return false;
        }
        _items.Add(item);
        RefreshUI();
        return true;
    }

    public bool RemoveItem(Item item)
    {
        if (_items.Remove(item))
        {
            RefreshUI();
            return true;
        }
        return false;
    }

    public bool IsFull()
    {
        return _items.Count >= _itemSlots.Length;
    }

    public void RefreshUI()
    {
        int i = 0;
        for (; i < _items.Count && i<_itemSlots.Length; i++)
        {
            _itemSlots[i].Item = _items[i];
        }
        for (; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Item = null;
        }
        
    }
}
