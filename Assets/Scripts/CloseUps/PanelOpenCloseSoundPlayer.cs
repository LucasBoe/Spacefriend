using SoundSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CloseUp))]
public class PanelOpenCloseSoundPlayer : MonoBehaviour
{
    CloseUp closeUp;
    Sound openSound, closeSound;
    private void Awake()
    {
        closeUp = GetComponent<CloseUp>();
        openSound = GameReferenceHolder.Instance.Sounds.PanelOpenSound;
        closeSound = GameReferenceHolder.Instance.Sounds.PanelCloseSound;
    }

    private void OnEnable()
    {
        closeUp.ChangePanelOpenEvent += OnChangeCloseUpOpen;
        openSound.Play();
    }


    private void OnDisable()
    {
        closeUp.ChangePanelOpenEvent -= OnChangeCloseUpOpen;
    }

    private void OnChangeCloseUpOpen(bool open)
    {
        if (!open) closeSound.Play();
    }
}
