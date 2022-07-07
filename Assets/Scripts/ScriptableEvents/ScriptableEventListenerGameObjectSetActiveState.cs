using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableEventListenerGameObjectSetActiveState : MonoBehaviour
{
    [SerializeField] ScriptableEvent scriptableEvent;
    [SerializeField] bool active;

#if UNITY_EDITOR
    [SerializeField] bool log;
#endif

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
#if UNITY_EDITOR
        if (log) Debug.Log("Event called: " + scriptableEvent.name);
#endif
        gameObject.SetActive(active);
    }
}
