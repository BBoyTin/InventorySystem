using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItems : MonoBehaviour
{


	[SerializeField]
	InfiniteInventory _inventory;

    [SerializeField]
    Text _pickUpText;
    [SerializeField]
    Text _debugText;

    [SerializeField]
    KeyCode _keyCodeToPickUp = KeyCode.F;

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (Input.GetKeyDown(_keyCodeToPickUp)){
            
            CheckAndAddItem(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            UpdateText(collision);
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _pickUpText.text = "";
    }

    private void UpdateText(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ItemInWorld>() != null)
        {
            ItemInWorld itemInWorld = collision.gameObject.GetComponent<ItemInWorld>();
            _pickUpText.text = "Press " + _keyCodeToPickUp.ToString()+" to get "+itemInWorld.GetItem().name;
        }
        else
        {
            _pickUpText.text = "";
        }
    }

    private void CheckAndAddItem(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ItemInWorld>() != null)
        {
            ItemInWorld itemInWorld = collision.gameObject.GetComponent<ItemInWorld>();

            if (itemInWorld.GetItem().IsPickUpable == false)
            {
                _debugText.text = itemInWorld.GetItem().GetDescription();
                Destroy(collision.gameObject);
                return;
            }

            else if (_inventory.AddItem(itemInWorld.GetItem().GetCopy(), itemInWorld.GetAmountOfThisItem()))
            {

                Destroy(collision.gameObject);
            }
            else
            {
                Debug.Log("greska s pickup itema");
            }
        }
    }

}
