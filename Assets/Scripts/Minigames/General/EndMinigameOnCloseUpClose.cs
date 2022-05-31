using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMinigameOnCloseUpClose : MonoBehaviour
{
    [SerializeField] CloseUp closeUp;
    [SerializeField] Minigame minigame;

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
        if (!open) minigame.EndMinigame();
    }
}
