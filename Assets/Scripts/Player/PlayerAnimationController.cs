using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] PlayerSkin[] skins;
    [SerializeField] SpriteRenderer spriteRenderer;

    private List<PlayerAnimationOverrider> playerAnimationOverrides = new List<PlayerAnimationOverrider>();
    private void OnEnable()
    {
        PlayerAnimationOverrider.AddOverrideEvent += OnAddOverride;
        PlayerAnimationOverrider.RemoveOverrideEvent += OnRemoveOverride;
        player.SkinModule.ChangedSkinTypeEvent += OnSkinChanged;
    }

    private void OnDisable()
    {
        PlayerAnimationOverrider.AddOverrideEvent -= OnAddOverride;
        PlayerAnimationOverrider.RemoveOverrideEvent -= OnRemoveOverride;
        player.SkinModule.ChangedSkinTypeEvent -= OnSkinChanged;
    }

    private void OnAddOverride(PlayerAnimationOverrider over)
    {
        playerAnimationOverrides.Add(over);
        UpdateAnimation();
    }


    private void OnRemoveOverride(PlayerAnimationOverrider over)
    {
        playerAnimationOverrides.Remove(over);
        UpdateAnimation();
    }
    private void OnSkinChanged(PlayerSkinType newSkin)
    {
        Sprite newSprite = null;

        foreach (PlayerSkin skin in skins)
        {
            if (skin.Type == newSkin)
                newSprite = skin.PlaceholderSprite;
        }

        if (newSprite != null)
            spriteRenderer.sprite = newSprite;
    }

    private void UpdateAnimation()
    {
        //
    }
}
