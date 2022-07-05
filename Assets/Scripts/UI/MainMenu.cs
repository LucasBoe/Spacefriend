using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : SingletonBehaviour<MainMenu>
{
    [SerializeField] CanvasGroup canvasGroup;

    protected override void Awake()
    {
        canvasGroup.alpha = 0;
        SetInteractabe(false);
    }

    private void OnEnable()
    {
        GameModeManager.GameModeChangedEvent += OnGameModeChanged;
    }

    private void OnDisable()
    {
        GameModeManager.GameModeChangedEvent -= OnGameModeChanged;
    }

    private void OnGameModeChanged(GameMode before, GameMode after)
    {
        if (before == GameMode.Menu) SetActive(false);
        if (after == GameMode.Menu) SetActive(true);
    }

    public void SetActive(bool active)
    {
        CoroutineUtil.ExecuteFloatRoutine(active ? 0f : 1f, active ? 1f : 0f, SetAlpha, this, 0.5f);
        if (!active) SetInteractabe(false);
        else CoroutineUtil.Delay(() => SetInteractabe(true), this, 0.5f);
    }

    private void SetInteractabe(bool interactable)
    {
        canvasGroup.interactable = interactable;
        canvasGroup.blocksRaycasts = interactable;
    }

    private void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    public void MenueOption_Continue()
    {
        GameModeManager.SetGameMode(GameMode.Play);
    }
}
