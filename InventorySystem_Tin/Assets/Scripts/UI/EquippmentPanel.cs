using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippmentPanel : MonoBehaviour
{
    [SerializeField]
    Transform _equipmentSlotsParent;
    [SerializeField]
    EquippmentSlot[] _equipmentSlots;

	public event Action<Item> OnItemRightClickEvent;

	private void Start()
	{
		for (int i = 0; i < _equipmentSlots.Length; i++)
		{
			_equipmentSlots[i].OnRightClickEvent += OnItemRightClickEvent;
		}
	}

	private void OnValidate()
    {
        _equipmentSlots = _equipmentSlotsParent.GetComponentsInChildren<EquippmentSlot>();
    }
	public bool AddItem(EquippableItem item,out EquippableItem previousItem)
	{
		for (int i = 0; i < _equipmentSlots.Length; i++)
		{
			if (_equipmentSlots[i].EquipmentType == item.EquipmentType)
			{
				previousItem = (EquippableItem)_equipmentSlots[i].Item;
				_equipmentSlots[i].Item = item;
				//_equipmentSlots[i].Amount = 1;
				return true;
			}
		}
		previousItem = null;
		return false;
	}

	public bool RemoveItem(EquippableItem item)
	{
		for (int i = 0; i < _equipmentSlots.Length; i++)
		{
			if (_equipmentSlots[i].Item == item)
			{
				_equipmentSlots[i].Item = null;
				//_equipmentSlots[i].Amount = 0;
				return true;
			}
		}
		return false;
	}

}
