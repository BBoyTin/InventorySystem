using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InfiniteInventory : Inventory
{
    [SerializeField]
    GameObject _itemSlotPrefab;
	[SerializeField] 
	Transform _itemsParent;


	[SerializeField] int maxSlots;
	public int MaxSlots
	{
		get { return maxSlots; }
		set {// SetMaxSlots(value);
		}
	}

	protected override void Start()
	{
		//SetMaxSlots(maxSlots);
		base.Start();
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

		if (maxSlots < GetItemSlots().Length)
		{
			for (int i = maxSlots; i < GetItemSlots().Length; i++)
			{
				Destroy(GetItemSlotOfIndexI(i).transform.parent.gameObject);
			}
			int diff = GetItemSlots().Length - maxSlots;
			//GetItemSlots().RemoveRange(maxSlots, diff);
		}
		else if (maxSlots > GetItemSlots().Length)
		{
			int diff = maxSlots - GetItemSlots().Length;

			for (int i = 0; i < diff; i++)
			{
				GameObject gameObject = Instantiate(_itemSlotPrefab);
				gameObject.transform.SetParent(_itemsParent, worldPositionStays: false);

			}
		}
	}
	
}
