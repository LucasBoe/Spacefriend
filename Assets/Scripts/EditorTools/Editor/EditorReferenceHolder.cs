using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System;
using NaughtyAttributes;


[FilePath("Other/EditorReferenceHolder.asset", FilePathAttribute.Location.PreferencesFolder)]
public class EditorReferenceHolder : ScriptableSingleton<EditorReferenceHolder>
{
    public AnimatorController PlayerAnimatorController;
    [ReadOnly] public string LastSelectedObjectName;
    public static void StoreLastSelectedObject(UnityEngine.Object selected)
    {
        instance.LastSelectedObjectName = selected.name;
    }

    public static string GetLastSelected()
    {
        return (instance.LastSelectedObjectName);
    }
}
