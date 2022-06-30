using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUIBehaviour : UIBehaviour
{
    [SerializeField] protected Slider slider;
    AnimationCurve scaleCurve;

    private void Start()
    {
        scaleCurve = GameReferenceHolder.Instance.UiScaleInOutAnimationCurve;
        SetHidden(true);
    }

    public virtual Slider.SliderEvent OnValueChanged => slider.onValueChanged;

    public void SetValue(float value) => slider.value = value;

    protected override void SetInteractable(bool active)
    {
        slider.interactable = active;
    }

    protected override void SetVisibilityAmount(float value)
    {
        var scale = scaleCurve.Evaluate(value);
        slider.transform.localScale = new Vector3(scale, scale, scale);
    }

    protected override void SetHidden(bool hidden)
    {
        slider.gameObject.SetActive(!hidden);
    }
}
