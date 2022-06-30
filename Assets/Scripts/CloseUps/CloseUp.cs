using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class CloseUp : AnimatedPanel
{

    private void OnEnable() => InteractionHandler.ClickOutsideOfCloseUpEvent += OnClickedOutsideOfCloseUp;
    private void OnDisable() => InteractionHandler.ClickOutsideOfCloseUpEvent -= OnClickedOutsideOfCloseUp;
    private void OnClickedOutsideOfCloseUp() => Close();

    protected override void Awake()
    {
        base.Awake();
        gameObject.layer = LayerMask.NameToLayer("CloseUp");
    }
}
