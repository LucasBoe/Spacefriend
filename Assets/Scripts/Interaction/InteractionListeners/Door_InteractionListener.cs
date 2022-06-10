using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_InteractionListener : MonoBehaviour, IInteractionListener
{
    [SerializeField] Room room;
    public void Interact()
    {
        Room.TriggerEnterRoomEvent(room);
    }
}
