using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRoomSpaceMode_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] SpaceTransitionRoom transitionRoom;
    [SerializeField] bool space = true;
    [SerializeField] bool addSpaceSuite = true;

    public void Interact()
    {
        transitionRoom.SetRoomState(isSpace: space);
        if (addSpaceSuite) PlayerManager.SetPlayerSkin(space ? PlayerSkinType.Space : PlayerSkinType.Default);
    }
}
