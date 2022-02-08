using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItems : MonoBehaviour
{


	[SerializeField]
	InfiniteInventory _inventory;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.F)){
            CheckAndAddItem(collision);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //CheckAndAddItem(collision);
    }

    private void CheckAndAddItem(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ItemInWorld>() != null)
        {
            ItemInWorld itemInWorld = collision.gameObject.GetComponent<ItemInWorld>();

            if (_inventory.AddItem(itemInWorld.GetItem().GetCopy(), itemInWorld.GetAmountOfThisItem()))
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
