using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEvent_MinigamePhaseTrigger : MinigamePhaseTriggerBase
{
    [SerializeField] UnityEvent customEvent;
    protected override void Trigger()
    {
        customEvent?.Invoke();
    }
}
