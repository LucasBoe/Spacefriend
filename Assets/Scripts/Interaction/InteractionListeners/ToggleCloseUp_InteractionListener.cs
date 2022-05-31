using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCloseUp_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] CloseUp closeUp;

    public void Interact()
    {
        closeUp.Toggle();
    }
}
