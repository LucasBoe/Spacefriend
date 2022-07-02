using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamePhaseTrigger_GameObjectActiveInactive : MinigamePhaseTriggerBase
{
    [SerializeField] GameObject toSetActive;
    [SerializeField] bool active;
    protected override void Trigger()
    {
        toSetActive.SetActive(active);
    }
}
