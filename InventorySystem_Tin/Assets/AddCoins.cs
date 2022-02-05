using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCoins : MonoBehaviour
{

    [SerializeField] Item item;


    [SerializeField] Inventory inventory;

    [SerializeField] KeyCode addCoinKeyCode = KeyCode.X;

    private void Update()
    {
        if (Input.GetKeyDown(addCoinKeyCode))
        {
            Item itemCopy = item.GetCopy();
            inventory.AddItem(itemCopy);

        }
    }
}
