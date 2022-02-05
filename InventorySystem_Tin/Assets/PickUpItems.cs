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
			Item itemCopy = collision.gameObject.GetComponent<ItemInWorld>().GetItem().GetCopy();
            if (_inventory.AddItem(itemCopy))
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
