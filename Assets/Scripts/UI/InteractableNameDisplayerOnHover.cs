using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InteractableNameDisplayerOnHover : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private void OnEnable()
    {
        Interactable.BeginHoverInteractableEvent += OnBeginHoverInteractable;
        Interactable.EndHoverInteractableEvent += OnEndHoverInteractable;
    }

    private void OnDisable()
    {
        Interactable.BeginHoverInteractableEvent -= OnBeginHoverInteractable;
        Interactable.EndHoverInteractableEvent -= OnEndHoverInteractable;
    }
    private void OnBeginHoverInteractable(Interactable interactable)
    {
        text.text = interactable.DisplayName;
    }
    private void OnEndHoverInteractable(Interactable interactable)
    {
        text.text = "";
    }
}
