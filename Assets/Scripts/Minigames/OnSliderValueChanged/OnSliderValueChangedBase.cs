using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnSliderValueChangedBase : MonoBehaviour
{
    [SerializeField] SliderUIBehaviour slider;

    private void OnEnable()
    {
        slider.OnValueChanged.AddListener(OnValueChanged);
    }
    private void OnDisable()
    {
        slider.OnValueChanged.RemoveListener(OnValueChanged);
    }

    protected abstract void OnValueChanged(float value);
}
