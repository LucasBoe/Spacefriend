using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToSpaceTest_InteractionListener : MonoBehaviour, IInteractionListener
{
    public void Interact()
    {
        PlayerManager.SetPlayerSkin(PlayerSkinType.Space);
        PlayerManager.SetSpaceMode(true);
    }
}
