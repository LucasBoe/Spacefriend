using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

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


#if UNITY_EDITOR
    public string GetComponentName() => "SpriteFeedback";
    public void DrawInspector() { }
#endif
}
