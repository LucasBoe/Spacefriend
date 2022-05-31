using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUIBehaviour : GroupUIBehaviour
{
    public Button.ButtonClickedEvent OnClick => button.onClick;

    [SerializeField] Button button;
}
