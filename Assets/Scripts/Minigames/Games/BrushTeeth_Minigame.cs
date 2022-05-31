using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushTeeth_Minigame : MinigamePhase
{
    [SerializeField] GameObject overriders;
    [SerializeField] SliderUIBehaviour slider;
    [SerializeField, PlayerAnimatorParam] string brushTeethParam;
    [SerializeField] ParticleSystem brushParticles;
    ParticleSystem.EmissionModule emissionModule;
    [SerializeField] AnimationCurve brushDeltaToParticlesPerSecondCurve;
    [SerializeField] AudioSource brushSource;
    [SerializeField] AnimationCurve brushDeltaToPitch, brushDeltaToVolume;

    Animator playerAnimator;
    float lastBrushValue = 0f;
    float newBrushDelta = 0f;
    float smoothedDelta;
    bool active = false;
    bool holdingMouse = false;

    protected override void Awake()
    {
        base.Awake();
        emissionModule = brushParticles.emission;
    }

    private void Start()
    {
        playerAnimator = PlayerServiceProvider.GetPlayerAnimator();
    }

    private void Update()
    {
        if (!active) return;

        float brushValueDelta = newBrushDelta;
        newBrushDelta = 0f;

        SetParticleEmissionBasedOnBrushDelta(brushValueDelta);

        smoothedDelta = Mathf.Lerp(smoothedDelta, brushValueDelta, Time.deltaTime * 10f);
        brushSource.pitch = brushDeltaToPitch.Evaluate(smoothedDelta);
        brushSource.volume = brushDeltaToVolume.Evaluate(smoothedDelta);
    }

    public override void StartPhase()
    {
        active = true;
        base.StartPhase();
        overriders.SetActive(true);
        slider.OnValueChanged.AddListener(OnValueChanged);
        brushSource.volume = 0;
        brushSource.Play();
        CoroutineUtil.Delay(EndPhase, this, 10f);
    }

    public override void EndPhase()
    {
        active = false;
        base.EndPhase();
        overriders.SetActive(false);
        SetParticleEmissionBasedOnBrushDelta(0f);
        slider.OnValueChanged.RemoveListener(OnValueChanged);
        CoroutineUtil.ExecuteFloatRoutine(brushSource.volume, 0, (float value) => brushSource.volume = value, this, duration: 0.5f);
    }
    private void OnValueChanged(float brushValue)
    {
        newBrushDelta = Mathf.Abs(brushValue - lastBrushValue);
        lastBrushValue = brushValue;
        playerAnimator.SetFloat(brushTeethParam, Mathf.Clamp(brushValue, 0f, 0.999f));
    }

    private void SetParticleEmissionBasedOnBrushDelta(float brushValueDelta)
    {
        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(brushDeltaToParticlesPerSecondCurve.Evaluate(brushValueDelta));
    }
}
