using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUIBehaviour : GroupUIBehaviour
{
    public Slider.SliderEvent OnValueChanged => slider.onValueChanged;

    [SerializeField] Slider slider;
}
