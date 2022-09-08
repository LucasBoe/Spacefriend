using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHasSkinType_InteractableCondition : InteractableConditionBase
{
    [SerializeField] PlayerSkinType playerSkinType;
    [SerializeField] bool invert = false;
    public override bool IsMet()
    {
        bool hasSkin = ServiceProvider.Player.GetSkin() == playerSkinType;
        return invert ? !hasSkin : hasSkin;
    }
}
