using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CarryItem_InteractionListener : InteractionListenerBaseBehaviour
{
    [SerializeField] ItemData data;
    [SerializeField] bool disableInsteadOfDestroy = false;

    public override void Interact()
    {
        PlayerServiceProvider.CollectItemToHand(data, transform);

        if (disableInsteadOfDestroy)
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
    }

#if UNITY_EDITOR
    public override void DrawInspector()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty dataProperty = serializedObject.FindProperty("data");
        EditorGUILayout.PropertyField(dataProperty);

        SerializedProperty disableInsteadOfDestroyProperty = serializedObject.FindProperty("disableInsteadOfDestroy");
        EditorGUILayout.PropertyField(disableInsteadOfDestroyProperty);
    }

#endif
}
