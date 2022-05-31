using SoundSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CloseUp))]
public class CloseUpSoundPlayerOpenClose : MonoBehaviour
{
    CloseUp closeUp;
    Sound openSound, closeSound;
    private void Awake()
    {
        closeUp = GetComponent<CloseUp>();
        openSound = GameReferenceHolder.Instance.Sounds.PopupOpenSound;
        closeSound = GameReferenceHolder.Instance.Sounds.PopupCloseSound;
    }

    private void OnEnable()
    {
        closeUp.ChangeCloseUpOpenEvent += OnChangeCloseUpOpen;
        openSound.Play();
    }


    private void OnDisable()
    {
        closeUp.ChangeCloseUpOpenEvent -= OnChangeCloseUpOpen;
    }

    private void OnChangeCloseUpOpen(bool open)
    {
        if (!open) closeSound.Play();
    }
}
