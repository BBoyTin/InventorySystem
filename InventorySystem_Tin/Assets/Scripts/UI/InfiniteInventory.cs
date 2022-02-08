using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteInventory : MonoBehaviour, IItemContainer
{

    [SerializeField] GameObject _itemSlotPrefab;

    [SerializeField] int _maxSlots;
    public int MaxSlots
    {
        get { return _maxSlots; }
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

        for (int i = 0; i < _itemSlots.Count; i++)
        {
            UpdateEvents(i);
        }
        SetMaxSlots(_maxSlots);
        SetStartingItems();
    }

    private void UpdateEvents(int indexOfItemSlotToUpdate)
    {
        _itemSlots[indexOfItemSlotToUpdate].OnPointerEnterEvent += OnPointerEnterEvent;
        _itemSlots[indexOfItemSlotToUpdate].OnPointerExitEvent += OnPointerExitEvent;
        _itemSlots[indexOfItemSlotToUpdate].OnRightClickEvent += OnRightClickEvent;
        _itemSlots[indexOfItemSlotToUpdate].OnMiddleClickEvent += OnMiddleClickEvent;
        _itemSlots[indexOfItemSlotToUpdate].OnBeginDragEvent += OnBeginDragEvent;
        _itemSlots[indexOfItemSlotToUpdate].OnEndDragEvent += OnEndDragEvent;
        _itemSlots[indexOfItemSlotToUpdate].OnDragEvent += OnDragEvent;
        _itemSlots[indexOfItemSlotToUpdate].OnDropEvent += OnDropEvent;
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
            _maxSlots = 1;
        }
        else
        {
            _maxSlots = value;
        }

        if (_maxSlots < _itemSlots.Count)
        {
            for (int i = _maxSlots; i < _itemSlots.Count; i++)
            {
                Destroy(_itemSlots[i].transform.parent.gameObject);
            }
            int diff = _itemSlots.Count - _maxSlots;
            _itemSlots.RemoveRange(_maxSlots, diff);
        }
        else if (_maxSlots > _itemSlots.Count)
        {
            int beforeUpdate = _itemSlots.Count;
            int diff = _maxSlots - _itemSlots.Count;

            for (int i = 0; i < diff; i++)
            {
                GameObject gameObject = Instantiate(_itemSlotPrefab);
                gameObject.transform.SetParent(_itemsParent, worldPositionStays: false);
                _itemSlots.Add(gameObject.GetComponentInChildren<ItemSlot>());
                
            }
            for (int i = beforeUpdate; i < _itemSlots.Count; i++)
            {
                UpdateEvents(i);
            }
        }
    }

    public bool AddItem(Item item)
    {

        if (IsFull())
        {
            SetMaxSlots(MaxSlots + 8);
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
            SetMaxSlots(MaxSlots + 8);
        }

        for (int i = 0; i < _itemSlots.Count; i++)
        {
            if (_itemSlots[i].CanAddStack(item, amountToAdd))
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
        if (IsHalfFull())
        {
            SetMaxSlots(MaxSlots / 2);
        }
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
        if (IsHalfFull())
        {
            Debug.Log("dropali smo preko pola itema");
            SetMaxSlots(MaxSlots / 2);
        }

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
    public bool IsHalfFull()
    {
        int numOfUsedSlots = 0;
        for (int i = 0; i < _itemSlots.Count; i++)
        {
            if(_itemSlots[i] != null)
            {
                numOfUsedSlots++;
            }
        }
        if (numOfUsedSlots  <= (MaxSlots/2))
            return true;
        else
            return false;
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
