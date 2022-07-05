using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BootRoutine : MonoBehaviour
{
    public UnityEvent[] awakeEvents, startEvents;

    void Awake() =>  HandleEventArray(awakeEvents);

    private void Start()
    {
        HandleEventArray(startEvents);
        Destroy(gameObject);
    }

    private void HandleEventArray(UnityEvent[] array)
    {
        foreach (UnityEvent ev in array)
        {
            ev?.Invoke();
        }
    }
}
