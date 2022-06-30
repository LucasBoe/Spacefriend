using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothSliderUIBehaviour : SliderUIBehaviour
{
    [SerializeField] Slider sliderDirect;
    [SerializeField] Sprite sliderFillDashed, sliderFillFull;
    [SerializeField] Image sliderFillImageSmooth, sliderFillImageDirect;

    [SerializeField] float sliderFillDuration = 4;

    [SerializeField, ReadOnly] bool isMoving = false;
    [SerializeField, ReadOnly] float target = 0;

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
        isMoving = true;
        target = value;
    } 

    private void Update()
    {
        if (!isMoving) return;

        float before = sliderDirect.value;
        float sliderValue = Mathf.MoveTowards(before, target, Time.deltaTime / sliderFillDuration);

        sliderDirect.value = sliderValue;

        float difference = target - sliderValue;

        sliderFillImageDirect.sprite = (difference < 0) ? sliderFillFull : sliderFillDashed;
        sliderFillImageSmooth.sprite = (difference < 0) ? sliderFillDashed : sliderFillFull;

        if (Mathf.Abs(difference) < 0.01f)
        {
            Debug.Log(difference);
            isMoving = false;
        }
    }
}
