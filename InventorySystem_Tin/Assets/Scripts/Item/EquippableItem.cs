using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Tin.CharacterStats;

public enum EquipmentType
{
	Helmet,
	Chest,
	Weapon,
	Accessory,
}

[CreateAssetMenu(menuName = "Item/Equipable Item")]
public class EquippableItem : Item
{
	public int StrengthBonus;
	public int AgilityBonus;
	public int IntelligenceBonus;
	public int VitalityBonus;
	
	[Space]
	public EquipmentType EquipmentType;


	public void Equip(Character c)
	{
		if (StrengthBonus != 0)
			c.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
		if (AgilityBonus != 0)
			c.Agility.AddModifier(new StatModifier(AgilityBonus, StatModType.Flat, this));
		if (IntelligenceBonus != 0)
			c.Intelligance.AddModifier(new StatModifier(IntelligenceBonus, StatModType.Flat, this));
		if (VitalityBonus != 0)
			c.Vitality.AddModifier(new StatModifier(VitalityBonus, StatModType.Flat, this));

		
	}

	public void Unequip(Character c)
	{
		c.Strength.RemoveAllModifiersFromSource(this);
		c.Agility.RemoveAllModifiersFromSource(this);
		c.Intelligance.RemoveAllModifiersFromSource(this);
		c.Vitality.RemoveAllModifiersFromSource(this);
	}

    public override Item GetCopy()
    {
        return Instantiate(this);
    }

    public override void DestroyItem()
    {
        Destroy(this);
    }
    public override string GetItemType()
    {
		return EquipmentType.ToString();

	}

	public override string GetDescription()
	{

		
		string descriptionText="";

		descriptionText +="Str "+ StrengthBonus +"\n"+
		"Agl "+ AgilityBonus +"\n"+
		"Int "+ IntelligenceBonus +"\n"+
		"Vit "+ VitalityBonus+"\n \n"
		+ base.GetDescription(); 


		return descriptionText;
	}
}
