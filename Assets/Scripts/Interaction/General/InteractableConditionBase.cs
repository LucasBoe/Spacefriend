using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableConditionBase : MonoBehaviour
{
    public abstract bool IsMet();

    [ContextMenu("Test Condition")]
    private void Test_TEMP()
    {
        Debug.Log(IsMet());
    }
}
