using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CarryItem_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] ItemData data;
    [SerializeField] bool disableInsteadOfDestroy = false;

    public void Interact()
    {
        PlayerServiceProvider.CollectItemToHand(data, transform);

        if (disableInsteadOfDestroy)
            gameObject.SetActive(false);
        else
            Destroy(gameObject);
    }

#if UNITY_EDITOR
    public string GetComponentName() => "Carry Item";
    public void DrawInspector()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty dataProperty = serializedObject.FindProperty("data");
        EditorGUILayout.PropertyField(dataProperty);

        SerializedProperty disableInsteadOfDestroyProperty = serializedObject.FindProperty("disableInsteadOfDestroy");
        EditorGUILayout.PropertyField(disableInsteadOfDestroyProperty);
    }

#endif
}
