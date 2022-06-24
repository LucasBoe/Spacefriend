using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog_InteractionListener : InteractableInteraction_BaseBehaviour
{
    public override void Interact()
    {
        Debug.Log(name);
    }
}
