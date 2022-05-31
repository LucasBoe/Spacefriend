using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpManager : MonoBehaviour
{
    List<CloseUp> activeCloseUps = new List<CloseUp>();
    private void OnEnable()
    {
        InteractionHandler.ClickOutsideOfCloseUpEvent += OnClickedOutsideOfCloseUp;
    }
    private void OnDisable()
    {
        InteractionHandler.ClickOutsideOfCloseUpEvent -= OnClickedOutsideOfCloseUp;
    }

    private void OnClickedOutsideOfCloseUp() { foreach (CloseUp closeUp in activeCloseUps) { closeUp.Close(); } }
}
