using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private List<PlayerAnimationOverrider> playerAnimationOverrides = new List<PlayerAnimationOverrider>();
    private void OnEnable()
    {
        PlayerAnimationOverrider.AddOverrideEvent += OnAddOverride;
        PlayerAnimationOverrider.RemoveOverrideEvent += OnRemoveOverride;
    }

    private void OnDisable()
    {
        PlayerAnimationOverrider.AddOverrideEvent -= OnAddOverride;
        PlayerAnimationOverrider.RemoveOverrideEvent -= OnRemoveOverride;
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
    private void UpdateAnimation()
    {
        //
    }
}
