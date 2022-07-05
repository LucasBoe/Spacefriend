using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System;
using NaughtyAttributes;


[FilePath("Other/EditorReferenceHolder.asset", FilePathAttribute.Location.ProjectFolder)]
public class EditorReferenceHolder : ScriptableSingleton<EditorReferenceHolder>
{
    public AnimatorController PlayerAnimatorController;
}
