using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequenceController : MonoBehaviour
{
    [SerializeField] SoundSystem.Sound engineStartSound;
    [SerializeField] GameObject stationGameObject, flightGameObject, exlosionEffect;
    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] Animator vCamAnimator;
    [SerializeField] Minigame startMinigame;

    bool played = false;
    bool finishedStartMinigame = false;

    private void Awake()
    {
        bool shouldPlay = true;
#if UNITY_EDITOR

        shouldPlay = EditorPersistentDataStorage.TestFromStart;
#endif

        if (!shouldPlay)
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameModeManager.GameModeChangedEvent += OnGameModeChanged;
    }
    private void OnDisable()
    {
        GameModeManager.GameModeChangedEvent -= OnGameModeChanged;
    }
    private void OnGameModeChanged(GameMode previous, GameMode next)
    {
        if (played) return;

        if (next == GameMode.Play)
        {
            played = true;
            StopAllCoroutines();
            StartCoroutine(SequenceRoutine());
        }
    }

    private IEnumerator SequenceRoutine()
    {
        ZoomHandler.SetZoomAllowed(false);
        GameModeManager.SetGameMode(GameMode.TotalSequence);
        vCamAnimator.enabled = true;

        yield return new WaitForSeconds(10f);

        startMinigame.StartMinigame();

        while (!finishedStartMinigame) yield return null;

        engineStartSound.Play();

        yield return new WaitForSeconds(9);
        vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 20f;
        vCam.transform.position = new Vector3(-66, vCam.transform.position.y, vCam.transform.position.z);
        vCam.Priority = 1;

        exlosionEffect.SetActive(true);
        flightGameObject.SetActive(true);
        stationGameObject.SetActive(false);
        yield return null;
        yield return null;
        exlosionEffect.SetActive(false);

        GameModeManager.SetGameMode(GameMode.Total);
        ZoomHandler.SetZoomAllowed(true);

        yield return new WaitForSeconds(0.5f);
    }

    public void FinishStartMinigame()
    {
        finishedStartMinigame = true;
    }
}
