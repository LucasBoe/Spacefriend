using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GroupUIBehaviour : UIBehaviour
{
    CanvasGroup group;
    protected virtual void Awake()
    {
        group = GetComponent<CanvasGroup>();
    }
    protected override void SetAlpha(float alpha)
    {
        group.alpha = alpha;
    }

    protected override void SetInteractable(bool interactable)
    {
        group.interactable = interactable;
    }
}
