using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStinkyPropertyVisualizer : MonoBehaviour
{
    [SerializeField] PlayerProperty stinkyProperty;
    [SerializeField] ParticleSystem stinkyParticles;
    ParticleSystem.EmissionModule emissionModule;

    private void Awake()
    {
        emissionModule = stinkyParticles.emission;
    }
    private void OnEnable()
    {
        stinkyProperty.ValueChangedEvent += OnValueChanged;
        OnValueChanged(stinkyProperty.Value);
    }
    private void OnDisable()
    {
        stinkyProperty.ValueChangedEvent -= OnValueChanged;
    }

    private void OnValueChanged(float value)
    {
        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(value);
    }
}
