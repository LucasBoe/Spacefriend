using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionOverrider : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("OnEnable");
        PlayerManager.Instance.SetPositionOverride(this);
    }

    private void OnDisable()
    {
        PlayerManager.Instance.RevokeOverridePosition(this);
    }
}
