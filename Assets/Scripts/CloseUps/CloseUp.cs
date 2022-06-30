using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUp : AnimatedPanel
{
    private void OnEnable() => InteractionHandler.ClickOutsideOfCloseUpEvent += OnClickedOutsideOfCloseUp;
    private void OnDisable() => InteractionHandler.ClickOutsideOfCloseUpEvent -= OnClickedOutsideOfCloseUp;
    private void OnClickedOutsideOfCloseUp() => Close();
}
