using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBehaviour : MonoBehaviour
{
    protected bool IsActive = false;
    private const float TRANSITION_DURATION = 0.3f;

    public void SetActive(bool active)
    {
        if (IsActive == active) return;
        StartCoroutine(BlendingRoutine(TRANSITION_DURATION, active));
        IsActive = active;
    }

    private IEnumerator BlendingRoutine(float duration, bool active)
    {
        if (active)
            SetHidden(false);
        else
            SetInteractable(false);


        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            SetVisibilityAmount(active ? t / duration : 1f - (t / duration));
            yield return null;
        }

        if (active)
            SetInteractable(true);
        else
            SetHidden(false);
    }
    protected abstract void SetInteractable(bool active);
    protected abstract void SetHidden(bool hidden);
    protected abstract void SetVisibilityAmount(float v);

    [Button]
    private void SetActive_TEMP()
    {
        SetActive(true);
    }

    [Button]
    private void SetInactive_TEMP()
    {
        SetActive(false);
    }
}
