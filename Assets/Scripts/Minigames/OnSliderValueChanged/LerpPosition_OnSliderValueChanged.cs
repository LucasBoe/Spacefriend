using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPosition_OnSliderValueChanged : OnSliderValueChangedBase
{
    [SerializeField] Transform startTransform, endTransform;
    [SerializeField, CurveRange(0, 0, 1, 1)] AnimationCurve lerpValueCurve;
    Vector3 startPos, endPos;

    private void Awake()
    {
        startPos = startTransform.position;
        endPos = endTransform.position;
        transform.position = startPos;
    }

    protected override void OnValueChanged(float value)
    {
        float lerp = lerpValueCurve.Evaluate(value);
        transform.position = Vector3.Lerp(startPos, endPos, lerp);
    }
}
