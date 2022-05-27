using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRoomToSpaceMode_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] SpaceTransitionRoom transitionRoom;

    public void Interact()
    {
        transitionRoom.SetRoomState(isSpace: true);
    }
}
