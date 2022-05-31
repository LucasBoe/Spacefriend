using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMugHoldsCoffee_InteractableCondition : InteractableConditionBase
{
    [SerializeField] CoffeMugItemData coffeMugItem;
    [SerializeField] bool invert;
    public override bool IsMet()
    {
        return coffeMugItem.HoldsCoffee != invert;
    }
}
