using NaughtyAttributes;
using SoundSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shower_Minigame : MinigamePhase
{
    [SerializeField] float showerPhaseDuration;
    [Foldout("References"), SerializeField] SliderUIBehaviour slider;
    [SerializeField] AnimationCurve heatToParticleAmountCurve, heatToParticleSizeCurve, heatToWhistlingVolume;
    [Foldout("References"), SerializeField] ParticleSystem steamShowerParticles;
    [Foldout("References"), SerializeField] SpriteRenderer showerRenderer;
    [Foldout("References"), SerializeField] Material showerMaterial;
    [Foldout("References"), SerializeField] PlayerProperty stinkyProperty;
    [Foldout("References"), SerializeField] PlayerAnimationOverrider showerAnimationOverrider;
    [Foldout("References"), SerializeField] AudioSource whistlingAudioSource;
    [SerializeField] string blurPropertyName;
    [SerializeField] Sound showerSound, freezingSound;
    [SerializeField, Range(0, 1)] float freezingEdgeValue;
    bool isFreezing = false;
    Material regularInteractableMaterial;
    ParticleSystem.EmissionModule emissionModule;
    ParticleSystem.MainModule mainModule;


    protected override void Awake()
    {
        base.Awake();
        emissionModule = steamShowerParticles.emission;
        mainModule = steamShowerParticles.main;
    }

    public override void StartPhase()
    {
        base.StartPhase();
        slider.OnValueChanged.AddListener(OnValueChanged);
        CoroutineUtil.Delay(EndPhase, this, showerPhaseDuration);
        CoroutineUtil.Delay(() => stinkyProperty.Value = 0, this, showerPhaseDuration / 2f);
        showerSound.PlayLoop(fadeDuration: 0.5f);
        showerAnimationOverrider.gameObject.SetActive(true);
        whistlingAudioSource.gameObject.SetActive(true);
    }

    public override void EndPhase()
    {
        base.EndPhase();
        slider.OnValueChanged.RemoveListener(OnValueChanged);
        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(0);
        PlayerServiceProvider.SetPlayerSkin(PlayerSkinType.AfterShower);
        showerSound.Stop();
        showerAnimationOverrider.gameObject.SetActive(false);
        whistlingAudioSource.gameObject.SetActive(false);

    }

    private void OnValueChanged(float heat)
    {
        emissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(heatToParticleAmountCurve.Evaluate(heat));
        mainModule.startSize = new ParticleSystem.MinMaxCurve(heatToParticleSizeCurve.Evaluate(heat));
        whistlingAudioSource.volume = heatToWhistlingVolume.Evaluate(heat);

        bool shouldBeFreezing = heat < freezingEdgeValue;

        if (!isFreezing && shouldBeFreezing)
        {
            freezingSound.Stop();
            freezingSound.Play();
        }

        isFreezing = shouldBeFreezing;
    }

    public void AnimateShowerBlurIn()
    {
        regularInteractableMaterial = showerRenderer.material;
        showerRenderer.material = showerMaterial;

        CoroutineUtil.ExecuteFloatRoutine(0, 1, SetBlurProperty, this, 1f);
    }
    public void AnimateShowerBlurOut()
    {
        CoroutineUtil.ExecuteFloatRoutine(1, 0, SetBlurProperty, this, 1f);
        CoroutineUtil.Delay(() => showerRenderer.material = regularInteractableMaterial, this, 1f);
    }

    private void SetBlurProperty(float value)
    {
        showerMaterial.SetFloat(blurPropertyName, value);
    }
}
