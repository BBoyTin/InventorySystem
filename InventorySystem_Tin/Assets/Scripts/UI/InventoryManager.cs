using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    Inventory _inventory;
    [SerializeField]
    EquippmentPanel _equippmentPanel;


    private void Awake()
    {
        _inventory.OnItemRightClickEvent += EquipFromInventory;
        _equippmentPanel.OnItemRightClickEvent += UnEquipFromEquipPanel;
    }

    private void EquipFromInventory(Item item)
    {
        if(item is EquippableItem)
        {
            Equip((EquippableItem)item);
        }
    }

    private void UnEquipFromEquipPanel(Item item)
    {
        if(item is EquippableItem)
        {
            UnEquip((EquippableItem)item);
        }
    }

    public void Equip(EquippableItem item)
    {
        EquippableItem previousItem;
        if(_equippmentPanel.AddItem(item,out previousItem))
        {
            if(previousItem != null)
            {
                _inventory.AddItem(previousItem);
                
            }
            _inventory.RemoveItem(item);
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
            _inventory.AddItem(item);
        }
    }

}
