using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchNavGrid_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] NavigationGrid navGrid;
    public void Interact()
    {
        NavigationAgent.TriggerSwitchEvent?.Invoke(navGrid);
    }
}
