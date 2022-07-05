using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityToolbarExtender;
namespace Tools
{
    [InitializeOnLoad]
    public class EditorPlayFromStartButtonAdder
    {
        static EditorPlayFromStartButtonAdder()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            if (!Application.isPlaying)
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button(new GUIContent(EditorGUIUtility.FindTexture("d_Animation.PrevKey"), "Play from Start"), ToolbarStyles.commandButtonStyle))
                {
                    EditorPersistentDataStorage.TestFromStart = true;
                    EditorApplication.EnterPlaymode();
                }
            }
        }

        static class ToolbarStyles
        {
            public static readonly GUIStyle commandButtonStyle;

            static ToolbarStyles()
            {
                commandButtonStyle = new GUIStyle("Command")
                {
                    fontSize = 16,
                    alignment = TextAnchor.MiddleCenter,
                    imagePosition = ImagePosition.ImageAbove,
                    fontStyle = FontStyle.Bold
                };
            }
        }
    }
}
#endif