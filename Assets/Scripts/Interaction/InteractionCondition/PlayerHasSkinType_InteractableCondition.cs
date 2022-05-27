using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHasSkinType_InteractableCondition : InteractableConditionBase
{
    [SerializeField] PlayerSkinType playerSkinType;
    [SerializeField] bool invert = false;
    public override bool IsMet()
    {
        bool hasSkin = PlayerManager.GetPlayerSkin() == playerSkinType;
        return invert ? !hasSkin : hasSkin;
    }
}
