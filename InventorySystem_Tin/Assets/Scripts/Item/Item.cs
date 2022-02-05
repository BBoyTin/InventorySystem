﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite Icon;

    public virtual string GetItemType()
    {
        return "basic item ";
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
