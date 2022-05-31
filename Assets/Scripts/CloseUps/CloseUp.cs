using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class CloseUp : MonoBehaviour
{
    private const float ANIMATION_DURATION = 0.25f;
    [SerializeField, ReadOnly] private bool open;

    public System.Action<bool> ChangeCloseUpOpenEvent;

    private void OnEnable() => InteractionHandler.ClickOutsideOfCloseUpEvent += OnClickedOutsideOfCloseUp;
    private void OnDisable() => InteractionHandler.ClickOutsideOfCloseUpEvent -= OnClickedOutsideOfCloseUp;
    private void OnClickedOutsideOfCloseUp() => Close();

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("CloseUp");

        open = true;
        Close();
    }

    internal void Toggle()
    {
        if (open)
            Close();
        else
            Open();
    }

    public void Open()
    {
        if (open) return;
        open = true;

        ChangeCloseUpOpenEvent?.Invoke(true);

        StopAllCoroutines();
        gameObject.SetActive(true);
        CoroutineUtil.ExecuteFloatRoutine(0,1, (float t) => transform.localScale = new Vector3(t,1,1), this, ANIMATION_DURATION);
    }

    public void Close()
    {
        if (!open) return;
        open = false;

        ChangeCloseUpOpenEvent?.Invoke(false);

        StopAllCoroutines();
        CoroutineUtil.ExecuteFloatRoutine(1, 0, (float t) => transform.localScale = new Vector3(t, 1, 1), this, ANIMATION_DURATION);
        CoroutineUtil.Delay(() => gameObject.SetActive(false), this, ANIMATION_DURATION);
    }
}
