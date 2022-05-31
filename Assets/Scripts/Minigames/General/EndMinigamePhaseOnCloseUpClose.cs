using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMinigamePhaseOnCloseUpClose : MonoBehaviour
{
    [SerializeField] CloseUp closeUp;
    [SerializeField] MinigamePhase minigamePhase;

    private void OnEnable()
    {
        closeUp.ChangeCloseUpOpenEvent += OnChangeCloseUpOpen;
    }
    private void OnDisable()
    {
        closeUp.ChangeCloseUpOpenEvent -= OnChangeCloseUpOpen;
    }

    private void OnChangeCloseUpOpen(bool open)
    {
        if (!open) minigamePhase.EndPhase();
    }
}
