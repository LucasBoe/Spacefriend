using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] SpriteLibrary library;
    [SerializeField, Foldout("AnimatorReferences")] Animator animator;
    [SerializeField, Foldout("AnimatorReferences"), AnimatorParam("animator")] string directionalVectorX, directionalVectorY, overrideParam;
    [SerializeField] SpriteResolver[] resolvers;
    [SerializeField] GameObject astronautBackpack;

    private List<PlayerAnimationOverrider> playerAnimationOverrides = new List<PlayerAnimationOverrider>();
    private bool hasOverrides => playerAnimationOverrides.Count > 0;
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
        animator.SetBool(over.Parameter, true);
        animator.SetBool(overrideParam, hasOverrides);
    }


    private void OnRemoveOverride(PlayerAnimationOverrider over)
    {
        playerAnimationOverrides.Remove(over);
        animator.SetBool(over.Parameter, false);
        animator.SetBool(overrideParam, hasOverrides);
    }
    private void OnSkinChanged(PlayerSkinType newSkin)
    {
        foreach (SpriteResolver resolver in resolvers)
        {
            string cat = resolver.GetCategory();
            List<string> labels = new List<string>(library.spriteLibraryAsset.GetCategoryLabelNames(cat));
            resolver.SetCategoryAndLabel(cat, labels[(int)newSkin]);
        }

        astronautBackpack.SetActive(newSkin == PlayerSkinType.Astronaut);
    }

    private void Update()
    {
        Vector2 directionalVector = Vector2.Lerp(lastDirVector, player.PhysicsModule.GetDirectionalMoveVector(), Time.deltaTime * 10f);

        animator.SetFloat(directionalVectorX, directionalVector.x);
        animator.SetFloat(directionalVectorY, directionalVector.y);
        transform.localScale = new Vector3(Mathf.Sign(directionalVector.x), 1, 1);

        lastDirVector = directionalVector;
    }
}
