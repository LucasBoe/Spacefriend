using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Animator animator;
    [SerializeField, AnimatorParam("animator")] string directionalVectorX, directionalVectorY;
    [SerializeField] PlayerSkin[] skins;

    private List<PlayerAnimationOverrider> playerAnimationOverrides = new List<PlayerAnimationOverrider>();
    private Vector2 lastDirVector;

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

        /*
        if (newSprite != null)
            spriteRenderer.sprite = newSprite;
        */
    }

    private void Update()
    {
        Vector2 directionalVector =Vector2.Lerp(lastDirVector,  player.MoveModule.GetDirectionalMoveVector(), Time.deltaTime * 10f);

        animator.SetFloat(directionalVectorX, directionalVector.x);
        animator.SetFloat(directionalVectorY, directionalVector.y);
        transform.localScale = new Vector3(Mathf.Sign(directionalVector.x), 1, 1);

        lastDirVector = directionalVector;
    }

    private void UpdateAnimation()
    {
        //
    }
}
