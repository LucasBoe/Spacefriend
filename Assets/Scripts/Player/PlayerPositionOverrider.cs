using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionOverrider : MonoBehaviour
{
    //TODO: Add event the physics module can subscribe to
    private void OnEnable()
    {
        ServiceProvider.Player.SetPositionOverride(this);
    }

    private void OnDisable()
    {
        ServiceProvider.Player.RevokeOverridePosition(this);
    }
}
