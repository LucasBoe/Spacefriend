using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSkinType
{
    Bed = -2,
    Bath = -1,
    Default = 0,
    Space = 1
}

[System.Serializable]
public class PlayerSkin
{
    public PlayerSkinType Type;
    public Sprite HeadOverride, BodyOverride, ArmLOverride, ArmROverride, LegLOverride, LegROverride;

    internal void Apply(PlayerAnimationController.PlayerBodySpriteRenderers renderers)
    {
        if (HeadOverride != null) renderers.HeadRenderer.sprite = HeadOverride;
        if (BodyOverride != null) renderers.BodyRenderer.sprite = BodyOverride;
        if (ArmLOverride != null) renderers.ArmLRenderer.sprite = ArmLOverride;
        if (ArmROverride != null) renderers.ArmRRenderer.sprite = ArmROverride;
        if (LegLOverride != null) renderers.LegLRenderer.sprite = LegLOverride;
        if (LegROverride != null) renderers.LegRRenderer.sprite = LegROverride;
    }
}
