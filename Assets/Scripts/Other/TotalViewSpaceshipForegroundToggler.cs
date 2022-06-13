using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalViewSpaceshipForegroundToggler : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    private void OnEnable()
    {
        InteractionHandler.ClickedInTotalViewEvent += OnClickedInTotalView;
    }
    private void OnDisable()
    {
        InteractionHandler.ClickedInTotalViewEvent -= OnClickedInTotalView;
    }

    private void OnClickedInTotalView(bool outsideOfShip)
    {
        spriteRenderer.enabled = outsideOfShip;
    }
}
