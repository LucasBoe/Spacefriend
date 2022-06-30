using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class AnimatedPanel : MonoBehaviour
{
    private static AnimationCurve easeInOutCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private const float ANIMATION_DURATION = 0.25f;
    [SerializeField, ReadOnly] protected bool open;

    public System.Action<bool> ChangePanelOpenEvent;

    protected virtual void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Panel");
        open = true;
        Close();
    }

    public void Toggle()
    {
        if (open)
            Close();
        else
            Open();
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);

        if (open) return;
        open = true;

        ChangePanelOpenEvent?.Invoke(true);

        StopAllCoroutines();
        CoroutineUtil.ExecuteFloatRoutine(0, 1, (float t) => transform.localScale = new Vector3(easeInOutCurve.Evaluate(t), 1, 1), this, ANIMATION_DURATION);
    }

    public virtual void Close()
    {
        if (!open) return;
        open = false;

        ChangePanelOpenEvent?.Invoke(false);

        StopAllCoroutines();
        CoroutineUtil.ExecuteFloatRoutine(1, 0, (float t) => transform.localScale = new Vector3(easeInOutCurve.Evaluate(t), 1, 1), this, ANIMATION_DURATION);
        CoroutineUtil.Delay(() => gameObject.SetActive(false), this, ANIMATION_DURATION);
    }
}
