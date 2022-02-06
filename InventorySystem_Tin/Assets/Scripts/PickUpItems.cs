using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{


	[SerializeField]
	Inventory _inventory;


	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.GetComponent<ItemInWorld>() != null)
        {
			
			ItemInWorld itemInWorld = collision.gameObject.GetComponent<ItemInWorld>();

            if (_inventory.AddItem(itemInWorld.GetItem().GetCopy(),itemInWorld.GetAmountOfThisItem()))
            {

				Destroy(collision.gameObject);

			}
            else
            {
				Debug.Log("item ne ide u inventory zbog neceg");
            }
		}
	}

	
}
