using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPositionOnSliderValueChanged : MonoBehaviour
{
    [SerializeField] SliderUIBehaviour slider;
    [SerializeField] Transform startTransform, endTransform;
    [SerializeField, CurveRange(0, 0, 1, 1)] AnimationCurve lerpValueCurve;
    Vector3 startPos, endPos;

    private void Awake()
    {
        startPos = startTransform.position;
        endPos = endTransform.position;
        transform.position = startPos;
    }

    private void OnEnable()
    {
        slider.OnValueChanged.AddListener(OnValueChanged);
    }
    private void OnDisable()
    {
        slider.OnValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        float lerp = lerpValueCurve.Evaluate(value);
        transform.position = Vector3.Lerp(startPos, endPos, lerp);
    }
}
