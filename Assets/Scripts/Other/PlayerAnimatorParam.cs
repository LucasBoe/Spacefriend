using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEditor.Animations;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;
#endif
using System;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class PlayerAnimatorParamAttribute : PropertyAttribute
{

}
