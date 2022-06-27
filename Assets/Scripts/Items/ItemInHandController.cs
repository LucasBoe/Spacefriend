using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class ItemInHandController : MonoBehaviour
{
    [SerializeField] ItemData item;
    [SerializeField, Foldout("Reference")] SpriteRenderer itemRenderer;
    [SerializeField, Foldout("Reference")] Transform readFlipTransform;
    [SerializeField, Foldout("Reference")] Transform targetPositionTransform, targetRotationTransform;
    [SerializeField, Foldout("Reference")] LimbSolver2D limbSolver;

    [SerializeField, BoxGroup("Lerp")] bool lerpActive = false;
    [SerializeField, BoxGroup("Lerp")] TransformData lerpWith;
    [SerializeField, Range(0, 1), BoxGroup("Lerp")] float lerpValue;

    public ItemData Item => item;

    private void LateUpdate()
    {
        if (item == null) return;

        bool flipped = readFlipTransform.localScale.x < 0.1f;

        TransformData target = GetTargetTransformData();

        if (lerpActive && lerpWith != null) target = target.Lerp(lerpWith, lerpValue);

        transform.SetPositionAndRotation(target.Position, target.Rotation);

        transform.localScale = new Vector3(flipped ? -1 : 1, 1, 1);
        itemRenderer.flipX = item.InHandFlip;
    }

    private TransformData GetTargetTransformData()
    {
        return new TransformData()
        {
            Position = targetPositionTransform.position + GetOffset(targetRotationTransform, readFlipTransform.localScale.x < 0.1f, item),
            Rotation = targetRotationTransform.rotation
        };
    }

    internal float SetItemInHand(ItemData data, Transform origin)
    {

        float transitonDuration = 0.25f;
        bool removeItem = data == null;

        if (removeItem)
        {
            CoroutineUtil.Delay(() =>
            {
                item = null;
                itemRenderer.sprite = null;
            }, this, transitonDuration);
        }
        else
        {
            item = data;
            itemRenderer.sprite = data.Sprite;
        }

        //TODO: Move the two Routines into one
        CoroutineUtil.ExecuteFloatRoutine(removeItem ? 0.75f : 0f, removeItem ? 0f : 0.75f, (value) => limbSolver.weight = value, this, 0.5f);


        lerpWith = new TransformData() { Position = origin.position, Rotation = origin.rotation };
        CoroutineUtil.ExecuteFloatRoutine(removeItem ? 0f : 1f, removeItem ? 1f : 0f, (float lerp) => lerpValue = lerp, this, transitonDuration);

        lerpActive = true;
        CoroutineUtil.Delay(() => lerpActive = false, this, transitonDuration);


        return transitonDuration;
    }

    private Vector3 GetOffset(Transform handTransform, bool flipped, ItemData item)
    {
        return (handTransform.right * item.InHandOffset.x * (flipped ? -1 : 1)) + (item.InHandOffset.y * handTransform.up);
    }
}

[System.Serializable]
public class TransformData
{
    public Vector3 Position;
    public Quaternion Rotation;

    public TransformData Lerp(TransformData toLerpWith, float t)
    {
        return new TransformData() { Position = Vector3.Lerp(Position, toLerpWith.Position, t), Rotation = Quaternion.Lerp(Rotation, toLerpWith.Rotation, t) };
    }
}
