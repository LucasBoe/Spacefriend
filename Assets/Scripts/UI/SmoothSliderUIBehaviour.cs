using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSliderUIBehaviour : SliderUIBehaviour
{
    [SerializeField, Foldout("Reference")] Slider sliderDirect;
    [SerializeField, Foldout("Reference")] Sprite sliderFillDashed, sliderFillFull;
    [SerializeField, Foldout("Reference")] Image sliderFillImageSmooth, sliderFillImageDirect;

    [SerializeField, Range(0.25f, 10f)] float sliderFillDuration = 4;

    [SerializeField, ReadOnly] bool isMoving = false;
    private bool IsMoving
    {
        get { return isMoving; }
        set
        {
            bool before = isMoving;
            isMoving = value;

            if (isMoving != before)
                ChangedMovingEvent?.Invoke(isMoving);
        }
    } 

    [SerializeField, ReadOnly] float target = 0;

    public System.Action<bool> ChangedMovingEvent;

    public override Slider.SliderEvent OnValueChanged => sliderDirect.onValueChanged;

    private void OnEnable()
    {
        slider.onValueChanged.AddListener(OnTargetValueChanged);
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnTargetValueChanged);
    }

    private void OnTargetValueChanged(float value)
    {
        IsMoving = true;
        target = value;
    } 

    private void Update()
    {
        if (!IsMoving) return;

        float before = sliderDirect.value;
        float sliderValue = Mathf.MoveTowards(before, target, Time.deltaTime / sliderFillDuration);

        sliderDirect.value = sliderValue;

        float difference = target - sliderValue;

        sliderFillImageDirect.sprite = (difference < 0) ? sliderFillFull : sliderFillDashed;
        sliderFillImageSmooth.sprite = (difference < 0) ? sliderFillDashed : sliderFillFull;

        if (Mathf.Abs(difference) < 0.01f)
        {
            Debug.Log(difference);
            IsMoving = false;
        }
    }
}
