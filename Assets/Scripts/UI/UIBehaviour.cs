using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIBehaviour : MonoBehaviour
{
    protected bool IsActive = false;
    private const float TRANSITION_DURATION = 1f;

    public void SetActive(bool active)
    {
        if (IsActive == active) return;
        StartCoroutine(BlendingRoutine(TRANSITION_DURATION, active));
        IsActive = active;
    }

    private IEnumerator BlendingRoutine(float duration, bool active)
    {
        if (!active) SetInteractable(false);

        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            SetAlpha(active ? t / duration : 1f - (t / duration));
            yield return null;
        }

        if (active) SetInteractable(true);
    }
    protected abstract void SetInteractable(bool active);
    protected abstract void SetAlpha(float v);

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
