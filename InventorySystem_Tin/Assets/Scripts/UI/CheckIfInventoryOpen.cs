using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfInventoryOpen : MonoBehaviour
{
    [SerializeField]
    InfiniteInventory _inventory;

    private void Update()
    {
        
        if (_inventory.gameObject.activeSelf)
            return;
        else
            MakeThisGoAway();
    }

    private void MakeThisGoAway()
    {
        RectTransform myRectTransform = GetComponent<RectTransform>();
        myRectTransform.localPosition =new Vector3(10000,10000);
        
      
    }
}
