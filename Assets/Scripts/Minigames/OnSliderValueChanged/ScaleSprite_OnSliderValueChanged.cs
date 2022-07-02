using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ScaleSprite_OnSliderValueChanged : OnSliderValueChangedBase
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite baseSprite;
    [SerializeField] ScaleSpriteData[] scaleSpriteDatas;
    protected override void OnValueChanged(float value)
    {
        for (int i = scaleSpriteDatas.Length -1; i >= 0; i--)
        {
            ScaleSpriteData data = scaleSpriteDatas[i];

            if (data.InTime <= value)
            {
                spriteRenderer.sprite = data.Sprite;
                transform.localScale = data.ScaleOverTimeV3Curve.Evaluate(value);
                return;
            }
        }


        spriteRenderer.sprite = baseSprite;
        transform.localScale = Vector3.one;
    }
}

[System.Serializable]
public class ScaleSpriteData
{
    public Sprite Sprite;
    public float InTime;
    public Vector3AnimationCurve ScaleOverTimeV3Curve;
}

[System.Serializable]
public class Vector3AnimationCurve
{
    [CurveRange(0,0,1,1)] public AnimationCurve xCurve, yCurve, zCurve;
    public Vector3 Evaluate(float time)
    {
        return new Vector3(xCurve.Evaluate(time), yCurve.Evaluate(time), zCurve.Evaluate(time));
    } 
}
