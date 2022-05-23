using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog_InteractionListener : MonoBehaviour, IInteractionListener
{
    public void Interact()
    {
        Debug.Log(name);
    }
}
