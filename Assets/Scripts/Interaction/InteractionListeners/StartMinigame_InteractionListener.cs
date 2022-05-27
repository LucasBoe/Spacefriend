using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMinigame_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] Minigame minigame;
    public void Interact()
    {
        minigame.StartMinigame();
    }
}
