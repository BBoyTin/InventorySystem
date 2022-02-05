using UnityEngine;
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


    private void Awake()
    {
        _statPanel.SetStats(Strength, Agility, Intelligance, Vitality);
        _statPanel.UpdateStatValues();

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
