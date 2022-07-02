using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectActiveInactive_MinigamePhaseTrigger : MinigamePhaseTriggerBase
{
    [SerializeField] GameObject toSetActive;
    [SerializeField] bool active;
    protected override void Trigger()
    {
        toSetActive.SetActive(active);
    }
}
