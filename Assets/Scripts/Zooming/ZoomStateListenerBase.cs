using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ZoomStateListenerBase : MonoBehaviour
{
    [SerializeField] protected ZoomState state;
    private void OnEnable()
    {
        ZoomHandler.ChangedStateEvent += OnChangedState;
    }
    private void OnDisable()
    {
        ZoomHandler.ChangedStateEvent -= OnChangedState;
    }

    protected abstract void OnChangedState(ZoomState newState);
}
