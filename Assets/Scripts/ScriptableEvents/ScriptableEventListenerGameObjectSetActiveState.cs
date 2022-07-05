using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableEventListenerGameObjectSetActiveState : MonoBehaviour
{
    [SerializeField] ScriptableEvent scriptableEvent;
    [SerializeField] bool active;

    private void OnEnable()
    {
        scriptableEvent.AddListener(SetActive);
    }

    private void OnDisable()
    {
        scriptableEvent.RemoveListener(SetActive);
    }

    private void SetActive()
    {
        Debug.Log("Event called: " + scriptableEvent.name);
        gameObject.SetActive(active);
    }
}
