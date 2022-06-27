using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChangeRoom_InteractionListener : InteractableInteraction_BaseBehaviour
{
    [SerializeField] RoomData room;

    public override void Interact()
    {
        RoomManager.TriggerEnterRoomEvent(room);
    }

#if UNITY_EDITOR
    public override void DrawInspector()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty roomProperty = serializedObject.FindProperty("room");
        EditorGUILayout.PropertyField(roomProperty);
        serializedObject.ApplyModifiedProperties();
    }
#endif
}
