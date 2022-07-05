using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScripableEventTrigger : MonoBehaviour
{
    [SerializeField] ScriptableEvent scriptableEvent;
    public void Trigger()
    {
        scriptableEvent?.Invoke();
    }
}
