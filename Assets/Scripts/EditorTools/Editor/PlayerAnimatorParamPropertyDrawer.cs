using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Animations;
#endif

using System.Linq;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(PlayerAnimatorParamAttribute))]
public class PlayerAnimatorParamPropertyDrawer : PropertyDrawer
{
    int _choiceIndex;
    bool editMode = false;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float EditButtonWidth = 50f;
        var varRect = new Rect(position.x, position.y, position.width - EditButtonWidth, position.height);
        var editRect = new Rect(position.width - EditButtonWidth, position.y, EditButtonWidth, position.height);

        string selected = property.stringValue;

        if (editMode)
        {
            EditorGUI.BeginChangeCheck();

            AnimatorController playerController = EditorReferenceHolder.instance.PlayerAnimatorController;

            string[] variables = playerController.parameters.Select(p => p.name).ToArray();

            int index = 0;
            for (int i = 0; i < variables.Length; i++)
            {
                if (selected == variables[i])
                    index = i;
            }

            _choiceIndex = EditorGUI.Popup(position, index, variables);
            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = variables[_choiceIndex];
                editMode = false;
            }
        }
        else
        {
            GUI.Box(varRect, "param : " + (selected == "" ? "<UNDEFINED>" : selected));
            if (GUI.Button(editRect, "Edit"))
            {
                editMode = true;
            }
        }
    }
}
#endif