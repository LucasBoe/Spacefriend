using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog_InteractionListener : InteractionListenerBaseBehaviour
{
    public override void Interact()
    {
        Debug.Log(name);
    }
}
