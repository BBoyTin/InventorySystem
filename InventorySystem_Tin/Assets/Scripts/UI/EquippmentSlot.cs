
public class EquippmentSlot : ItemSlot
{
    public EquipmentType EquipmentType;


    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = EquipmentType.ToString() + " Slot";
    }

    public override bool CanReciveItem(Item item)
    {
        if (item == null)
            return true;

        EquippableItem equippableItem = item as EquippableItem;

        //ako postoji, ako je moze je tip itema za odreden slot
        return equippableItem != null && equippableItem.EquipmentType == EquipmentType;
    }

}
