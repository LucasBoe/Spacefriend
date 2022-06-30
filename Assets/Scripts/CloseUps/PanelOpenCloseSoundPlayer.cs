using SoundSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatedPanel))]
public class PanelOpenCloseSoundPlayer : MonoBehaviour
{
    AnimatedPanel panel;
    Sound openSound, closeSound;
    private void Awake()
    {
        panel = GetComponent<AnimatedPanel>();
        openSound = GameReferenceHolder.Instance.Sounds.PanelOpenSound;
        closeSound = GameReferenceHolder.Instance.Sounds.PanelCloseSound;
    }

    private void OnEnable()
    {
        panel.ChangePanelOpenEvent += OnChangeCloseUpOpen;
        openSound.Play();
    }


    private void OnDisable()
    {
        panel.ChangePanelOpenEvent -= OnChangeCloseUpOpen;
    }

    private void OnChangeCloseUpOpen(bool open)
    {
        if (!open) closeSound.Play();
    }
}
