using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFeedback_InteractionHoverListener : MonoBehaviour, IInteractableHoverListener
{
    Material material;
    private void Start()
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
