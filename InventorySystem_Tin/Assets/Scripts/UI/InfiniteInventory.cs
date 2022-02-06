using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteInventory : MonoBehaviour, IItemContainer
{

    [SerializeField] GameObject itemSlotPrefab;

    [SerializeField] int maxSlots;
    public int MaxSlots
    {
        get { return maxSlots; }
        set { SetMaxSlots(value); }
    }

    [Space]

    [SerializeField]
    List<Item> _startingItems;
    [SerializeField]
    Transform _itemsParent;
    [SerializeField]
    List<ItemSlot> _itemSlots;

    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;


    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnMiddleClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;

    protected virtual void Start()
    {
        SetMaxSlots(maxSlots);

        for (int i = 0; i < _itemSlots.Count; i++)
        {
            _itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            _itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
            _itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            _itemSlots[i].OnMiddleClickEvent += OnMiddleClickEvent;
            _itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            _itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            _itemSlots[i].OnDragEvent += OnDragEvent;
            _itemSlots[i].OnDropEvent += OnDropEvent;
        }
        SetStartingItems();
    }



    private void OnValidate()
    {
        if (_itemsParent != null)
            _itemsParent.GetComponentsInChildren(includeInactive: true, result: _itemSlots);

        if (!Application.isPlaying)
        {
            SetStartingItems();
        }

        
    }

    private void SetMaxSlots(int value)
    {
        if (value <= 0)
        {
            maxSlots = 1;
        }
        else
        {
            maxSlots = value;
        }

        if (maxSlots < _itemSlots.Count)
        {
            for (int i = maxSlots; i < _itemSlots.Count; i++)
            {
                Destroy(_itemSlots[i].transform.parent.gameObject);
            }
            int diff = _itemSlots.Count - maxSlots;
            _itemSlots.RemoveRange(maxSlots, diff);
        }
        else if (maxSlots > _itemSlots.Count)
        {
            int diff = maxSlots - _itemSlots.Count;

            for (int i = 0; i < diff; i++)
            {
                GameObject gameObject = Instantiate(itemSlotPrefab);
                gameObject.transform.SetParent(_itemsParent, worldPositionStays: false);
                _itemSlots.Add(gameObject.GetComponentInChildren<ItemSlot>());
            }
        }
    }

    public bool AddItem(Item item)
    {

        if (IsFull())
        {
            MaxSlots += 8;
        }

        for (int i = 0; i < _itemSlots.Count; i++)
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

    public bool AddItem(Item item, int amountToAdd)
    {
        if (IsFull())
        {
            MaxSlots += 8;
        }

        for (int i = 0; i < _itemSlots.Count; i++)
        {
            if (_itemSlots[i].CanAddStack(item, _itemSlots[i].Amount))
            {
                _itemSlots[i].Item = item;
                _itemSlots[i].Amount += amountToAdd;
                return true;
            }
        }

        for (int i = 0; i < _itemSlots.Count; i++)
        {
            if (_itemSlots[i].Item == null)
            {
                _itemSlots[i].Item = item;
                _itemSlots[i].Amount += amountToAdd;
                return true;
            }
        }
        return false;
    }
    public Item RemoveItem(string itemID)
    {
        for (int i = 0; i < _itemSlots.Count; i++)
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
        for (int i = 0; i < _itemSlots.Count; i++)
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
        for (int i = 0; i < _itemSlots.Count; i++)
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
        Clear();
        int i = 0;
        for (; i < _startingItems.Count && i < _itemSlots.Count; i++)
        {
            _itemSlots[i].Item = _startingItems[i].GetCopy();
            _itemSlots[i].Amount = 1;
        }




    }

    public int ItemCount(string itemID)
    {
        int numberOfItemsCounter = 0;
        for (int i = 0; i < _itemSlots.Count; i++)
        {
            if (_itemSlots[i].Item.ID == itemID)
            {
                numberOfItemsCounter++;
            }
        }
        return numberOfItemsCounter;
    }

    public void Clear()
    {
        for (int i = 0; i < _itemSlots.Count; i++)
        {
            _itemSlots[i].Item = null;
        }
    }
}
