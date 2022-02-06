using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item Effects/Heal")]
public class HealItemEffect : UsableItemEffect
{

    public int HealAmount=20;
    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        character.Health += HealAmount;
    }

	
}
