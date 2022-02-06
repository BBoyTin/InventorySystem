using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour,IItemContainer
{

    [SerializeField]
    List<Item> _startingItems;
    [SerializeField]
    Transform _itemsParent;
    [SerializeField]
    ItemSlot[] _itemSlots;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;


    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    private void Start()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            _itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            _itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            _itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            _itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            _itemSlots[i].OnDragEvent += OnDragEvent;
            _itemSlots[i].OnDropEvent += OnDropEvent;
        }
        SetStartingItems();
    }

    private void OnValidate()
    {
        if(_itemsParent != null)
        {
            _itemSlots = _itemsParent.GetComponentsInChildren<ItemSlot>();
        }

        SetStartingItems();
    }


    public bool AddItem(Item item)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == null || (_itemSlots[i].Item.ID == item.ID && _itemSlots[i].Amount < item.MaximumStacks))
            {
                _itemSlots[i].Item = item;
                _itemSlots[i].Amount++;
                return true;
            }
        }
        return false;
    }

    public bool AddItem(Item item,int amountToAdd)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == null || (_itemSlots[i].Item.ID == item.ID && _itemSlots[i].Amount < item.MaximumStacks))
            {
                _itemSlots[i].Item = item;
                _itemSlots[i].Amount+= amountToAdd;
                return true;
            }
        }
        return false;
    }
    public Item RemoveItem(string itemID)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            Item item = _itemSlots[i].Item;
            if (item != null && item.ID == itemID)
            {
                _itemSlots[i].Amount--;
                if (_itemSlots[i].Amount <= 0)
                {
                    _itemSlots[i].Item = null;
                }
                return item;
            }
        }
        return null;
    }
    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == item)
            {
                _itemSlots[i].Amount--;
                if (_itemSlots[i].Amount <= 0)
                {
                    _itemSlots[i].Item = null;
                }
                
                return true;
            }
        }
        return false;
    }

    public bool IsFull()
    {
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item == null)
            {
                return false;
            }
        }
        return true;
    }

    public void SetStartingItems()
    {
        int i = 0;
        for (; i < _startingItems.Count && i<_itemSlots.Length; i++)
        {
            _itemSlots[i].Item = _startingItems[i].GetCopy();
            _itemSlots[i].Amount = 1;
        }
        for (; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Item = null;
            _itemSlots[i].Amount = 0;
        }
        
    }





    public int ItemCount(string itemID)
    {
        int numberOfItemsCounter = 0;
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (_itemSlots[i].Item.ID == itemID)
            {
                numberOfItemsCounter++;
            }
        }
        return numberOfItemsCounter;
    }

 
}
