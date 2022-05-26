using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionOverrider : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerManager.SetPositionOverride(this);
    }

    private void OnDisable()
    {
        PlayerManager.RevokeOverridePosition(this);
    }
}
