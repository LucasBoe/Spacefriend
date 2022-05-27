using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRoomSpaceMode_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] SpaceTransitionRoom transitionRoom;
    [SerializeField] bool space = true;

    public void Interact()
    {
        transitionRoom.SetRoomState(isSpace: space);
    }
}
