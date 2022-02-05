using UnityEngine;
using UnityEngine.UI;
using Tin.CharacterStats;

public class Character : MonoBehaviour
{

    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligance;
    public CharacterStat Vitality;

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

    private ItemSlot _draggedSlot;

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
        _inventory.OnRightClickEvent += Equip;
        _equippmentPanel.OnRightClickEvent += UnEquip;

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

    }

 
    private void Equip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;

        if(equippableItem != null)
        {
            Equip(equippableItem);
        }
    }
    
    private void UnEquip(ItemSlot itemSlot)
    {
        EquippableItem equippableItem = itemSlot.Item as EquippableItem;

        if(equippableItem != null)
        {
            UnEquip(equippableItem);
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
            _draggedSlot = itemSlot;

            _draggableItem.sprite = itemSlot.Item.Icon;
            _draggableItem.transform.position = Input.mousePosition;
            _draggableItem.enabled = true;
        }
    }
    private void EndDrag(ItemSlot itemSlot)
    {
        _draggedSlot = null;

        _draggableItem.enabled = false;
    }
    private void Drag(ItemSlot itemSlot)
    {
        if(_draggableItem.enabled)
            _draggableItem.transform.position = Input.mousePosition;
    }
    private void Drop(ItemSlot dropItemSlot)
    {
        if(dropItemSlot.CanReciveItem(_draggedSlot.Item) && _draggedSlot.CanReciveItem(dropItemSlot.Item))
        {
            EquippableItem dragItem = _draggedSlot.Item as EquippableItem;
            EquippableItem dropItem = dropItemSlot.Item as EquippableItem;

            if(_draggedSlot is EquippmentSlot)
            {
                if (dragItem != null)
                    dragItem.Unequip(this);

                if (dropItem != null)
                    dropItem.Equip(this);
            }
            if(dropItemSlot is EquippmentSlot)
            {
                if (dragItem != null)
                    dragItem.Equip(this);

                if (dropItem != null)
                    dropItem.Unequip(this);
            }
            _statPanel.UpdateStatValues();


            SwapItems(dropItemSlot);
        }


    }

    private void SwapItems(ItemSlot dropItemSlot)
    {
        Item draggedItem = _draggedSlot.Item;
        _draggedSlot.Item = dropItemSlot.Item;
        dropItemSlot.Item = draggedItem;
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
