using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionOverrider : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerServiceProvider.SetPositionOverride(this);
    }

    private void OnDisable()
    {
        PlayerServiceProvider.RevokeOverridePosition(this);
    }
}
