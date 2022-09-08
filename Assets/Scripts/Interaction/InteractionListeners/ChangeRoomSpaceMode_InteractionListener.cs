using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ChangeRoomSpaceMode_InteractionListener : InteractableInteraction_BaseBehaviour
{
    //[SerializeField] SpaceTransitionRoom transitionRoom;
    [SerializeField] RoomData spaceRoom;
    [SerializeField] bool space = true;
    [SerializeField] bool addSpaceSuite = true;

    public override void Interact()
    {
        RoomManager.TriggerEnterRoomEvent?.Invoke(spaceRoom);
        if (addSpaceSuite) ServiceProvider.Player.SetPlayerSkin(space ? PlayerSkinType.Astronaut : PlayerSkinType.Clothes);
    }

#if UNITY_EDITOR
    public override string GetComponentName() => "Change Room (Space Mode)";
    public override void DrawInspector()
    {
        SerializedObject serializedObject = new SerializedObject(this);
        SerializedProperty spaceRoomProperty = serializedObject.FindProperty("spaceRoom");
        EditorGUILayout.PropertyField(spaceRoomProperty);
        SerializedProperty spaceProperty = serializedObject.FindProperty("space");
        EditorGUILayout.PropertyField(spaceProperty);
        SerializedProperty addSpaceSuiteProperty = serializedObject.FindProperty("addSpaceSuite");
        EditorGUILayout.PropertyField(addSpaceSuiteProperty);
        serializedObject.ApplyModifiedProperties();
    }
#endif
}
