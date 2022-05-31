using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriesItem_InteractableCondition : InteractableConditionBase
{
    [SerializeField] CarryQuestionType toCheckFor;
    [SerializeField, ShowIf("isLookingForSpecificItem")] ItemData itemToCheckFor;
    bool isLookingForSpecificItem => toCheckFor == CarryQuestionType.SPECIFIC;

    public override bool IsMet()
    {
        ItemData inHand = PlayerServiceProvider.GetPlayerItemInHand();

        if (toCheckFor == CarryQuestionType.SPECIFIC)
            return inHand == itemToCheckFor;


        return (toCheckFor == CarryQuestionType.ANY) == (inHand != null);
    }

    private enum CarryQuestionType
    {
        ANY,
        NONE,
        SPECIFIC
    }
}
