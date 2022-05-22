using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ZoomStateSpriteBlendListener : ZoomStateListenerBase
{
    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }
    protected override void OnChangedState(ZoomState newState)
    {
        if (newState == state)
            CoroutineUtil.ExecuteFloatRoutine(0f, 1f, BlendSprite, this, duration: 2f);
        else
            CoroutineUtil.ExecuteFloatRoutine(1f, 0f, BlendSprite, this, duration: 2f);
    }

    private void BlendSprite (float alpha)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}
