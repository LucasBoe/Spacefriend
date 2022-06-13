using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFeedback_InteractionHoverListener : MonoBehaviour, IInteractableHoverListener
{
    Material material;
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        Debug.Log(material);
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
