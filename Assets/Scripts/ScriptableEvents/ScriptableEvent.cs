using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScriptableEvent : ScriptableObject
{
    private System.Action _event;
    public void AddListener(System.Action listener)
    {
        _event += listener;
    }

    public void RemoveListener(System.Action listener)
    {
        _event -= listener;
    }

    public void Invoke()
    {
        _event?.Invoke();
    }
}