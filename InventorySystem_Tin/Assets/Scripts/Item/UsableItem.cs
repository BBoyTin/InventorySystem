using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(menuName = "Item/Usable Item")]
public class UsableItem :  Item
{
	public bool IsConsumed = false;

    public List<UsableItemEffect> Effects;

    public virtual void Use(Character character)
    {
        foreach (UsableItemEffect effect in Effects)
        {
            effect.ExecuteEffect(this, character);
        }
    }
    public override string GetItemType()
    {
        return "Potion";

    }

    public override string GetDescription()
    {
        string descriptionText = "This gives the player health"+"\n"+base.GetDescription();



        return descriptionText;
    }

    public void RemoveItemSlot(ItemSlot itemSlot)
    {
        itemSlot = null;
        //Destroy(this);
    }


}
