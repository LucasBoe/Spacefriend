using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUIBehaviour : GroupUIBehaviour
{
    public Slider.SliderEvent OnValueChanged => slider.onValueChanged;
    public void SetValue(float value) => slider.value = value; 

    [SerializeField] Slider slider;
}
