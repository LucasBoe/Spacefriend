using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRendererMaterialInstanciator))]
public class SpriteFeedback_InteractionHoverListener : MonoBehaviour, IInteractableHoverListener
{
    Material material;
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }
    public void BeginHover()
    {
        material.SetFloat("Hover", 1);
    }

    public void EndHover()
    {
        material.SetFloat("Hover", 0);
    }
}
