using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ZoomStateSpriteBlendListener : ZoomStateListenerBase
{
    SpriteRenderer spriteRenderer;
    [SerializeField]float duration = 2f;

    private float alpha = 0f;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        alpha = spriteRenderer.color.a;
    }
    protected override void OnChangedState(ZoomState previous, ZoomState newState)
    {
        if (newState == state)
            CoroutineUtil.ExecuteFloatRoutine(alpha, 1f, BlendSprite, this, duration: duration);
        else if (previous == state)
            CoroutineUtil.ExecuteFloatRoutine(alpha, 0f, BlendSprite, this, duration: duration);
    }

    private void BlendSprite (float alpha)
    {
        this.alpha = alpha;
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}
