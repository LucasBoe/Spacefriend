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
        group.alpha = 0f;
    }
    protected override void SetVisibilityAmount(float alpha)
    {
        group.alpha = alpha;
    }

    protected override void SetInteractable(bool interactable)
    {
        group.interactable = interactable;
    }

    protected override void SetHidden(bool hidden)
    {
        //
    }
}
