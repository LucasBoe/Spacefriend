using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : SingletonBehaviour<MainMenu>
{
    [SerializeField] CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
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
        if (before == GameMode.MainMenu) SetActive(false);
        if (after == GameMode.MainMenu) SetActive(true);
    }

    public void SetActive(bool active)
    {
        CoroutineUtil.ExecuteFloatRoutine(active ? 0f : 1f, active ? 1f : 0f, SetAlpha, this, 0.5f);
        if (!active) canvasGroup.interactable = false;
        else CoroutineUtil.Delay(() => canvasGroup.interactable = true, this, 0.5f);
    }

    private void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }

    public void MenueOption_Continue()
    {
        GameModeManager.SetGameMode(GameMode.Total);
    }
}
