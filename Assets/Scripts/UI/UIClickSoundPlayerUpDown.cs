using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SoundSystem;

public class UIClickSoundPlayerUpDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Sound pointerDownSound, pointerUpSound;
    private void Awake()
    {
        pointerDownSound = GameReferenceHolder.Instance.Sounds.PointerDownSound;
        pointerUpSound = GameReferenceHolder.Instance.Sounds.PointerUpSound;
    }

    public void OnPointerDown(PointerEventData eventData) => pointerDownSound.Play();
    public void OnPointerUp(PointerEventData eventData) => pointerUpSound.Play();
}
