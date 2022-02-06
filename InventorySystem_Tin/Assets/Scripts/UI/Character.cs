using UnityEngine;
using UnityEngine.UI;
using Tin.CharacterStats;
using System;

public class Character : MonoBehaviour
{
    public int Health = 100;

    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligance;
    public CharacterStat Vitality;

    [SerializeField]
    DropItemArea _dropItemArea;
    [SerializeField]
    Inventory _inventory;
    [SerializeField]
    EquippmentPanel _equippmentPanel;
    [SerializeField]
    StatPanel _statPanel;
    [SerializeField]
    ItemTooltip _itemTooltip;
    [SerializeField]
    Image _draggableItem;

    private ItemSlot _dragItemSlot;

    private void OnValidate()
    {
        if(_itemTooltip == null)
        {
            _itemTooltip = FindObjectOfType<ItemTooltip>();
        }
    }

    private void Awake()
    {
        _statPanel.SetStats(Strength, Agility, Intelligance, Vitality);
        _statPanel.UpdateStatValues();

        //Setup svih eventa:

        //Click:
        _inventory.OnRightClickEvent += InventoryRightClick;
        _inventory.OnMiddleClickEvent += InventoryMiddleClick;
        _equippmentPanel.OnRightClickEvent += EquipmentPanelRightClick;

        //Point Enter
        _inventory.OnPointerEnterEvent += ShowTooltip;
        _equippmentPanel.OnPointerEnterEvent += ShowTooltip;

        //Point Exit
        _inventory.OnPointerExitEvent += HideTooltip;
        _equippmentPanel.OnPointerExitEvent += HideTooltip;

        //Begin Drag
        _inventory.OnBeginDragEvent += BeginDrag;
        _equippmentPanel.OnBeginDragEvent += BeginDrag;

        //End Drag
        _inventory.OnEndDragEvent += EndDrag;
        _equippmentPanel.OnEndDragEvent += EndDrag;

        //Drag
        _inventory.OnDragEvent += Drag;
        _equippmentPanel.OnDragEvent += Drag;

        //Drop
        _inventory.OnDropEvent += Drop;
        _equippmentPanel.OnDropEvent += Drop;
        _dropItemArea.OnDropEvent += DropItemOutsideUI;

       
    }

 

    private void InventoryRightClick(ItemSlot itemSlot)
    {

        if(itemSlot.Item is EquippableItem)
        {
            Equip((EquippableItem)itemSlot.Item);
        }
     
    }
    private void InventoryMiddleClick(ItemSlot itemSlot)
    {

         if (itemSlot.Item is UsableItem)
        {
            UsableItem usableItem = (UsableItem)itemSlot.Item;
            usableItem.Use(this);

            usableItem.IsConsumed = true;

            if (usableItem.IsConsumed)
            {
                _inventory.RemoveItem(usableItem);
                usableItem.RemoveItemSlot(itemSlot);
            }

        }
    }
    private void EquipmentPanelRightClick(ItemSlot itemSlot)
    {
        if (itemSlot.Item is EquippableItem)
        {
            UnEquip((EquippableItem)itemSlot.Item);
        }
    }


    private void ShowTooltip(ItemSlot itemSlot)
    {
        if (itemSlot.Item != null)
            _itemTooltip.ShowTooltip(itemSlot.Item);
    }
    private void HideTooltip(ItemSlot itemSlot)
    {
        _itemTooltip.HideTooltip();
    }

    private void BeginDrag(ItemSlot itemSlot)
    {
        if(itemSlot.Item != null)
        {
            _dragItemSlot = itemSlot;

            _draggableItem.sprite = itemSlot.Item.Icon;
            _draggableItem.transform.position = Input.mousePosition;
            _draggableItem.enabled = true;
        }
    }
    private void EndDrag(ItemSlot itemSlot)
    {
        _dragItemSlot = null;

        _draggableItem.enabled = false;
    }
    private void Drag(ItemSlot itemSlot)
    {
        if(_draggableItem.enabled)
            _draggableItem.transform.position = Input.mousePosition;
    }
    private void Drop(ItemSlot dropItemSlot)
    {
        if (_dragItemSlot == null)
            return;



        
        if (dropItemSlot.CanAddStack(_dragItemSlot.Item,1))
        {
            AddStacks(dropItemSlot);
        }

        else if(dropItemSlot.CanReciveItem(_dragItemSlot.Item) && _dragItemSlot.CanReciveItem(dropItemSlot.Item))
        {
            SwapItemsAndUpdateAmount(dropItemSlot);

        }


    }

    private void DropItemOutsideUI()
    {
        if(_dragItemSlot == null) return;


        Debug.Log("we dropped an item");
        _dragItemSlot.Item.DestroyItem();
        _dragItemSlot.Item=null;


    }
    private void AddStacks(ItemSlot dropItemSlot)
    {
        int numOfFreeSlotsLeft = dropItemSlot.Item.MaximumStacks - dropItemSlot.Amount;

        if(numOfFreeSlotsLeft <= 0)
        {
            return;
        }

        if (_dragItemSlot.Amount > numOfFreeSlotsLeft)
        {
            dropItemSlot.Amount += numOfFreeSlotsLeft;
            _dragItemSlot.Amount -= numOfFreeSlotsLeft;
        }
        else
        {            
            dropItemSlot.Amount += _dragItemSlot.Amount;
            _dragItemSlot.Amount -= _dragItemSlot.Amount;

            _dragItemSlot.Item=null;
        }

    }

    private void SwapItemsAndUpdateAmount(ItemSlot dropItemSlot)
    {
        EquippableItem dragItem = _dragItemSlot.Item as EquippableItem;
        EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

        if (_dragItemSlot is EquippmentSlot)
        {
            if (dragItem != null)
                dragItem.Unequip(this);

            if (dropItem != null)
                dropItem.Equip(this);
        }
        if (dropItemSlot is EquippmentSlot)
        {
            if (dragItem != null)
                dragItem.Equip(this);

            if (dropItem != null)
                dropItem.Unequip(this);
        }
        _statPanel.UpdateStatValues();

        Item draggedItem = _dragItemSlot.Item;
        int draggedItemAmount = _dragItemSlot.Amount;

        _dragItemSlot.Item = dropItemSlot.Item;
        _dragItemSlot.Amount = dropItemSlot.Amount;

        dropItemSlot.Item = draggedItem;
        dropItemSlot.Amount = draggedItemAmount;
    }

    public void Equip(EquippableItem item)
    {
        EquippableItem previousItem;
        if(_equippmentPanel.AddItem(item,out previousItem))
        {
            if(previousItem != null)
            {
                _inventory.AddItem(previousItem);
                previousItem.Unequip(this);
                _statPanel.UpdateStatValues();

            }
            _inventory.RemoveItem(item);

            item.Equip(this);
            _statPanel.UpdateStatValues();
        }
        else
        {
            _inventory.AddItem(item);
        }
    }

    public void UnEquip(EquippableItem item)
    {
        if (!_inventory.IsFull() && _equippmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            _statPanel.UpdateStatValues();
            _inventory.AddItem(item);
        }
    }

}
