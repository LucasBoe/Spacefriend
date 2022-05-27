using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerProperty : ScriptableObject
{
    public System.Action<float> ValueChangedEvent;
    [SerializeField] private float _value;
    public float Value
    {
        get { return _value; }
        set
        {
            _value = value;
            ValueChangedEvent?.Invoke(value);
        }
    }
}
