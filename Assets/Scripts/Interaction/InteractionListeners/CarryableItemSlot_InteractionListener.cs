using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CarryableItemSlot_InteractionListener : InteractableConditionBase, IInteractableInteractionListener
{
    [SerializeField] ItemData itemData;
    [SerializeField, ReadOnly] bool holdsItem;
    [SerializeField] SpriteRenderer itemRenderer;

    public override bool IsMet()
    {
        return holdsItem || (ServiceProvider.Player.GetPlayerItemInHand() == itemData && !holdsItem);
    }
    public void Interact()
    {
        if (holdsItem)
        {
            ServiceProvider.Player.SetItemInHand(itemData, itemRenderer.transform);
            itemRenderer.enabled = false;
            holdsItem = false;
        }
        else if (ServiceProvider.Player.GetPlayerItemInHand() == itemData)
        {
            float transitionDuration = ServiceProvider.Player.RemoveItemFromHand(itemRenderer.transform);
            CoroutineUtil.Delay(() =>
            {
                itemRenderer.enabled = true;
                holdsItem = true;
            }, this, transitionDuration);
        }

    }

#if UNITY_EDITOR

    public string GetComponentName() => "Item Slot : " + (itemData == null ? "<NULL>" : itemData.name);
    public void DrawInspector()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty itemDataProperty = serializedObject.FindProperty("itemData");
        EditorGUILayout.PropertyField(itemDataProperty);
        SerializedProperty holdsItemProperty = serializedObject.FindProperty("holdsItem");
        EditorGUILayout.PropertyField(holdsItemProperty, new GUIContent("holds Item on start"));
        SerializedProperty itemRendererProperty = serializedObject.FindProperty("itemRenderer");
        EditorGUILayout.PropertyField(itemRendererProperty);
        serializedObject.ApplyModifiedProperties();
    }
    public void RemoveComponent() => DestroyImmediate(this);
    public void SetVisible(bool visible) => hideFlags = visible ? HideFlags.None : HideFlags.HideInInspector;

    public bool GetVisible() => hideFlags == HideFlags.None;

#endif
}
