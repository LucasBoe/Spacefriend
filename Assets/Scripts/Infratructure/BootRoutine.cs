using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BootRoutine : MonoBehaviour
{
    public UnityEvent[] bootEvents;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (UnityEvent ev in bootEvents)
        {
            ev?.Invoke();
        }
        Destroy(gameObject);
    }
}
