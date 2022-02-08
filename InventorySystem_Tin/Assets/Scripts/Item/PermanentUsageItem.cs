using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/PermanentUsage Item")]
public class PermanentUsageItem : Item
{
    private void OnValidate()
    {
        IsPickUpable = false;

    }

    public override string GetItemType()
    {
        return "PermanentUsage";

    }

    public override string GetDescription()
    {
        string descriptionText = "You used an Permanent Usage by name: "+ItemName;

        return descriptionText;
    }


}
